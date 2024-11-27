using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Entities.OrderAggregate;

namespace Talabat.Repository.Data.DataSeed
{
    public static class StoreContextSeed
    {
        public async static Task SeedAsync(StoreContext _dbContext)
        {
            
            if (!_dbContext.ProductBrands.Any())
            {
                var brandsData = File.ReadAllText("../Talabat.repository/Data/DataSeed/brands.json");
                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);
                if (brands?.Count > 0)
                {
                    foreach (var brand in brands)
                    {
                        await _dbContext.Set<ProductBrand>().AddAsync(brand);
                    }
                }
            }            

            if (!_dbContext.ProductCategories.Any())
            {
                var categoriesData = File.ReadAllText("../Talabat.repository/Data/DataSeed/categories.json");
                var categories = JsonSerializer.Deserialize<List<ProductCategory>>(categoriesData);
                if (categories?.Count > 0)
                {
                    foreach (var cat in categories)
                        await _dbContext.Set<ProductCategory>().AddAsync(cat);
                    
                }
            }
           
            if (!_dbContext.Products.Any())
            {
                var productsData = File.ReadAllText("../Talabat.repository/Data/DataSeed/products.json");
                var products = JsonSerializer.Deserialize<List<Product>>(productsData);
                if (products?.Count > 0)
                {
                    foreach (var product in products)
                    {
                        await _dbContext.Set<Product>().AddAsync(product);
                    }
                }
            }


            if (!_dbContext.DeliveryMethods.Any())
            {
                var DeliveryMethodData = File.ReadAllText("../Talabat.repository/Data/DataSeed/delivery.json");
                var DeliveryMethods = JsonSerializer.Deserialize<List<DeliveryMethod>>(DeliveryMethodData);
                if (DeliveryMethods?.Count > 0)
                {
                    foreach (var DeliveryMethod in DeliveryMethods)
                    {
                       await _dbContext.Set<DeliveryMethod>().AddAsync(DeliveryMethod);
                    }
                }
            }

            await _dbContext.SaveChangesAsync();

        }
    }
}
