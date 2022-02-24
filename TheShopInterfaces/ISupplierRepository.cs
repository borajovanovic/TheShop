using System.Collections.Generic;

namespace ShopInterfaces
{
    /// <summary>
    /// Represent SupplierRepository interface
    /// </summary>
    public interface ISupplierRepository
    {
        /// <summary>
        /// Gets all supliers.
        /// </summary>
        /// <returns></returns>
        IList<Supplier> GetAllSupliers();

    }
}
