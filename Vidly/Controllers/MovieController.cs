using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.viewModels;
using System.Data.Entity.Validation;

namespace Vidly.Controllers
{
    public class MovieController : Controller
    {

        private ApplicationDbContext _db;
        
        public MovieController()   //ctor
        {
            _db = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _db.Dispose();
        }

        
        public ViewResult Index()     // get all movies
        {
            if(User.IsInRole(RoleName.CanManageMovies))
                return View("List");
             
            return View("ReadOnlyList");

        }

        [Authorize(Roles = RoleName.CanManageMovies)]
        public ActionResult Edit(int id)    // get specific movie
        {
            ViewBag.Name = "Edit";
            var movie = _db.Movie.SingleOrDefault(m => m.Id == id);

            var viewModel = new MovieFormViewModel(movie)
            {
                
                GenreType = _db.GenreType.ToList()
            };
            return View("MovieForm", viewModel);
        }

        [HttpGet]
        [Authorize(Roles = RoleName.CanManageMovies)]
        public ActionResult New()
        {
            ViewBag.Name = "New";
            var GenreList = _db.GenreType.ToList();
            var viewModel = new MovieFormViewModel
            {
                GenreType = GenreList
            };
            return View("MovieForm", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Movie movie)
        {
            
            if(!ModelState.IsValid)
            {
                var viewModel = new MovieFormViewModel(movie)
                {
                    GenreType = _db.GenreType.ToList()
                };
                return View("MovieForm", viewModel);
            }
            
            if(movie.Id == 0)
            {
                movie.DateAdded = DateTime.Now; 
                _db.Movie.Add(movie);
            }
            else
            {
                var MovieInDb = _db.Movie.Single(m => m.Id == movie.Id);

                MovieInDb.Name = movie.Name;
                MovieInDb.ReleseDate = movie.ReleseDate;
                MovieInDb.GenreTypeId = movie.GenreTypeId;
                MovieInDb.NumberInStock = movie.NumberInStock;
            }
            
                _db.SaveChanges();
          
            return RedirectToAction("Index","Movie");
        }
        
    }
}