using MoviesApp.Domain.Interfaces;
using MoviesApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesApp.Domain.Services
{
    public class ListService : IListService
    {
        private readonly IListRepository _listRepository;

        public ListService(IListRepository listRepository)
        {
            _listRepository = listRepository;
        }
        public async Task<List> Add(List list, string username)
        {
            if (_listRepository.Search(b => b.Title == list.Title && b.UserId == username).Result.Any())
                return null;

            await _listRepository.Add(list);
            return list;
        }

        public void Dispose()
        {
            _listRepository?.Dispose();
        }

        public async Task<IEnumerable<List>> GetAll(string username)
        {
            return await _listRepository.GetAll(username);
        }

        public async Task<List> GetById(int id)
        {
            return await _listRepository.GetById(id);
        }

        public async Task<bool> Remove(List list)
        {
            await _listRepository.DeleteList(list);
            return true;
        }

        public async Task<List> Update(List list)
        {
            if (_listRepository.Search(b => b.Title == list.Title && b.Id != list.Id).Result.Any())
                return null;

            await _listRepository.Update(list);
            return list;
        }
    }
}
