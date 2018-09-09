using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QualitySouvenir.Models
{
    public class Souvenir
    {
        public int ID { get; set; }

        [Required]
        [StringLength(30)]
        //[RegularExpression(@"^[A-Z]+[a-zA-Z''-'\s]*$")]
        public string Name { get; set; }

        [Required]
        public Double Price { get; set; }

        public string Description { get; set; }

        [Required]
        [Display(Name = "Supplier Name")]
        public int SupplierID { get; set; }

        [Required]
        [Display(Name = "Category Name")]
        public int CategoryID { get; set; }

        public string Image { get; set; }

        public Supplier Supplier { get; set; }

        public Category Category { get; set; }
    }
}
