using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DTOCls
{
    public class ProductDTO
    {
        public int ProductID { get; set; }

        [Required(ErrorMessage = "Please enter a product name")]
        [Display(Name = "Product Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please select a category")]
        public String CategoryName { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Please enter a positive price")]
        [Display(Name = "Product Price")]
        public decimal Price { get; set; }
    }
}
