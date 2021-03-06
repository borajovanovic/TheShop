using ShopInterfaces;
using System.Collections.Generic;

namespace ShopImplementation
{
    internal class SupplierService : ISupplierService
    {
        private readonly ISupplierRepository supplierRepository;

        public SupplierService(ISupplierRepository supplierRepository)
        {
            this.supplierRepository = supplierRepository;

        }

        IList<Supplier> ISupplierService.GetSupliers()
        {
            return this.supplierRepository.GetAllSupliers();
        }
    }
}
