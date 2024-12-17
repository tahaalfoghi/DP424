using DP424.Domain.Dtos;
using DP424.Domain.Models;
using System.Net.Http;

namespace DP424.UI.ApiAdaptor
{
    // Declare services implementing the Adapter Pattern to ensure compatibility 
    // between the data retrieved from the Web API and the format required by the UI.
    public interface IProductApiAdpator
    {
        Task<IEnumerable<Product>> GetProductsAsync();
        Task<bool> CreateProductAsync(ProductPostDto product);
        Task<bool> UpdateProductAsync(int id, ProductPostDto product);
        Task<bool> DeleteProductAsync(int id);
    }
}
