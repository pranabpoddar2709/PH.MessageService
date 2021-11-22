using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PHDataAccessLayer.DataInterfaces
{
    public interface IGenericRepository<TEntity>
   where TEntity : class
    {
        Task<IQueryable<TEntity>> GetAllAsync();
        Task<TEntity> GetIdAsync(int id);
        Task<TEntity> Create(TEntity entity);

        Task<int> Update(int id, TEntity entity);

        Task Delete(int id);
        Task BulkInsert(List<TEntity> entity);
    }
}
