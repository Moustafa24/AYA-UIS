using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Entities;
using Presistence.Data;

namespace Presistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {

        private readonly AYA_UIS_InfoDbContext _dbContext;
        //private Dictionary<string, object> _repositories; 
        private ConcurrentDictionary<string, object> _repositories;

        public UnitOfWork(AYA_UIS_InfoDbContext dbContext)
        {
            _dbContext = dbContext;
            _repositories = new();
        }

        
        public IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : BaseEntities<TKey>
        => (IGenericRepository<TEntity, TKey>)_repositories.GetOrAdd(typeof(TEntity).Name, (_) => new GenericRepository<TEntity, TKey>(_dbContext));
        public async Task<int> SaveChangeAsync()
        =>await _dbContext.SaveChangesAsync();

    }
}
