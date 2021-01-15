using ProductService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DTOCls;

namespace ProductService.Controllers
{
    public class ProductController : ApiController
    {
        public IHttpActionResult GetAllProducts()
        {
            IList<ProductDTO> products = null;
            using (var ctx = new ProductStoreDB())
            {
                products = ctx.Products.Include("Category")
                .Select(p => new ProductDTO()
                {
                    ProductID = p.ProductID,
                    Name = p.Name,
                    CategoryName = p.Category.CategoryName,
                    Price = p.Price
                }).ToList<ProductDTO>();
            }
            if (products.Count == 0)
                return NotFound();
            return Ok(products);
        }
        public IHttpActionResult GetProductById(int id)
        {
            ProductDTO productDTO = null;

            using (var ctx = new ProductStoreDB())
            {
                productDTO = ctx.Products.Include("Category")
                    .Where(p => p.ProductID == id)
                    .Select(p => new ProductDTO()
                    {
                        ProductID = p.ProductID,
                        Name = p.Name,
                        CategoryName = p.Category.CategoryName,
                        Price = p.Price
                    }).FirstOrDefault<ProductDTO>();
            }

            if (productDTO == null)
            {
                return NotFound();
            }

            return Ok(productDTO);
        }
        public IHttpActionResult GetAllProducts(string CategoryName)
        {
            IList<ProductDTO> producs = null;

            using (var ctx = new ProductStoreDB())
            {
                producs = ctx.Products.Include("Category")
                    .Where(p => p.Category.CategoryName.ToLower() == CategoryName.ToLower())
                    .Select(p => new ProductDTO()
                    {
                        Name = p.Name,
                        CategoryName = p.Category.CategoryName,
                        Price = p.Price
                    }).ToList<ProductDTO>();
            }
            if (producs.Count == 0)
            {
                return NotFound();
            }
            return Ok(producs);
        }
        public IHttpActionResult PostNewProduct(ProductDTO productDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid data.");
            using (var ctx = new ProductStoreDB())
            {   //get new product form productDTO
                Product product = GetNewProduct(productDTO);
                if (product != null)
                {
                    ctx.Products.Add(product);
                    ctx.SaveChanges();
                    return Ok();
                }
                return BadRequest("Invalid data.");
            }
        }
        public IHttpActionResult PutProduct(ProductDTO product)
        {
            if (!ModelState.IsValid)
                return BadRequest("Not a valid model");
            using (var ctx = new ProductStoreDB())
            {
                var existingProduct = ctx.Products
                    .Where(p => p.ProductID == product.ProductID)
                    .FirstOrDefault<Product>();
                if (existingProduct != null)
                {
                    if (UpdateProduct(existingProduct, product))
                    {
                        ctx.SaveChanges();
                        return Ok();
                    }
                    return BadRequest("Not a valid model"); //CategoryName Invalid
                }
                return NotFound();//Product not found
            }
        }
        public IHttpActionResult Delete(int id)
        {
            if (id <= 0)
                return BadRequest("Not a valid student id");
            using (var ctx = new ProductStoreDB())
            {
                var product = ctx.Products
                    .Where(p => p.ProductID == id)
                    .FirstOrDefault();
                ctx.Entry(product).State = System.Data.Entity.EntityState.Deleted;
                ctx.SaveChanges();
            }
            return Ok();
        }
        //Internal Methods
        private bool UpdateProduct(Product p, ProductDTO pDTO)
        {
            Category category = GetCategory(pDTO.CategoryName);
            if (category != null)
            {
                p.Name = pDTO.Name;
                p.Price = pDTO.Price;
                p.CategoryID = category.CategoryID;
                return true;
            }
            return false;
        }
        private Product GetNewProduct(ProductDTO p)
        {
            Category category = GetCategory(p.CategoryName);
            if (category != null)
            {
                Product product = new Product()
                {
                    Name = p.Name,
                    Price = p.Price,
                    CategoryID = category.CategoryID
                };
                return product;
            }
            return null;
        }

        private Category GetCategory(string CategoryName)
        {
            Category category = null;
            using (var ctx = new ProductStoreDB())
            {
                category = ctx.Categories
                    .Where(c => c.CategoryName.ToLower() == CategoryName.ToLower())
                    .FirstOrDefault();
            }
            return category;
        }
    }
}
