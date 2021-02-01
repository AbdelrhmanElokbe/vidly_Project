using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.Entity;
//using System.Web.Mvc;
using Vidly.Models;
using Vidly.Dtos;
using AutoMapper;

namespace Vidly.Controllers.API
{
    public class MovieController : ApiController
    {
        private ApplicationDbContext _db;
        public MovieController()
        {
            _db = new ApplicationDbContext();
        }

        // GET /api/movie
        public IEnumerable<MovieDto> GetMovies(string query = null)
        {
            var MoviesQuery = _db.Movie
                .Include(m => m.GenreType)
                .Where(m => m.NumberAvailable > 0);

            if (!string.IsNullOrEmpty(query))
                MoviesQuery = MoviesQuery.Where(m => m.Name.Contains(query));

            var MovieDtos = MoviesQuery
                .ToList()
                .Select(Mapper.Map<Movie, MovieDto>);

            return MovieDtos;
        }

        // GET /api/movie/10
        public IHttpActionResult GetMovie(int id)
        {
            var movie = _db.Movie.SingleOrDefault(m => m.Id == id);
            if (movie == null)
                return NotFound();

            return Ok(Mapper.Map<Movie, MovieDto>(movie));
        }

        // POST /api/movie
        [HttpPost]
        [Authorize(Roles = RoleName.CanManageMovies)]
        public IHttpActionResult CreateMovie(MovieDto movieDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var movie = Mapper.Map<MovieDto, Movie>(movieDto);
            movie.DateAdded = DateTime.Now ;
            _db.Movie.Add(movie);
            _db.SaveChanges();

            movieDto.Id = movie.Id;
            return Created(new Uri(Request.RequestUri + "/" + movie.Id), movieDto);
        }

        

        //PUT /api/movie/10
        [HttpPut]
        [Authorize(Roles = RoleName.CanManageMovies)]
        public void EditMovie(int id, MovieDto movieDto)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var movieInDb = _db.Movie.SingleOrDefault(m => m.Id == id);

            if (movieInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            Mapper.Map<MovieDto, Movie>(movieDto, movieInDb);
            _db.SaveChanges();
        }

        //DELETE /api/movie/10
        [HttpDelete]
        public void DeleteMovie(int id)
        {
            var movieInDb = _db.Movie.SingleOrDefault(m => m.Id == id);
            if (movieInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            _db.Movie.Remove(movieInDb);
            _db.SaveChanges();
        }

       
    }
}
