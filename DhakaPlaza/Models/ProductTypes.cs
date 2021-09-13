using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DhakaPlaza.Models
{
    public class ProductTypes
    {
        public int Id { get; set; }


        [Required] // Annotations
        [Display(Name = "Product Name")]
        public string ProductType { get; set; }

    }
}
