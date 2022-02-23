using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        public MoviesController(IMapper mapper, IMoviesService moviesService)
        {
            _mapper = mapper;
            _moviesService = moviesService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var movies = await _moviesService.GetAll();

            return Ok(_mapper.Map<IEnumerable<MovieResultDto>>(movies));
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var movie = await _moviesService.GetById(id);

            if (movie == null)
                return NotFound();
            return Ok(_mapper.Map<MovieResultDto>(movie));
        }

        [HttpGet]
        [Route("get-movies-by-category/{categoryId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetMoviesByCategory(int categoryId)
        {
            var movies = await _moviesService.GetMoviesByCategory(categoryId);

            if (!movies.Any()) 
                return NotFound();

            return Ok(_mapper.Map<IEnumerable<MovieResultDto>>(movies));
        }

        [HttpGet]
        [Route("get-movies-by-director/{directorId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetMoviesByDirector(int directorId)
        {
            var movies = await _moviesService.GetMoviesByDirector(directorId);

            if (!movies.Any())
                return NotFound();

            return Ok(_mapper.Map<IEnumerable<MovieResultDto>>(movies));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Add(MovieAddDto movieDto)
        {
            if (!ModelState.IsValid) return BadRequest();

            var movie = _mapper.Map<Movie>(movieDto);
            var movieResult = await _moviesService.Add(movie);

            if (movieResult == null) 
                return BadRequest();

            return Ok(_mapper.Map<MovieResultDto>(movieResult));
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(int id, MovieUpdateDto movieDto)
        {
            if (id != movieDto.Id) 
                return BadRequest();

            if (!ModelState.IsValid) 
                return BadRequest();

            await _moviesService.Update(_mapper.Map<Movie>(movieDto));
            return Ok(movieDto);
        }

        [HttpGet]
        [Route("search-movies-by-category-and-director/{searchedValue}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<Movie>>> SearchMoviesWithCategoryAndDirector(string searchedValue)
        {
            var movies = _mapper.Map<List<Movie>>(await _moviesService.SearchMoviesWithCategoryAndDirector(searchedValue));

            if (!movies.Any()) 
                return NotFound("Nije pronađen taj film.");

            return Ok(_mapper.Map<IEnumerable<MovieResultDto>>(movies));
        }
    }
}
