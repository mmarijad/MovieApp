using MoviesApp.Domain.Interfaces;
using MoviesApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesApp.Domain.Services
{
    public class MovieListService : IMovieListService { 

        private readonly IMovieListRepository _movielistRepository;

        public MovieListService(IMovieListRepository movielistRepository)
        {
            _movielistRepository = movielistRepository;
        }
   
        public async Task<ListMovie> Add(ListMovie movieList)
        {
            if (_movielistRepository.Search(b => b.MovieId == movieList.MovieId && b.ListId == movieList.ListId).Result.Any())
                return null;

            await _movielistRepository.Add(movieList);
            return movieList;
        }

        public void Dispose()
        {
            _movielistRepository?.Dispose();
        }

        public async Task<IEnumerable<ListMovie>> GetAllByList(int listId)
        {
            return await _movielistRepository.GetAllByList(listId);
        }

        public async Task<ListMovie> GetByIds(int movieId, int listId)
        {
            return await _movielistRepository.GetByIds(movieId, listId);
        }

        public async Task<ListMovie> GetById(int id)
        {
            return await _movielistRepository.GetById(id);
        }

        public async Task<bool> Remove(ListMovie movielist)
        {
            await _movielistRepository.Remove(movielist);
            return true;
        }

        public async Task<ListMovie> Update(ListMovie movieList)
        {
            if (_movielistRepository.Search(b => b.MovieId == movieList.MovieId && b.ListId == movieList.ListId).Result.Any())
                return null;

            await _movielistRepository.Update(movieList);
            return movieList;
        }
    }
}
