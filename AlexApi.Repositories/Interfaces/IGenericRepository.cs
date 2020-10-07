using AlexApi.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AlexApi.Repositories.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetAsync(T entity, string clientId);
        Task<int> AddAsync(T entity, string clientId);
    }
}
