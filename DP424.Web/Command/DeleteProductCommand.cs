using DP424.Application.UnitOfWork;

namespace DP424.Web.Command
{
    // Command Pattern : Execute Delete product

    public class DeleteProductCommand : ICommand
    {
        private readonly int _id;
        private readonly IUnitOfWork _uow;
       
        public DeleteProductCommand(int id, IUnitOfWork uow)
        {
            _id = id;
            _uow = uow;
        }

        public async Task Execute()
        {
            // Logic for deleting a product
            await _uow.ProductRepository.Delete(_id);
        }
    }


}
