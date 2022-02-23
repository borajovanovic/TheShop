using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopInterfaces
{
    public interface ISupplierRepository
    {
        IList<Supplier> GetAllSupliers();

    }
}
