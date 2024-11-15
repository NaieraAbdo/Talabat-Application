using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specifications
{
    public class BaseSpecifications<T> : ISpecifications<T> where T : BaseEntity
    {
        public Expression<Func<T, bool>> Criteria { get ; set ; }
        public List<Expression<Func<T, object>>> Includes { get; set; } = new List<Expression<Func<T, object>>>();
        public Expression<Func<T, object>> OrderBy { get ; set ; }
        public Expression<Func<T, object>> OrderByDesc { get ; set; }
        public int Skip { get ; set ; }
        public int Take { get; set ; }
        public bool IsPaginationEnabled { get ; set ; }

        public BaseSpecifications()
        {
            //Includes = new List<Expression<Func<T, object>>> ();
        }

        public BaseSpecifications(Expression<Func<T,bool>> CriteriaExp)
        {
            Criteria = CriteriaExp;
            //Includes = new List<Expression<Func<T, object>>>();

        }
        //Func for OrderBy
        public void AddOrderBy(Expression<Func<T,object>> orderExpr)
        {
            OrderBy = orderExpr;
        }
        //Func for OrderByDesc
        public void AddOrderByDesc(Expression<Func<T, object>> orderExpr)
        {
            OrderBy = orderExpr;
        }
        //Pagination
        
        public void ApplyPagination(int skip,int take)
        {
            IsPaginationEnabled = true;
            Skip = skip; Take = take; 

        }
    }
}
