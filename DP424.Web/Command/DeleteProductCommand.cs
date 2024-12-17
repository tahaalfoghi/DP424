using DP424.Application.Repo.Implementation;


namespace DP424.Web.Command
{
    // Concrete Command
    // Command Pattern : Execute Delete product

    public class DeleteProductCommand : ICommand
    {
        private readonly int _id;
        private readonly ProductRepository repo;
        

        public DeleteProductCommand(int id,  ProductRepository repo)
        {
            _id = id;
            this.repo = repo;
            
        }

        public async Task Execute()
        {
            // Logic for deleting a product
            await repo.Delete(_id);

            // Usage of anti-pattern 
            await repo.SendProductDeleteNotification(_id, "deleteed");
        }
    }


}
