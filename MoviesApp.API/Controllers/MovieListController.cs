using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MoviesApp.API.DTOs.MovieList;
using MoviesApp.Domain.Interfaces;
using MoviesApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieListController : MainController
    {
        private readonly IMovieListService _movielistService;
        private readonly IMapper _mapper;
        private readonly ILogger<MovieListController> _logger;

        public MovieListController(IMapper mapper, IMovieListService movielistService, ILogger<MovieListController> logger)
        {
            _mapper = mapper;
            _movielistService = movielistService;
            _logger = logger;
        }

        [HttpGet("{listId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<MovieListResultDto>>> GetAll(int listId)
        {
            try
            {
                var movielists = await _movielistService.GetAllByList(listId);

                _logger.LogInformation("Get all movie-lists succeded.");
                return Ok(_mapper.Map<IEnumerable<MovieListResultDto>>(movielists));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in MovieListController, error message: {0}, HResult: {1}", ex.Message, ex.HResult);
                return NotFound();
            }
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Add(MovieListAddDto movielistDto)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest();
                var movielist = _mapper.Map<ListMovie>(movielistDto);
                var movielistResult = await _movielistService.Add(movielist);
                if (movielistResult == null) return BadRequest();

                _logger.LogInformation("Add movielist succeded.");
                return Ok(_mapper.Map<MovieListResultDto>(movielistResult));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in MovieListController, error message: {0}, HResult: {1}", ex.Message, ex.HResult);
                return NotFound();
            }
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Remove(int id)
        {
            try
            {
                var listMovie = await _movielistService.GetById(id);
                if (listMovie == null) return NotFound();

                await _movielistService.Remove(listMovie);

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in ListController, error message: {0}, HResult: {1}", ex.Message, ex.HResult);
                return NotFound();
            }
        }
    }
}
