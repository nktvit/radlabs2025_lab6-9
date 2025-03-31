using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductModel
{
    public interface IProduct<T> : IRepository<T> where T : Product
    {
        // Might want to implement specific Product functionality Later

        public Product UpdateReorderLevel(int id, int reorderLevel);
    }
}
