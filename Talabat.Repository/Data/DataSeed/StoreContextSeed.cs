using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Repository.Data.DataSeed
{
    public static class StoreContextSeed
    {
        public async static Task SeedAsync(StoreContext _dbContext)
        {
            var brandsData = File.ReadAllText("../Talabat.repository/Data/DataSeed/brands.json");
            var brands =JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);
            if (!_dbContext.ProductBrands.Any())
            {
                if (brands?.Count() > 0)
                {
                    foreach (var brand in brands)
                    {
                        _dbContext.Set<ProductBrand>().Add(brand);
                    }
                }
                await _dbContext.SaveChangesAsync(); 
            }

            var categoriesData = File.ReadAllText("../Talabat.repository/Data/DataSeed/categories.json");
            var categories = JsonSerializer.Deserialize<List<ProductCategory>>(categoriesData);
            if (!_dbContext.ProductCategories.Any())
            {
                if (categories?.Count() > 0)
                {
                    foreach (var cat in categories)
                    {
                        _dbContext.Set<ProductCategory>().Add(cat);
                    }
                }
                await _dbContext.SaveChangesAsync();
            }

            var productsData = File.ReadAllText("../Talabat.repository/Data/DataSeed/products.json");
            var products= JsonSerializer.Deserialize<List<Product>>(productsData);
            if (!_dbContext.Products.Any())
            {
                if (products?.Count() > 0)
                {
                    foreach (var product in products)
                    {
                        _dbContext.Set<Product>().Add(product);
                    }
                }
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
