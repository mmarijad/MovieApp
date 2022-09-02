using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MoviesApp.API.DTOs.Movie;
using MoviesApp.Domain.Interfaces;
using MoviesApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesApp.API.Controllers
{
    [Route("api/[controller]")]
    public class MoviesController : MainController
    {
        private readonly IMoviesService _moviesService;
        private readonly IMapper _mapper;
        private readonly ILogger<MoviesController> _logger;

        public MoviesController(IMapper mapper, IMoviesService moviesService, ILogger<MoviesController> logger)
        {
            _mapper = mapper;
            _moviesService = moviesService;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var movies = await _moviesService.GetAll();

                _logger.LogInformation("Get all movies succeded.");
                return Ok(_mapper.Map<IEnumerable<MovieResultDto>>(movies));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in MoviesController, error message: {0}, HResult: {1}", ex.Message, ex.HResult);
                return NotFound();
            }
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var movie = await _moviesService.GetById(id);
                if (movie == null) return NotFound();

                _logger.LogInformation("Get movie by id: {0} succeded.", id);
                return Ok(_mapper.Map<MovieResultDto>(movie));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in MoviesController, error message: {0}, HResult: {1}", ex.Message, ex.HResult);
                return NotFound();
            }
        }

        [HttpGet]
        [Route("get-movies-by-category/{categoryId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetMoviesByCategory(int categoryId)
        {
            try
            {
                var movies = await _moviesService.GetMoviesByCategory(categoryId);
                if (!movies.Any()) return NotFound();

                _logger.LogInformation("Get movies by category: {0} succeded.", categoryId);
                return Ok(_mapper.Map<IEnumerable<MovieResultDto>>(movies));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in MoviesController, error message: {0}, HResult: {1}", ex.Message, ex.HResult);
                return NotFound();
            }
        }

        [HttpGet]
        [Route("get-movies-by-director/{directorId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetMoviesByDirector(int directorId)
        {
            try
            {
                var movies = await _moviesService.GetMoviesByDirector(directorId);
                if (!movies.Any()) return NotFound();

                _logger.LogInformation("Get movies by director: {0} succeded.", directorId);
                return Ok(_mapper.Map<IEnumerable<MovieResultDto>>(movies));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in MoviesController, error message: {0}, HResult: {1}", ex.Message, ex.HResult);
                return NotFound();
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Add(MovieAddDto movieDto)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest();
                var movie = _mapper.Map<Movie>(movieDto);
                var movieResult = await _moviesService.Add(movie);
                if (movieResult == null) return BadRequest();

                _logger.LogInformation("Add movie succeded.");
                return Ok(_mapper.Map<MovieResultDto>(movieResult));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in MoviesController, error message: {0}, HResult: {1}", ex.Message, ex.HResult);
                return NotFound();
            }
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(int id, MovieUpdateDto movieDto)
        {
            try
            {
                if (id != movieDto.Id) return BadRequest();
                if (!ModelState.IsValid) return BadRequest();

                await _moviesService.Update(_mapper.Map<Movie>(movieDto));
                _logger.LogInformation("Update movie with id: {0} succeded.", id);
                return Ok(movieDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in MoviesController, error message: {0}, HResult: {1}", ex.Message, ex.HResult);
                return NotFound();
            }
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Remove(int id)
        {
            try { 
            var movie = await _moviesService.GetById(id);
            if (movie == null) return NotFound();

            await _moviesService.Remove(movie);

            return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in MoviesController, error message: {0}, HResult: {1}", ex.Message, ex.HResult);
                return NotFound();
            }
        }

        [HttpGet]
        [Route("search-movies-with-category-and-director/{searchedValue}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<Movie>>> SearchMoviesWithCategoryAndDirector(string searchedValue)
        {
            try
            {
                var movies = _mapper.Map<List<Movie>>(await _moviesService.SearchMoviesWithCategoryAndDirector(searchedValue));
                if (!movies.Any()) return NotFound("Nije pronađen taj film.");
                _logger.LogInformation("Search movies succeded.");
                return Ok(_mapper.Map<IEnumerable<MovieResultDto>>(movies));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in MoviesController, error message: {0}, HResult: {1}", ex.Message, ex.HResult);
                return NotFound();
            }
        }
    }
}
