﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specifications
{
    public class ProductWithFiltrationForCountAsync:BaseSpecifications<Product>
    {
        public ProductWithFiltrationForCountAsync(ProductSpecParams Params):base(
            p => (!Params.BrandId.HasValue || p.BrandId == Params.BrandId)
            &&
            (!Params.TypeId.HasValue || p.CategoryId == Params.TypeId)
            )
        {
            
        }
    }
}
