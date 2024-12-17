using DP424.Domain.Models;
using DP424.Domain.Prototype;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DP424.Domain.Dtos
{
    // Prototype pattern usage
    public class ProductPostDto:IProductPrototype
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public double Price { get; set; }
        public IFormFile Image { get; set; } = default!;
        public string? ImageUrl { get; set; }
        public string Category { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        // Use the Prototype Pattern to create a clone of the current ProductPostDto object, 
        // allowing duplication of product data with a specified image path.
        public Product clone(string imagPath)
        {
            return new Product
            {
                Name = this.Name,
                Description = this.Description,
                Price = this.Price,
                Image = imagPath,
                Category = this.Category
            };
        }
    }
}
