using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        public DirectorsController(IMapper mapper, IDirectorService directorService)
        {
            _mapper = mapper;
            _directorService = directorService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var directors = await _directorService.GetAll();

            return Ok(_mapper.Map<IEnumerable<DirectorResultDto>>(directors));
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var director = await _directorService.GetById(id);

            if (director == null) 
                return NotFound();

            return Ok(_mapper.Map<DirectorResultDto>(director));
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Add(DirectorAddDto directorDto)
        {
            if (!ModelState.IsValid) return BadRequest();

            var director = _mapper.Map<Director>(directorDto);
            var directorResult = await _directorService.Add(director);

            if (directorResult == null) return BadRequest();

            return Ok(_mapper.Map<DirectorResultDto>(directorResult));
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(int id, DirectorUpdateDto directorDto)
        {
            if (id != directorDto.Id) 
                return BadRequest();

            if (!ModelState.IsValid) 
                return BadRequest();

            await _directorService.Update(_mapper.Map<Director>(directorDto));

            return Ok(directorDto);
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Remove(int id)
        {
            var director = await _directorService.GetById(id);
            if (director == null) 
                return NotFound();

            var result = await _directorService.Remove(director);

            if (!result) 
                return BadRequest();
            return Ok();
        }


        [HttpGet]
        [Route("search/{director}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<Director>>> Search(string director)
        {
            var directors = _mapper.Map<List<Director>>(await _directorService.Search(director));

            if (directors == null || directors.Count == 0)
                return NotFound("Redatelj nije pronađen.");

            return Ok(directors);
        }
    }
}
