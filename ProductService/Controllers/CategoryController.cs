using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ProductService.Models;

namespace ProductService.Controllers
{
    public class CategoryController : ApiController
    {
        public IHttpActionResult GetAllCategoryName()
        {
            IList<string> categoryNames = null;

            using (var ctx = new ProductStoreDB())
            {
                categoryNames = ctx.Categories
                    .Select(c => c.CategoryName).ToList<string>();
            }
            if (categoryNames.Count == 0)
            {
                return NotFound();
            }
           return Ok(categoryNames);
        }
    }
}
