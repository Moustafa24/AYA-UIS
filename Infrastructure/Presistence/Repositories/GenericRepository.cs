using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Presistence.Data;

namespace Presistence.Repositories
{
    public class GenericRepository<TEntity, TKey>(AYA_UIS_InfoDbContext _dbContext)
        : IGenericRepository<TEntity, TKey> where TEntity : BaseEntities<TKey>
    {

        // Get All
        public async Task<IEnumerable<TEntity>> GetAllAsync(bool asNoTracking = false)
        => asNoTracking ? await _dbContext.Set<TEntity>().AsNoTracking().ToListAsync()
            : await _dbContext.Set<TEntity>().ToListAsync();


        // Add
        public async Task AddAsync(TEntity entity)
         => await _dbContext.Set<TEntity>().AddAsync(entity);


        // Update
        public void Update(TEntity entity)
       => _dbContext.Set<TEntity>().Update(entity);

        // Delete
        public void Delete(TEntity entity)
        => _dbContext.Set<TEntity>().Remove(entity);

        // Find 
        public async Task<TEntity?> FindAsync(Expression<Func<TEntity, bool>> predicate, bool asNoTracking = true)
        {
            IQueryable<TEntity> query = _dbContext.Set<TEntity>();

            if (asNoTracking)
                query = query.AsNoTracking();

            return await query.FirstOrDefaultAsync(predicate);
        }

       


        #region Specification

        public async Task<TEntity?> GetAsync(ISpecification<TEntity, TKey> spec)
        {
            IQueryable<TEntity> query = ApplySpecification(spec);
            return await query.FirstOrDefaultAsync();
        }

        // ===== Specification: List =====
        public async Task<IEnumerable<TEntity>> ListAsync(ISpecification<TEntity, TKey> spec)
        {
            IQueryable<TEntity> query = ApplySpecification(spec);
            return await query.ToListAsync();
        }

        // ===== Helper لتطبيق الـ Specification =====
        private IQueryable<TEntity> ApplySpecification(ISpecification<TEntity, TKey> spec)
        {
            IQueryable<TEntity> query = _dbContext.Set<TEntity>().AsQueryable();

            // Apply criteria
            if (spec.Criteria != null)
                query = query.Where(spec.Criteria);

            // Apply includes
            foreach (var include in spec.Includes)
                query = query.Include(include);

            return query;
        }

       
        #endregion

    }
}