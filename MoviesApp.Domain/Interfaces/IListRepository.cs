using MoviesApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MoviesApp.Domain.Interfaces
{
    public interface IListRepository : IRepository<List>
    {
        Task<List<List>> GetAll(string username);
        new Task<List> GetById(int id);
        Task<IEnumerable<List>> Search(string searchedValue);
        Task<bool> DeleteList(List list);
    }
}
