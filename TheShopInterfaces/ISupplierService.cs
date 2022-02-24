using System.Collections.Generic;

namespace ShopInterfaces
{
    /// <summary>
    /// Represent SupplierService interface
    /// </summary>
    public interface ISupplierService
    {
        /// <summary>
        /// Gets the supliers.
        /// </summary>
        /// <returns></returns>
        IList<Supplier> GetSupliers();

    }
}
