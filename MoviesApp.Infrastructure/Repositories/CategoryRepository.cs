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
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(MoviesDatabaseContext context) : base(context) { }

        public async Task<Category> GetByName(string Name)
        {
            return await Db.Categories.AsNoTracking().Where(c => c.Name.ToUpper() == Name.ToUpper()).FirstOrDefaultAsync();
        }
    }
}
