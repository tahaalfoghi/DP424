using DP424.Domain.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DP424.Domain.Prototype
{
    // Prototype pattern usage
    public class ProductPostDto : IProductPrototype
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public double Price { get; set; }
        public IFormFile Image { get; set; } = default!;
        public string? ImageUrl { get; set; }
        public string Category { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        // Apply the Prototype Pattern to create a clone of the current ProductPostDto object, 
        // enabling duplication of product data with a specified image path.

        public Product clone(string imagPath)
        {
            return new Product
            {
                Name = Name,
                Description = Description,
                Price = Price,
                Image = imagPath,
                Category = Category
            };
        }
    }
}
