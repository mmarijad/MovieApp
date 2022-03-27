using MoviesApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MoviesApp.Domain.Interfaces
{
    public interface IMovieListRepository : IRepository<ListMovie>
    {
        Task<ListMovie> GetByIds(int movieId, int listId);
        Task<List<ListMovie>> GetAllByList(int listId);
    }
}
