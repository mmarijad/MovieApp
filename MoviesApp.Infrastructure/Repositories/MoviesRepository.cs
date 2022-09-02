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
    public class MoviesRepository : Repository<Movie>, IMoviesRepository
    {
        public MoviesRepository(MoviesDatabaseContext context) : base(context) { }


        public async Task<IEnumerable<Movie>> GetMoviesByCategory(int categoryId)
        {
            return await Search(m => m.CategoryId == categoryId);
        }

        public async Task<IEnumerable<Movie>> GetMoviesByDirector(int directorId)
        {
            return await Search(m => m.DirectorId == directorId);
        }

        public async Task<IEnumerable<Movie>> SearchMoviesWithCategoryAndDirector(string searchedValue)
        {
            return await Db.Movies.AsNoTracking()
               .Include(m => m.Category).Include(m => m.Director)
               .Where(m => m.Name.ToUpper().Contains(searchedValue.ToUpper()) ||
                           m.Director.Name.ToUpper().Contains(searchedValue.ToUpper()) ||
                           m.Director.LastName.ToUpper().Contains(searchedValue.ToUpper()) ||
                           m.Description.ToUpper().Contains(searchedValue.ToUpper()) ||
                           m.Year.ToUpper().Contains(searchedValue.ToUpper()) ||
                           m.Category.Name.ToUpper().Contains(searchedValue.ToUpper()))
               .ToListAsync();
        }

        public override async Task<List<Movie>> GetAll()
        {
            return await Db.Movies.AsNoTracking().Include(b => b.Category).Include(b => b.Director)
                .OrderBy(b => b.Name)
                .ToListAsync();
        }

        public override async Task<Movie> GetById(int id)
        {
            return await Db.Movies.AsNoTracking().Include(b => b.Category).Include(b => b.Director)
                .Where(b => b.Id == id)
                .FirstOrDefaultAsync();
        }
    }
}
