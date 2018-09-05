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

        public Status Status { get; set; }

        public decimal GST { get; set; }

        public decimal SubTotal { get; set; }

        public decimal GrandTotal { get; set; }
       
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Order Date")]
        public System.DateTime Date { get; set; }

        public List<OrderItem> OrderItems { get; set; }


    }
}
