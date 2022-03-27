using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MoviesApp.API.DTOs.List;
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
    public class ListController : ControllerBase
    {
        private readonly IListService _listService;
        private readonly IMapper _mapper;
        private readonly ILogger<ListController> _logger;

        public ListController(IMapper mapper, IListService listService, ILogger<ListController> logger)
        {
            _mapper = mapper;
            _listService = listService;
            _logger = logger;
        }


        [HttpGet("{username:alpha}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ListResultDto>>> GetAll(string username)
        {
            try
            {
                var lists = await _listService.GetAll(username);

                _logger.LogInformation("Get all users lists succeded.");
                return Ok(_mapper.Map<IEnumerable<ListResultDto>>(lists));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in ListController, error message: {0}, HResult: {1}", ex.Message, ex.HResult);
                return NotFound();
            }
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ListResultDto>> GetById(int id)
        {
            try
            {
                var list = await _listService.GetById(id);
                if (list == null) return NotFound();

                _logger.LogInformation("Get list by id: {0} succeded.", id);
                return Ok(_mapper.Map<ListResultDto>(list));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in ListController, error message: {0}, HResult: {1}", ex.Message, ex.HResult);
                return NotFound();
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Add(ListAddDto listDto, string userId)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest();
                var list = _mapper.Map<List>(listDto);
                var listResult = await _listService.Add(list, userId);
                if (listResult == null) return BadRequest();

                _logger.LogInformation("Add list succeded.");
                return Ok(_mapper.Map<ListResultDto>(listResult));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in ListController, error message: {0}, HResult: {1}", ex.Message, ex.HResult);
                return NotFound();
            }
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(int id, ListUpdateDto listDto)
        {
            try
            {
                if (id != listDto.Id) return BadRequest();
                if (!ModelState.IsValid) return BadRequest();

                await _listService.Update(_mapper.Map<List>(listDto));
                _logger.LogInformation("Update list with id: {0} succeded.", id);
                return Ok(listDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in ListController, error message: {0}, HResult: {1}", ex.Message, ex.HResult);
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
                var list = await _listService.GetById(id);
                if (list == null) return NotFound();

                await _listService.Remove(list);

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
