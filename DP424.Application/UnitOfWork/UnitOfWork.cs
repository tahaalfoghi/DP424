using DP424.Application.Repo.Abstract;
using DP424.Infrastructure;

namespace DP424.Application.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext context;

        public IProductRepository ProductRepository { get; }

        public UnitOfWork(AppDbContext context, IProductRepository productRepository)
        {
            this.context = context;
            ProductRepository = productRepository;
        }

        public async Task CommitAsync()
        {
           await context.SaveChangesAsync();
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}
