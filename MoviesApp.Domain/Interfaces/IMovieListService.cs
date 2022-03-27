using MoviesApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MoviesApp.Domain.Interfaces
{
    public interface IMovieListService : IDisposable
    {
        Task<IEnumerable<ListMovie>> GetAllByList(int listId);
        Task<ListMovie> GetByIds(int movieId, int listId);
        Task<ListMovie> GetById(int id);
        Task<ListMovie> Add(ListMovie movieList);
        Task<ListMovie> Update(ListMovie movieList);
        Task<bool> Remove(ListMovie movielist);
    }
}
