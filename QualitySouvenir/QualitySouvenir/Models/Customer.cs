using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QualitySouvenir.Models
{
    public class Customer
    {
        public int ID { get; set; }

        public string UserName { get; set; }

        public string LastName { get; set; }

        public string FirstName { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }

        public bool AccountStatus { get; set; }

        public string Password { get; set; }

        public ICollection<Order> Orders { get; set; }
    }
}
