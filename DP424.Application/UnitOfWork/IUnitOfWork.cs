using DP424.Application.Repo.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DP424.Application.UnitOfWork
{
    public interface IUnitOfWork:IDisposable
    {
        IProductRepository ProductRepository { get; }
        Task CommitAsync();

    }
}
