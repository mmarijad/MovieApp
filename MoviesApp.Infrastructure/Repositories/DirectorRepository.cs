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
    public class DirectorRepository : Repository<Director>, IDirectorRepository
    {
        public DirectorRepository(MoviesDatabaseContext context) : base(context) { }

        public async Task<Director> GetByName(string name)
        {
            return await Db.Directors.AsNoTracking().Where(m => m.Name.ToUpper() == name.ToUpper()).FirstOrDefaultAsync();
        }
    }
}
