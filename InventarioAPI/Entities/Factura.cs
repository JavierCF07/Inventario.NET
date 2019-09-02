using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InventarioAPI.Entities
{
    public class Factura
    {
        public int numeroFactura { get; set; }
        [Required]
        public string nit { get; set; }
        public DateTime fecha { get; set; }
        public decimal total { get; set; }
        public Clientes Clientes { get; set; }
    }
}
