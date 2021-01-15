using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProductService.Models
{
    public class DataInitializer :
    System.Data.Entity.CreateDatabaseIfNotExists<ProductStoreDB>
    {
        protected override void Seed(ProductStoreDB context)
        {
            context.Categories.Add(new Category { CategoryName = "Beverages", Description = "Soft drinks, coffees, teas, beers, and ales" });
            context.Categories.Add(new Category { CategoryName = "Condiments", Description = "Sweet and savory sauces, relishes, spreads, and seasonings" });
            context.Categories.Add(new Category { CategoryName = "Confections", Description = "Desserts, candies, and sweet breads" });
            context.Categories.Add(new Category { CategoryName = "Dairy Products", Description = "Cheeses" });
            context.Categories.Add(new Category { CategoryName = "Grains, Cereals", Description = "Breads, crackers, pasta, and cereal" });
            context.Categories.Add(new Category { CategoryName = "Produce", Description = "Dried fruit and bean curd" });
            context.SaveChanges();
            context.Products.Add(new Product { Name = "Aniseed Syrup", CategoryID = 1, Price = 12 });
            context.Products.Add(new Product { Name = "Chef Anton's Cajun Seasoning", CategoryID = 1, Price = 22 });
            context.Products.Add(new Product { Name = "Chartreuse verte", CategoryID = 1, Price = 22 });
            context.Products.Add(new Product { Name = "Chai 6", CategoryID = 1, Price = 22 });
            context.Products.Add(new Product { Name = "Guaraná Fantástica", CategoryID = 1, Price = 22 });
            context.Products.Add(new Product { Name = "Original Frankfurter grüne Soße", CategoryID = 2, Price = 22 });
            context.Products.Add(new Product { Name = "Sirop d'érable", CategoryID = 2, Price = 22 });
            context.Products.Add(new Product { Name = "Vegie-spread", CategoryID = 2, Price = 22 });
            context.Products.Add(new Product { Name = "Genen Shouyu", CategoryID = 3, Price = 22 });
            context.Products.Add(new Product { Name = "Pavlova", CategoryID = 3, Price = 22 });
            context.Products.Add(new Product { Name = "Teatime Chocolate Biscuits", CategoryID = 3, Price = 22 });
            context.Products.Add(new Product { Name = "Queso Manchego La Pastora", CategoryID = 4, Price = 22 });
            base.Seed(context);
        }
    }
}