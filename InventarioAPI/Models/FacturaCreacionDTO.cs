using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InventarioAPI.Models
{
    public class FacturaCreacionDTO
    {
        [Required]
        public DateTime fecha { get; set; }
        public decimal total { get; set; }
        public int nit { get; set; }
    }
}
