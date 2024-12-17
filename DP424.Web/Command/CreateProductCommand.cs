using DP424.Application.Repo.Implementation;
using DP424.Domain.Prototype;

namespace DP424.Web.Command
{
    // Concrete Command
    // Command Pattern : Execute create product
    public class CreateProductCommand : ICommand
    {
        private readonly ProductPostDto _request;
        
        private readonly ProductRepository repo;
        public CreateProductCommand(ProductPostDto request,  ProductRepository repo)
        {
            _request = request;
            this.repo = repo;
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

            /*var product = new Product
            {
                Name =  _request.Name,
                Price = _request.Price,
                Description  = _request.Description,
                Category = _request.Category,
                Image = _request.Image,
            }*/
            var product = _request.clone(ImgUrl);


            await repo.Create(product);

            // Usage of anti-pattern 
            await repo.SendProductCreationNotification(product, "created");

        }
    }


}
