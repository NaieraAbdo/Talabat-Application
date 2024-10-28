﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specifications
{
    public class ProductWithBrandAndCategorySpecifications:BaseSpecifications<Product>
    {
        public ProductWithBrandAndCategorySpecifications():base()
        {
            Includes.Add(p => p.Category);
            Includes.Add(p => p.Brand);
        }

        public ProductWithBrandAndCategorySpecifications(int id) : base(p =>p.Id == id)
        {
            Includes.Add(p => p.Category);
            Includes.Add(p => p.Brand);
        }
    }
}
