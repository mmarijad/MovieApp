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
    public class ListRepository : Repository<List>, IListRepository
    {
        public ListRepository(MoviesDatabaseContext context) : base(context) { }

        public async Task<List<List>> GetAll(string username)
        {
            return await Db.Lists.AsNoTracking()
               .Where(m => m.UserId == username)
               .ToListAsync();
        }

        public async Task<IEnumerable<List>> Search(string searchedValue)
        {
            return await Db.Lists.AsNoTracking()
               .Where(m => m.Title.ToUpper().Contains(searchedValue.ToUpper()))
               .ToListAsync();
        }

        public async Task<bool> DeleteList(List list)
        {
            List listToDelete = Db.Lists.Where(m => m.Id == list.Id).Include(m => m.MovieLists).FirstOrDefault();
            Db.Lists.Remove(listToDelete);
            Db.SaveChanges();
            return true;
        }
    }
}
