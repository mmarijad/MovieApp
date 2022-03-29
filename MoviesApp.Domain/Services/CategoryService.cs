using MoviesApp.Domain.Interfaces;
using MoviesApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesApp.Domain.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMoviesService _moviesService;

        public CategoryService(ICategoryRepository categoryRepository, IMoviesService moviesService)
        {
            _categoryRepository = categoryRepository;
            _moviesService = moviesService;
        }

        public async Task<IEnumerable<Category>> GetAll()
        {
            return await _categoryRepository.GetAll();
        }

        public async Task<Category> GetById(int id)
        {
            return await _categoryRepository.GetById(id);
        }

        public async Task<Category> Add(Category category)
        {
            if (_categoryRepository.Search(c => c.Name == category.Name).Result.Any())
                return null;

            await _categoryRepository.Add(category);
            return category;
        }

        public async Task<Category> Update(Category category)
        {
            if (_categoryRepository.Search(c => c.Name == category.Name && c.Id != category.Id).Result.Any())
                return null;

            await _categoryRepository.Update(category);
            return category;
        }

        public async Task<bool> Remove(Category category)
        {
            var categories = await _moviesService.GetMoviesByCategory(category.Id);
            if (categories.Any()) 
                return false;

            await _categoryRepository.Remove(category);
            return true;
        }

        public async Task<IEnumerable<Category>> Search(string categoryName)
        {
            return await _categoryRepository.Search(c => c.Name.ToUpper().Contains(categoryName.ToUpper()));
        }

        public void Dispose()
        {
            _categoryRepository?.Dispose();
        }

        public async Task<Category> GetByName(string name)
        {
            return await _categoryRepository.GetByName(name);
        }
    }
}
