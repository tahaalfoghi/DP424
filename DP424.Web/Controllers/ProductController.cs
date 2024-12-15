using AutoMapper;
using DP424.Application.UnitOfWork;
using DP424.Domain.Dtos;
using DP424.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace DP424.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;

        public ProductController(IUnitOfWork uow, IMapper mapper)
        {
            this.uow = uow;
            this.mapper = mapper;
        }
        [HttpGet]
        [Route("products")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await uow.ProductRepository.GetAll());
        }
        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            if (id <= 0)
                return BadRequest($"Invalid request for id:{id}");

            var product = await uow.ProductRepository.GetById(id);
            if (product is null)
                return NotFound($"Product with id: {id} not found");

            return Ok(product);
        }
        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateProduct([FromForm] ProductPostDto request)
        {
            if (!ModelState.IsValid)
                return BadRequest("Inavlid model");

            string? ImgUrl = string.Empty;
            if (request.Image is not null)
            {
                var path = Path.Combine("wwwroot", "Images", request.Image.FileName);
                using var stream = new FileStream(path, FileMode.Create);
                await request.Image.CopyToAsync(stream);
                ImgUrl = $"/Images/{request.Image.FileName}";
            }
            var product = new Product
            {
                Name = request.Name,
                Description = request.Description,
                Price = request.Price,
                Image = ImgUrl,
                Category = request.Category
            };
            //var product = mapper.Map<Product>(request); 
            await uow.ProductRepository.Create(product);

            return CreatedAtAction(nameof(CreateProduct), new { productId = product.Id }, product);
        }
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            if (id <= 0)
                return BadRequest($"Invalid request for id:{id}");

            var product = await uow.ProductRepository.GetById(id);
            if (product is null)
                return NotFound($"Product with id:{id} is not found");
            await uow.ProductRepository.Delete(id);

            return Ok("Product deleted successfully");
        }
        [HttpPut]
        [Route("update/{id:int}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromForm] ProductPostDto request)
        {
            if (id <= 0 || !ModelState.IsValid)
                return BadRequest("Invalid request");

            var product = await uow.ProductRepository.GetById(id);
            if(product is null)
                return NotFound();

          
            string? ImgUrl = string.Empty;
            if (request.Image is not null)
            {
                var path = Path.Combine("wwwroot", "Images", request.Image.FileName);
                using var stream = new FileStream(path, FileMode.Create);
                await request.Image.CopyToAsync(stream);
                ImgUrl = $"/Images/{request.Image.FileName}";
            }
            product = new Product
            {
                Name = request.Name,
                Description = request.Description,
                Price = request.Price,
                Image = ImgUrl,
                Category = request.Category
            };
            await uow.ProductRepository.Update(id, product);
            return Ok("Product updated successfully");
        }
    }
}
