using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MoviesApp.API.DTOs.Director;
using MoviesApp.Domain.Interfaces;
using MoviesApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesApp.API.Controllers
{
    [Route("api/[controller]")]
    public class DirectorsController : MainController
    {
        private readonly IDirectorService _directorService;
        private readonly IMapper _mapper;
        private readonly ILogger<DirectorsController> _logger;

        public DirectorsController(IMapper mapper, IDirectorService directorService, ILogger<DirectorsController> logger)
        {
            _mapper = mapper;
            _directorService = directorService;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<DirectorResultDto>>> GetAll()
        {
            try
            {
                var directors = await _directorService.GetAll();
                _logger.LogInformation("Get all directors succeded.");
                return Ok(_mapper.Map<IEnumerable<DirectorResultDto>>(directors));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in DirectorsController, error message: {0}, HResult: {1}", ex.Message, ex.HResult);
                return NotFound();
            }

        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<DirectorResultDto>> GetById(int id)
        {
            try
            {
                var director = await _directorService.GetById(id);
                if (director == null) return NotFound();
                _logger.LogInformation("Get director by id: {0} succeded.", id);
                return Ok(_mapper.Map<DirectorResultDto>(director));
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in DirectorsController, error message: {0}, HResult: {1}", ex.Message, ex.HResult);
                return NotFound();
            }
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Add(DirectorAddDto directorDto)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest();
                var director = _mapper.Map<Director>(directorDto);
                var directorResult = await _directorService.Add(director);
                if (directorResult == null) return BadRequest();

                _logger.LogInformation("Add director succeded.");
                return Ok(_mapper.Map<DirectorResultDto>(directorResult));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in DirectorsController, error message: {0}, HResult: {1}", ex.Message, ex.HResult);
                return NotFound();
            }

        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(int id, DirectorUpdateDto directorDto)
        {
            try
            {
                if (id != directorDto.Id) return BadRequest();
                if (!ModelState.IsValid) return BadRequest();

                await _directorService.Update(_mapper.Map<Director>(directorDto));
                _logger.LogInformation("Update director with id: {0} succeded.", id);
                return Ok(directorDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in DirectorsController, error message: {0}, HResult: {1}", ex.Message, ex.HResult);
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
                var director = await _directorService.GetById(id);
                if (director == null) return NotFound();
                var result = await _directorService.Remove(director);
                if (!result) return BadRequest();

                _logger.LogInformation("Delete director with id: {0} succeded.", id);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in DirectorsController, error message: {0}, HResult: {1}", ex.Message, ex.HResult);
                return NotFound();
            }
        }

        [HttpGet]
        [Route("get-directors-by-name/{Name}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<DirectorResultDto>> GetByName(string name)
        {
            try
            {
                var director = await _directorService.GetByName(name);
                if (director == null) return NotFound();
                _logger.LogInformation("Get director by name: {0} succeded.", name);
                return Ok(_mapper.Map<DirectorResultDto>(director));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in DirectorsController, error message: {0}, HResult: {1}", ex.Message, ex.HResult);
                return BadRequest();
            }
        }


        [HttpGet]
        [Route("search/{director}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<Director>>> Search(string director)
        {
            try
            {
                var directors = _mapper.Map<List<Director>>(await _directorService.Search(director));
                if (directors == null || directors.Count == 0) return NotFound("Redatelj nije pronađen.");

                _logger.LogInformation("Search directors succeded.");
                return Ok(directors);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in DirectorsController, error message: {0}, HResult: {1}", ex.Message, ex.HResult);
                return NotFound();
            }
        }
    }
}
