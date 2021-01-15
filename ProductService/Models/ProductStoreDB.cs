using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ProductService.Models
{
    public class ProductStoreDB: DbContext
    {
        public ProductStoreDB() : base("name=ProductStoreDB")
        {
        }
        public System.Data.Entity.DbSet<ProductService.Models.Product> Products { get; set; }

        public System.Data.Entity.DbSet<ProductService.Models.Category> Categories { get; set; }
    }
}