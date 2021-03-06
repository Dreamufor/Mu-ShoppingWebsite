﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace QualitySouvenir.Models
{
    public class OrderItem
    {
        public int ID { get; set; }

        public int Quantity { get; set; }

        public decimal UnitPrice { get; set; }
        
        public Souvenir Souvenir { get; set; }

        public Order Order { get; set; }
    }
}
