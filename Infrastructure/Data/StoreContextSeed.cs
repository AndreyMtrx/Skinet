using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Infrastructure.Data
{
    public class StoreContextSeed
    {
        public static async Task SeedAsync(StoreContext context, ILoggerFactory loggerFactory)
        {
            try
            {
                if (!context.ProductBrands.Any())
                {
                    var brandsData = await File.ReadAllTextAsync("../Infrastructure/Data/SeedData/brands.json");
                    var brands = JsonConvert.DeserializeObject<List<ProductBrand>>(brandsData);

                    foreach (var item in brands)
                    {
                        await context.ProductBrands.AddAsync(item);
                    }

                    await context.SaveChangesAsync();
                }

                if (!context.ProductTypes.Any())
                {
                    var typesData = await File.ReadAllTextAsync("../Infrastructure/Data/SeedData/types.json");
                    var types = JsonConvert.DeserializeObject<List<ProductType>>(typesData);

                    foreach (var item in types)
                    {
                        await context.ProductTypes.AddAsync(item);
                    }

                    await context.SaveChangesAsync();
                }

                if (!context.Products.Any())
                {
                    var productsData = await File.ReadAllTextAsync("../Infrastructure/Data/SeedData/products.json");
                    var products = JsonConvert.DeserializeObject<List<Product>>(productsData);

                    foreach (var item in products)
                    {
                        await context.Products.AddAsync(item);
                    }

                    await context.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {
                var logger = loggerFactory.CreateLogger<StoreContextSeed>();
                logger.LogError(e.Message);
            }
        }
    }
}
