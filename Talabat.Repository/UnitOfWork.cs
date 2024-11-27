using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core;
using Talabat.Core.Entities;
using Talabat.Core.Repositories.Contract;
using Talabat.Repository.Data;

namespace Talabat.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreContext dbContext;
        private Hashtable Repositories;

        public UnitOfWork(StoreContext dbContext)
        {
            this.dbContext = dbContext;
            Repositories = new Hashtable();
        }
        public async Task<int> CompleteAsync()
        {
            return await dbContext.SaveChangesAsync();
        }

        public async ValueTask DisposeAsync()
        => await dbContext.DisposeAsync();
        

        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
        {
            var type = typeof(TEntity).Name;
            if (!Repositories.ContainsKey(type))
            {
                var repo = new GenericRepository<TEntity>(dbContext);
                Repositories.Add(type, repo);
            }return Repositories[type] as IGenericRepository<TEntity>;
        }
    }
}
