using AlexApi.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AlexApi.Repositories.Abstract
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetAsync(string userName, string password);
        Task<int> AddAsync(T entity);
    }
}
