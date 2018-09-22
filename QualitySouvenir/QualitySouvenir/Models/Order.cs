using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace QualitySouvenir.Models
{
    public enum Status
    {
        waitting, shipped
    }
    public class Order
    {
      
        public int ID { get; set; }

        [Required]
        [StringLength(30)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(30)]
        public string LastName { get; set; }

        [Required]
        [StringLength(30)]
        public string City { get; set; }

        public string State { get; set; }

        [Required]
        [StringLength(30)]
        public string PostalCode { get; set; }


        public string Country { get; set; }

        [Required]
        [StringLength(50)]
        public string Phone { get; set; }
        public Status Status { get; set; }
        public decimal GST { get; set; }
        public decimal SubTotal { get; set; }
        public decimal GrandTotal { get; set; }    
        
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Order Date")]
        public System.DateTime Date { get; set; }
        public List<OrderItem> OrderItems { get; set; }
        public ApplicationUser User { get; set; }



    }
}
