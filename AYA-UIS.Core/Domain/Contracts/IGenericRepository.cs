using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Contracts
{
    public interface IGenericRepository<TEntity, TKey> where TEntity : BaseEntities<TKey>
    {
        //GetAll
        Task<IEnumerable<TEntity>> GetAllAsync(bool asNoTracking = false);

        //Add
        Task AddAsync(TEntity entity);

        //Remove
        void Delete(TEntity entity);

        //Update
        void Update(TEntity entity);

        // Find 

        Task<TEntity?> FindAsync(Expression<Func<TEntity, bool>> predicate, bool asNoTracking = true);
        #region Specification


        Task<TEntity?> GetAsync(ISpecification<TEntity, TKey> spec);
        Task<IEnumerable<TEntity>> ListAsync(ISpecification<TEntity, TKey> spec);
    }

    #endregion
}

