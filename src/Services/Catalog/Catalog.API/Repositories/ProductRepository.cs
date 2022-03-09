using Catalog.API.Data;
using Catalog.API.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catalog.API.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ICatalogContext _catalogContext;
        public ProductRepository(ICatalogContext context)
        {
            _catalogContext= context?? throw new ArgumentNullException(nameof(context));
        }
        public async Task CreateProduct(Product product)
        {
             await _catalogContext.Products.InsertOneAsync(product);
        }

        public async Task<bool> DeleteProduct(string id)
        {
            var result = await _catalogContext.
                Products
                .DeleteOneAsync(filter:t=>t.Id==id);
            return result.IsAcknowledged && result.DeletedCount > 0;
        }

        public async Task<IEnumerable<Product>> GetProductByCategory(string categoryName)
        {
            FilterDefinition<Product> filterDefinition = Builders<Product>.Filter.Eq(t => t.Category, categoryName);
            return await
                _catalogContext.
                 Products
                 .Find(filterDefinition)
                 .ToListAsync();
        }

        public async Task<Product> GetProductById(string id)
        {
            return await 
                _catalogContext.
                 Products
                 .Find(p => p.Id==id)
                 .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Product>> GetProductByName(string name)
        {
            return await
                _catalogContext.
                 Products
                 .Find(p => p.Name == name)
                 .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await _catalogContext.
                 Products
                 .Find(p => true)
                 .ToListAsync();
        }

        public async Task<bool> UpdateProduct(Product product)
        {
          var result=  await _catalogContext.
                Products
                .ReplaceOneAsync(filter:t => t.Id == product.Id,replacement: product);
            return result.IsAcknowledged && result.ModifiedCount > 0;
        }
    }
}
