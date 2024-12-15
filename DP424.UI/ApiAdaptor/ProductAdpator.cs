using DP424.Domain.Dtos;
using DP424.Domain.Models;
using Newtonsoft.Json;
using System.Net.Http;

namespace DP424.UI.ApiAdaptor
{
    public class ProductAdpator : IProductAdpator
    {
        Uri baseAddress = new Uri("https://localhost:7083/api");
        private readonly HttpClient _client;

        public ProductAdpator()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
        }

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

        public async Task<bool> DeleteProductAsync(int id)
        {
            var response = await _client.DeleteAsync($"/api/product/{id}");
            return response.IsSuccessStatusCode;
        }

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
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
