using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace QualitySouvenir.Models
{
    public class CartItem
    {
        [Key]
        public int ID { get; set; }

        public string CartID { get; set; }
        public int Quantity { get; set; }

        public decimal UnitPrice { get; set; }

        public DateTime DateCreated { get; set; }

        public Souvenir Souvenir { get; set; }
    }
}
