using Infrastructure.Custom.ResultPattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.IRepositories
{
    public interface IRepository<M> where M : class
    {
        Task<Result<M>> Save(M model);
        Task<Result<bool>> Update(M model);
        Task<Result<bool>> Delete(int id);
        Task<Result<bool>> SoftDelete(int id);
        Task<Result<M>> FindById(int id);
        IQueryable<M> GetAll();
    }
}
