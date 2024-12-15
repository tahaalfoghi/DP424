using DP424.Domain.Dtos;
using DP424.Domain.Models;
using System.Net.Http;

namespace DP424.UI.ApiAdaptor
{
    public interface IProductAdpator
    {
        Task<IEnumerable<Product>> GetProductsAsync();
        Task<bool> CreateProductAsync(ProductPostDto product);
        Task<bool> UpdateProductAsync(int id, ProductPostDto product);
        Task<bool> DeleteProductAsync(int id);
    }
}
