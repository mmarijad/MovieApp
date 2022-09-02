using MoviesApp.Domain.Interfaces;
using MoviesApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesApp.Domain.Services
{
    public class DirectorService : IDirectorService
    {
        private readonly IDirectorRepository _directorRepository;
        private readonly IMoviesService _moviesService;

        public DirectorService(IDirectorRepository directorRepository, IMoviesService moviesService)
        {
            _directorRepository = directorRepository;
            _moviesService = moviesService;
        }

        public async Task<Director> Add(Director director)
        {
            if (_directorRepository.Search(c => c.Name == director.Name || c.LastName == director.LastName).Result.Any())
                return null;

            await _directorRepository.Add(director);
            return director;
        }

        public void Dispose()
        {
            _directorRepository.Dispose();
        }

        public async Task<IEnumerable<Director>> GetAll()
        {
            return await _directorRepository.GetAll();
        }

        public async Task<Director> GetById(int id)
        {
            return await _directorRepository.GetById(id);
        }

        public async Task<bool> Remove(Director director)
        {
            var directors = await _moviesService.GetMoviesByCategory(director.Id);
            if (directors.Any())
                return false;

            await _directorRepository.Remove(director);
            return true;
        }

        public async Task<IEnumerable<Director>> Search(string directorName)
        {
            return await _directorRepository.Search(c => c.Name.ToUpper().Contains(directorName.ToUpper()) || c.LastName.ToUpper().Contains(directorName.ToUpper()));
        }

        public async Task<Director> Update(Director director)
        {
            if (_directorRepository.Search(c => c.Name == director.Name && c.LastName == director.LastName && c.Id != director.Id).Result.Any())
                return null;

            await _directorRepository.Update(director);
            return director;
        }

        public async Task<Director> GetByName(string name)
        {
            return await _directorRepository.GetByName(name);
        }
    }
}
