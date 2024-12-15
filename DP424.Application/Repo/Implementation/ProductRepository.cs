using DP424.Application.Repo.Abstract;
using DP424.Domain.Models;
using DP424.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace DP424.Application.Repo.Implementation
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext context;

        public ProductRepository(AppDbContext context)
        {
            this.context = context;
        }

        public async Task Create(Product entity)
        {
           context.Products.Add(entity);
            await context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var product = await context.Products.FirstOrDefaultAsync(p => p.Id == id);
            if (product is null)
                throw new Exception($"Product {id} not found");

            context.Products.Remove(product);
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
            return await context.Products.ToListAsync();
        }

        public async Task<Product> GetById(int id)
        {
            return await context.Products.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task Update(int id, Product entity)
        {
            var product = await context.Products.FirstOrDefaultAsync(x=>x.Id == id);
            if (product is not null)
            {
                product.Name = entity.Name;
                product.Description = entity.Description;
                product.Image = entity.Image;
                product.Price = entity.Price;
            }
            await context.SaveChangesAsync();
            
        }
    }
}
