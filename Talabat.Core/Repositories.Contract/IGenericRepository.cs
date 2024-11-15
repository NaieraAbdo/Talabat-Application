using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Specifications;

namespace Talabat.Core.Repositories.Contract
{
    public interface IGenericRepository<T> where T:BaseEntity
    {
        #region Before Specification design
        Task<T?> GetAsync(int id);
        Task<IReadOnlyList<T>> GetAllAsync();
        #endregion

        #region With Specification design
        Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecifications<T> spec);
        Task<T> GetByIdWithSpecAsync(ISpecifications<T> spec);
        #endregion

        Task<int> GetCountWithSpecAsync(ISpecifications<T> spec);
    }
}
