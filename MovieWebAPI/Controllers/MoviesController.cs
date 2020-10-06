using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MovieWebAPI.Data;
using MovieWebAPI.Models;

namespace MovieWebAPI.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class MoviesController: ControllerBase
    {
        private readonly MovieContext movieContext;
        public MoviesController (MovieContext _movieContext)
        {
            movieContext = _movieContext;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Movie>> GetAll ()
        {
            var movies = movieContext.Movies;
            return movies;
        }

        [HttpGet("{id}")]
        public ActionResult<Movie> GetById(int id)
        {
            var movie = movieContext.Movies.FirstOrDefault( x => x.Id == id);
            if (movie == null)
                return NotFound();
            return movie;
        }

        [HttpPost]
        public ActionResult<Movie> Insert([FromBody] Movie movie)
        {
            var newMovie = new Movie()
            {
                Name = movie.Name,
                Link  =movie.Link,
                Category = movie.Category,
                Rating = movie.Rating
            };

            var savedMovie = movieContext.Movies.Add(newMovie).Entity;
            movieContext.SaveChanges();
            return savedMovie;
        }

        [HttpPut("{id}")]
        public ActionResult<Movie> Update(int id, [FromBody] Movie movie)
        {
            var existingMovie = movieContext.Movies.FirstOrDefault(x => x.Id == id);
            if (existingMovie == null)
                return NotFound();

            existingMovie.Name = movie.Name;
            existingMovie.Link = movie.Link;
            existingMovie.Category = movie.Category;
            existingMovie.Rating = movie.Rating;

            var savedMovie = movieContext.Movies.Update(existingMovie).Entity;
            movieContext.SaveChanges();
            return savedMovie;
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var existingMovie = movieContext.Movies.FirstOrDefault(x => x.Id == id);
            if (existingMovie == null)
                return NotFound();

            movieContext.Movies.Remove(existingMovie);
            movieContext.SaveChanges();
            return NoContent();
        }
    }
}
