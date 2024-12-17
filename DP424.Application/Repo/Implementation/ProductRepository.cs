using DP424.Domain.Models;
using DP424.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace DP424.Application.Repo.Implementation
{
    public class ProductRepository
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
        public async Task SendProductCreationNotification(Product product,string action)
        {
            Console.WriteLine($"Email sent: Product '{product.Name}' has been {action}.");
            await Task.CompletedTask;
        }
        public async Task SendProductUpdateNotification(Product product, string action)
        {
            Console.WriteLine($"Email sent: Product '{product.Name}' has been {action}.");
            await Task.CompletedTask;
        }
        public async Task SendProductDeleteNotification(int id, string action)
        {
            Console.WriteLine($"Email sent: Product '{id}' has been {action}.");
            await Task.CompletedTask;
        }
        public void LogOperation(string operation)
        {
            Console.WriteLine($"Log: {operation} operation performed at {DateTime.Now}");
        }
        public bool ValidateProduct(Product product)
        {
            if (string.IsNullOrWhiteSpace(product.Name))
                return false;
            if (product.Price <= 0)
                return false;
            return true;
        }
    }
}
