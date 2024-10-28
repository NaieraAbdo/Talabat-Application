﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Specifications;

namespace Talabat.Repository
{
    public static class SpecificationsEvaluator<T> where T : BaseEntity
    {
        //Func to build Query
        public static IQueryable<T> GetQuery(IQueryable<T> inputQuery,ISpecifications<T> spec)
        {
            var Query = inputQuery;
            if (spec.Criteria is not null)
                Query = Query.Where(spec.Criteria);
            Query = spec.Includes.Aggregate(Query, (CurrentQuery, IncludeExpression) => CurrentQuery.Include(IncludeExpression));
            return Query;
        }
    }
}