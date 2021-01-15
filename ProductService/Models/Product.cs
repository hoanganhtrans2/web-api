using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace ProductService.Models
{
    public class Product //model
    {   [Required]
        public int ProductID { get; set; }
        [Required(ErrorMessage = "Please enter a product name")]
        [Display(Name = "Product Name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please select a category")]
        public int CategoryID { get; set; }
        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Please enter a positive price")]
        [Display(Name = "Product Price")]
        public decimal Price { get; set; }
        public virtual Category Category { get; set; }
    }
}