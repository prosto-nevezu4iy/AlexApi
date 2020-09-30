using AlexApi.Domain.Models;
using AlexApi.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AlexApi.Repositories.Repositories
{
    public interface IUserRepository: IGenericRepository<User>
    {
    }
}
