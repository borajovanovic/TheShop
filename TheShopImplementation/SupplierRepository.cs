using ShopInterfaces;
using System.Collections.Generic;
using System.Linq;

namespace ShopImplementation
{
    public class SupplierRepository : ISupplierRepository
    {
        private List<Supplier> suppliers = new List<Supplier>();

        public IList<Supplier> GetAllSupliers()
        {
            if (this.suppliers.Count == 0 )
            {
                this.suppliers.AddRange(new SupplierCollectionBuilder()
              .WithSupplier(new SupplierBuilder("Suplier1").WithId(1).WithInventory(new InventoryBuilder().WithArticle(new ArticleBuilder(1, "Article from supplier1", 458))))
              .WithSupplier(new SupplierBuilder("Suplier2").WithId(2).WithInventory(new InventoryBuilder().WithArticle(new ArticleBuilder(1, "Article from supplier2", 459))))
              .WithSupplier(new SupplierBuilder("Suplier2").WithId(3).WithInventory(new InventoryBuilder().WithArticle(new ArticleBuilder(1, "Article from supplier3", 460))))
              .Build());
            }
            return this.suppliers;
        }

        
    }
}
