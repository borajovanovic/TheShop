using ShopImplementation;
using ShopInterfaces;
using System.Collections.Generic;

namespace ShopImplementation
{
    public class SupplierCollectionBuilder
    {
        private IList<SupplierBuilder> supplierBuilders;
        public SupplierCollectionBuilder()
        {
            this.supplierBuilders = new List<SupplierBuilder>();
        }

        public SupplierCollectionBuilder WithSupplier(SupplierBuilder supplierBuilder)
        {
            this.supplierBuilders.Add(supplierBuilder);
            return this;
        }

        public IList<Supplier> Build()
        {
            IList<Supplier> suppliers = new List<Supplier>();
            foreach (SupplierBuilder supplierBuilder in supplierBuilders)
            {
                suppliers.Add(supplierBuilder.BuildSupplier());
            }
            return suppliers;
        }
    }

    public class SupplierBuilder
    {
        private int id;
        private string suplierName;
        private InventoryBuilder inventoryBuilder;

        public SupplierBuilder(string suplierName)
        {
            this.suplierName = suplierName;
            this.inventoryBuilder = new InventoryBuilder();
        }

        public SupplierBuilder WithId(int id)
        {
            this.id = id;
            return this;
        }

        public SupplierBuilder WithInventory(InventoryBuilder inventoryBuilder)
        {
            this.inventoryBuilder = inventoryBuilder;
            return this;
        }

        public Supplier BuildSupplier()
        {
            return new Supplier
            {
                Id = this.id,
                SuplierName = this.suplierName,
                Inventory = this.inventoryBuilder.BuildInventory()
            };
        }
    }

    public class InventoryBuilder
    {
        private List<ArticleBuilder> inventoryArticlesBuilder;

        public InventoryBuilder()
        {
            this.inventoryArticlesBuilder = new List<ArticleBuilder>();
        }

        public InventoryBuilder WithArticle(ArticleBuilder articleBuilder)
        {
            this.inventoryArticlesBuilder.Add(articleBuilder);
            return this;
        }

        public Inventory BuildInventory()
        {
            Inventory inventory = new Inventory();

            foreach (ArticleBuilder articleBuilder in inventoryArticlesBuilder)
            {
                inventory.InventoryArticles = new List<Article>();
                inventory.InventoryArticles.Add(articleBuilder.BuildArticle());
            }
            return inventory;
        }
    }

    public class ArticleBuilder
    {
        private int id;

        private string articleName;

        private int articlePrice;

        public ArticleBuilder(int id, string articleName, int articlePrice)
        {
            this.id = id;
            this.articleName = articleName;
            this.articlePrice = articlePrice;
        }

        public Article BuildArticle()
        {
            return new Article(this.id, this.articleName, this.articlePrice);
        }

    }
}
