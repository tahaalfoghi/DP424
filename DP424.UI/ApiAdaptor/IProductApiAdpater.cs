using DP424.Domain.Models;
using DP424.Domain.Prototype;
using System.Net.Http;

namespace DP424.UI.ApiAdaptor
{
    // Declare services implementing the Adapter Pattern to ensure compatibility 
    // between the data retrieved from the Web API and the format required by the UI.
    public interface IProductApiAdpater
    {
        Task<IEnumerable<Product>> GetProductsAsync();
        Task<bool> CreateProductAsync(ProductPostDto product);
        Task<bool> UpdateProductAsync(int id, ProductPostDto product);
        Task<bool> DeleteProductAsync(int id);
    }
}
