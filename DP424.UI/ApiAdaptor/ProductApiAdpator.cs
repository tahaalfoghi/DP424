using DP424.Domain.Dtos;
using DP424.Domain.Models;
using Newtonsoft.Json;
using System.Net.Http;

namespace DP424.UI.ApiAdaptor
{
    // Implementing the Adapter Pattern to transform data between the Web API and the UI.
    // This ensures the data retrieved in JSON format is converted into Product objects for the UI,
    // and the data submitted by the UI is formatted correctly for the Web API.
    public class ProductApiAdpator : IProductApiAdpator
    {
        Uri baseAddress = new Uri("https://localhost:7083/api");
        private readonly HttpClient _client;

        public ProductApiAdpator()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
        }

        // Sends a POST request to create a new product in the Web API.
        public async Task<bool> CreateProductAsync(ProductPostDto product)
        {
            var formData = new MultipartFormDataContent
            {
               { new StringContent(product.Name), "Name" },
               { new StringContent(product.Description), "Description" },
               { new StringContent(product.Price.ToString()), "Price" },
               { new StringContent(product.Category), "Category" }
            };

            if (product.Image != null)
            {
                var imageStream = product.Image.OpenReadStream();
                formData.Add(new StreamContent(imageStream), "Image", product.Image.FileName);
            }

            var response = await _client.PostAsync("/api/product/create", formData);
            return response.IsSuccessStatusCode;
        }

        // Sends a POST request to create a new product in the Web API.
        public async Task<bool> DeleteProductAsync(int id)
        {
            var response = await _client.DeleteAsync($"/api/product/{id}");
            return response.IsSuccessStatusCode;
        }

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            // Get product and convert it from json format into List<Product>

            List<Product> products = new List<Product>();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress +
                "/product/products").Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                products = JsonConvert.DeserializeObject<List<Product>>(data);
            }
            return products;
        }
        // Sends a PUT request to update an existing product in the Web API by its ID.
        public async Task<bool> UpdateProductAsync(int id, ProductPostDto product)
        {
            var formData = new MultipartFormDataContent
            {
                { new StringContent(product.Name), "Name" },
                { new StringContent(product.Description), "Description" },
                { new StringContent(product.Price.ToString()), "Price" },
                { new StringContent(product.Category), "Category" }
            };

            if (product.Image != null)
            {
                var imageStream = product.Image.OpenReadStream();
                formData.Add(new StreamContent(imageStream), "Image", product.Image.FileName);
            }

            var response = await _client.PutAsync($"/api/product/update/{id}", formData);
            return response.IsSuccessStatusCode;
        }
    }
}
