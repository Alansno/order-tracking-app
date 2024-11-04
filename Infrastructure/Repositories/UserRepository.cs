using Domain.Entities;
using Infrastructure.Custom.ResultPattern;
using Infrastructure.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class UserRepository : IRepository<UserEntity>
    {
        
        public Task<Result<bool>> Delete(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<Result<UserEntity>> FindById(int Id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<UserEntity> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Result<UserEntity>> Save(UserEntity model)
        {
            throw new NotImplementedException();
        }

        public Task<Result<bool>> SoftDelete(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<Result<bool>> Update(UserEntity model)
        {
            throw new NotImplementedException();
        }
    }
}
