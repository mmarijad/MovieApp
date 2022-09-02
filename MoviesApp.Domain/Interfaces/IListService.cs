using MoviesApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MoviesApp.Domain.Interfaces
{
    public interface IListService: IDisposable
    {
        Task<IEnumerable<List>> GetAll(string username);
        Task<List> GetById(int id);
        Task<List> Add(List list, string username);
        Task<List> Update(List list);
        Task<bool> Remove(List list);
    }
}
