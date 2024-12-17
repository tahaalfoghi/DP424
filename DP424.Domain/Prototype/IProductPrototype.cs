using DP424.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DP424.Domain.Prototype
{
    // Declare the inteface for the prototype pattern
    public interface IProductPrototype
    {
        Product clone(string imagPath);
    }
}
