using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using PHDataAccessLayer.DataInterfaces;
using PHDataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PHDataAccessLayer.DataAccess
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity>
        where TEntity : class
    {
        private MessageDBContext _dbContext = null;

        public GenericRepository()
        {
            _dbContext = new MessageDBContext();
        }

        public async Task<IQueryable<TEntity>> GetAllAsync()
        {
            return await Task.Run(() => _dbContext.Set<TEntity>());
        }

        public async Task<TEntity> GetIdAsync(int id)
        {
            return await _dbContext.Set<TEntity>().FindAsync(id);
        }

        public async Task<TEntity> Create(TEntity entity)
        {
            await _dbContext.Set<TEntity>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task BulkInsert(List<TEntity> entity)
        {
           await _dbContext.BulkInsertAsync(entity);
        }

        public async Task<int> Update(int id, TEntity entity)
        {
            _dbContext.Set<TEntity>().Update(entity);
           return await _dbContext.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var entity = await GetIdAsync(id);
            _dbContext.Set<TEntity>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
