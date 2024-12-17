using DP424.Application.UnitOfWork;
using DP424.Domain.Dtos;

namespace DP424.Web.Command
{
    // Command Pattern : Execute create product
    public class CreateProductCommand : ICommand
    {
        private readonly ProductPostDto _request;
        private readonly IUnitOfWork _uow;
        public CreateProductCommand(ProductPostDto request, IUnitOfWork uow)
        {
            _request = request;
            _uow = uow;
        }

        public async Task Execute()
        {
            // Logic for creating a product
            string? ImgUrl = string.Empty;
            if (_request.Image is not null)
            {
                var path = Path.Combine("wwwroot", "Images", _request.Image.FileName);
                using var stream = new FileStream(path, FileMode.Create);
                await _request.Image.CopyToAsync(stream);
                ImgUrl = $"/Images/{_request.Image.FileName}";
            }

            // Use the Prototype Pattern to create a clone of the current ProductPostDto object,
            var product = _request.clone(ImgUrl);
            await _uow.ProductRepository.Create(product);
        }
    }


}
