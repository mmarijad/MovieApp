using Microsoft.EntityFrameworkCore;
using MoviesApp.Domain.Interfaces;
using MoviesApp.Domain.Models;
using MoviesApp.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesApp.Infrastructure.Repositories
{
    public class MovieListRepository : Repository<ListMovie>, IMovieListRepository
    {
        public MovieListRepository(MoviesDatabaseContext context) : base(context) { }

        public async Task<List<ListMovie>> GetAllByList(int listId)
        {
            return await Db.MovieLists.AsNoTracking().Include(b => b.List).Include(b => b.Movie)
                .Where(b => b.ListId == listId)
               .ToListAsync();
        }

        public Task<ListMovie> GetByIds(int movieId, int listId)
        {
            throw new NotImplementedException();
        }

    }
}
