using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Repositories.Contract;
using Talabat.Core.Specifications;
using Talabat.Repository.Data;

namespace Talabat.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly StoreContext dbContext;

        public GenericRepository(StoreContext dbContext)
        {
            this.dbContext = dbContext;
        }

        #region Before Specification
        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            if (typeof(T) == typeof(Product))
                return (IReadOnlyList<T>)await dbContext.Set<Product>().Include(p => p.Brand).Include(p => p.Category).ToListAsync();
            return await dbContext.Set<T>().ToListAsync();
        }


        public async Task<T?> GetAsync(int id)
        {
            if (typeof(T) == typeof(Product))
                return await dbContext.Set<Product>().Where(p => p.Id == id).Include(p => p.Brand).Include(p => p.Category).FirstOrDefaultAsync() as T;
            return await dbContext.Set<T>().FindAsync(id);
        }
        #endregion

        #region After Specification
        public async Task<T> GetByIdWithSpecAsync(ISpecifications<T> spec)
        {
            return await ApplySpec(spec).FirstOrDefaultAsync();
        }

        public async Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecifications<T> spec)
        {
            return await ApplySpec(spec).ToListAsync();
        }

        private  IQueryable<T> ApplySpec(ISpecifications<T> spec)
        {
            return  SpecificationsEvaluator<T>.GetQuery(dbContext.Set<T>(), spec);
        }

        public Task<int> GetCountWithSpecAsync(ISpecifications<T> spec)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
