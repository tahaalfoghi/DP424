using DP424.Application.Repo.Implementation;
using DP424.Domain.Prototype;

namespace DP424.Web.Command
{
    // Concrete Command
    // Command Pattern : Execute Update product

    public class UpdateProductCommand : ICommand
    {
        private readonly int _id;
        private readonly ProductPostDto _request;
        private readonly ProductRepository repo;

        public UpdateProductCommand(int id, ProductPostDto request,ProductRepository repo)     {
            _id = id;
            _request = request;
            this.repo = repo;
        }

        public async Task Execute()
        {
            // Logic for updating a product
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
            await repo.Update(_id, product);

            // Usage of anti-pattern 
            await repo.SendProductUpdateNotification(product, "updated");

        }
    }


}
