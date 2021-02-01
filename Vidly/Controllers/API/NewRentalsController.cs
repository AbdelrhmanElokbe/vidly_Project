using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using Vidly.Dtos;
using Vidly.Models;

namespace Vidly.Controllers.API
{
    public class NewRentalsController : ApiController
    {

        private ApplicationDbContext _db ;
        public NewRentalsController()
        {
            _db = new ApplicationDbContext();
           
        }

        [HttpPost]
        //POST /api/NewRentals
        public IHttpActionResult CreateRent(NewRentalDto newRental)
        {
            var customer = _db.Customers.Single(
                c => c.Id == newRental.CustomerId);

            var movies = _db.Movie.Where(
                m => newRental.MovieIds.Contains(m.Id)).ToList();
            foreach(var movie in movies)
            {
                if (movie.NumberAvailable == 0)
                    return BadRequest("Movie is not available.");

                movie.NumberAvailable-- ;
                var rental = new Rental
                {
                    Customer = customer,
                    Movie = movie,
                    DateRetal = DateTime.Now
                };

                _db.Rentals.Add(rental);
            }
            _db.SaveChanges();
            return Ok();
        }
    }
}
