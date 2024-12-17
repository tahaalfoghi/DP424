using AutoMapper;
using DP424.Application.UnitOfWork;
using DP424.Domain.Dtos;
using DP424.Domain.Models;
using DP424.Web.Command;
using Microsoft.AspNetCore.Mvc;

namespace DP424.Web.Controllers
{
    // Command pattern implemented in POST,PUT,DELETE methods
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;
        private readonly CommandHandler commandHandler;
        public ProductController(IUnitOfWork uow, IMapper mapper, CommandHandler commandHandler)
        {
            this.uow = uow;
            this.mapper = mapper;
            this.commandHandler = commandHandler;
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

            // Command pattern 
            // Create a new product using the command pattern
            var createCommand = new CreateProductCommand(request,uow);
            await commandHandler.ExecuteCommand(createCommand);

            return CreatedAtAction(nameof(CreateProduct), new { productId = request.Id }, request);
        }
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            if (id <= 0)
                return BadRequest($"Invalid request for id:{id}");

            // Command pattern 
            // Delete an existing product using the command pattern
            var deleteCommand = new DeleteProductCommand(id,uow);
            await commandHandler.ExecuteCommand(deleteCommand);

            return Ok("Product deleted successfully");
        }
        [HttpPut]
        [Route("update/{id:int}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromForm] ProductPostDto request)
        {
            if (id <= 0 || !ModelState.IsValid)
                return BadRequest("Invalid request");

            // Command pattern
            // Updates an existing product using the Command Pattern.
            var updateCommand =  new UpdateProductCommand(id,request,uow);
            await commandHandler.ExecuteCommand(updateCommand);

            return Ok("Product updated successfully");
        }
    }
}
