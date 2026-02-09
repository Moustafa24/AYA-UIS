using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using AYA_UIS.Core.Domain.Entities;
using Domain.Contracts;
using Microsoft.EntityFrameworkCore;
using Presistence.Data;

namespace Presistence.Repositories
{
    public class GenericRepository<TEntity, TKey>(AYA_UIS_InfoDbContext _dbContext)
        : IGenericRepository<TEntity, TKey> where TEntity : BaseEntities<TKey>
    {

        // Get All
        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            var result = _dbContext.Set<TEntity>().AsQueryable();
            return await result.AsNoTracking().ToListAsync();
        } 

        // Add
        public async Task AddAsync(TEntity entity)
        {
            await _dbContext.Set<TEntity>().AddAsync(entity);
        }

        // Update
        public void Update(TEntity entity)
        {
            _dbContext.Set<TEntity>().Update(entity);
        }

        // Delete
        public void Delete(TEntity entity)
        {
            _dbContext.Set<TEntity>().Remove(entity);
        }

        // Get By Id
        public async Task<TEntity?> GetByIdAsync(TKey id)
        {
            return await _dbContext.Set<TEntity>().FindAsync(id);
        }

    }
}