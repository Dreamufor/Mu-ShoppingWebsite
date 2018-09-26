using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QualitySouvenir.Models
{
    public class Supplier
    {
        public int ID { get; set; }

        [Required]
        [StringLength(30)]
        public string Name { get; set; }

        [Required]
        [MaxLength(11)]
        [MinLength(6)]
        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public ICollection<Souvenir> Souvenirs { get; set; }
    }
}
