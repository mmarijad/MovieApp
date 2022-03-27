using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MoviesApp.API.DTOs.Category;
using MoviesApp.Domain.Interfaces;
using MoviesApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesApp.API.Controllers
{
    [Route("api/[controller]")]
    public class CategoriesController : MainController
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;
        private readonly ILogger<CategoriesController> _logger;

        public CategoriesController(IMapper mapper, ICategoryService categoryService, ILogger<CategoriesController> logger)
        {
            _mapper = mapper;
            _categoryService = categoryService;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<CategoryResultDto>>> GetAll()
        {
            try
            {
                var categories = await _categoryService.GetAll();
                _logger.LogInformation("Get all categories succeeded.");
                return Ok(_mapper.Map<IEnumerable<CategoryResultDto>>(categories));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in CategoriesController, error message: {0}, HResult: {1}", ex.Message, ex.HResult);
                return NotFound();
            }
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CategoryResultDto>> GetById(int id)
        {
            try
            {
                var category = await _categoryService.GetById(id);
                if (category == null)
                    return NotFound();
                _logger.LogInformation("Get category by id: {0} succeeded.", id);
                return Ok(_mapper.Map<CategoryResultDto>(category));

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in CategoriesController, error message: {0}, HResult: {1}.", ex.Message, ex.HResult);
                return NotFound();
            }

        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Add(CategoryAddDto categoryDto)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest();

                var category = _mapper.Map<Category>(categoryDto);
                var categoryResult = await _categoryService.Add(category);

                if (categoryResult == null) return BadRequest();
                _logger.LogInformation("Add category succeeded.");
                return Ok(_mapper.Map<CategoryResultDto>(categoryResult));

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in CategoriesController, error message: {0}, HResult: {1}", ex.Message, ex.HResult);
                return NotFound();
            }
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(int id, CategoryUpdateDto categoryDto)
        {
            try
            {
                if (id != categoryDto.Id) return BadRequest();
                if (!ModelState.IsValid) return BadRequest();

                await _categoryService.Update(_mapper.Map<Category>(categoryDto));
                _logger.LogInformation("Update category with id: {0} succeeded.", id);
                return Ok(categoryDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in CategoriesController, error message: {0}, HResult: {1}", ex.Message, ex.HResult);
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
                var category = await _categoryService.GetById(id);
                if (category == null) return NotFound();

                var result = await _categoryService.Remove(category);
                if (!result) return BadRequest();
                _logger.LogInformation("Delete category with id: {0} succeeded.", id);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in CategoriesController, error message: {0}, HResult: {1}", ex.Message, ex.HResult);
                return NotFound();
            }
        }

        [HttpGet]
        [Route("get-category-by-name/{Name}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CategoryResultDto>> GetByName(string Name)
        {
            try
            {
                var category = await _categoryService.GetByName(Name);
                if (category == null) return NotFound();
                _logger.LogInformation("Get category by name: {0} succeded.", Name);
                return Ok(_mapper.Map<CategoryResultDto>(category));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in CategoryController, error message: {0}, HResult: {1}", ex.Message, ex.HResult);
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("search/{category}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<Category>>> Search(string category)
        {
            try
            {
                var categories = _mapper.Map<List<Category>>(await _categoryService.Search(category));
                if (categories == null || categories.Count == 0) return NotFound("Kategorija nije pronađena.");
                _logger.LogInformation("Search categories succeeded.");
                return Ok(categories);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in CategoriesController, error message: {0}, HResult: {1}", ex.Message, ex.HResult);
                return NotFound();
            }
        }
    }
}
