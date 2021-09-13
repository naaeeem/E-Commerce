using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;

namespace DhakaPlaza.Models
{
    public class SpecialTags
    {
        public int Id { get; set; }

        [Required] // Annotations
        [Display(Name = "Tag Name")]
        public string SpecialTag { get; set; }
    }
}
