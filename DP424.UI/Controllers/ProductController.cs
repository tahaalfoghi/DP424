using DP424.Domain.Dtos;
using DP424.Domain.Models;
using DP424.UI.ApiAdaptor;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace DP424.UI.Controllers
{
    public class ProductController:Controller
    {
        Uri baseAddress = new Uri("https://localhost:7083/api");
        private readonly HttpClient _httpClient;
        private readonly IProductApiAdpator _apdator;
        
        public ProductController(IProductApiAdpator apdator)
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = baseAddress;
            _apdator = apdator;
        }
        public async Task<IActionResult> Index()
        {
            /*List<Product> products = new List<Product>();
            HttpResponseMessage response = _httpClient.GetAsync(_httpClient.BaseAddress +
                "/product/products").Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                products = JsonConvert.DeserializeObject<List<Product>>(data);
            }*/
            var products = await _apdator.GetProductsAsync();
            return View(products);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(ProductPostDto product)
        {
           
            if (!ModelState.IsValid)
                return View(product);

            bool isSuccess = await _apdator.CreateProductAsync(product);
            if (isSuccess)
                return RedirectToAction("Index");

            ModelState.AddModelError("", "Failed to create product");
            return View(product);
        }
       
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            
            var response = await _apdator.DeleteProductAsync(id);
            if (response)
            {
                return RedirectToAction("Index");

            }

            else
            {
                TempData["Error"] = "Failed to delete the product.";
                return RedirectToAction("Index");
            }

        }
        
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"/api/product/{id}");
            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                var product = JsonConvert.DeserializeObject<Product>(data);

                // Map Product to ProductPostDto
                var productDto = new ProductPostDto
                {
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price,
                    Category = product.Category,
                };

                return View(productDto);
            }

            TempData["Error"] = "Product not found.";
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> Edit(ProductPostDto product, int id)
        {
            /*if (!ModelState.IsValid)
                return View(product);

            var formData = new MultipartFormDataContent();
            formData.Add(new StringContent(product.Name), "Name");
            formData.Add(new StringContent(product.Description), "Description");
            formData.Add(new StringContent(product.Price.ToString()), "Price");
            formData.Add(new StringContent(product.Category), "Category");

            if (product.Image != null)
            {
                var imageStream = product.Image.OpenReadStream();
                formData.Add(new StreamContent(imageStream), "Image", product.Image.FileName);
            }

            try
            {
                var response = await _httpClient.PutAsync($"/api/product/update/{id}", formData);
                if (response.IsSuccessStatusCode)
                    return RedirectToAction("Index");
                else
                    ModelState.AddModelError("", "Failed to update product");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"An error occurred: {ex.Message}");
            }

            return View(product);*/
            if (!ModelState.IsValid)
                return View(product);

            bool isSuccess = await _apdator.UpdateProductAsync(id, product);
            if (isSuccess)
                return RedirectToAction("Index");

            ModelState.AddModelError("", "Failed to update product");
            return View(product);

        }
    }
}
