using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProductService.Models
{
    public class Category
    {
        
        [Key]
        public int CategoryID { get; set; }
        [Required(ErrorMessage = "Please enter a Category name")]
        [Display(Name = "Category Name")]
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public virtual ICollection<Product> ProductList { get; set; }
        public Category()
        {
            this.ProductList = new List<Product>();
        }
    }
}