using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InventarioAPI.Models
{
    public class DetalleFacturaCreacionDTO
    {
        public int numeroFactura { get; set; }
        public int codigoProducto { get; set; }
        [Required]
        public int cantidad { get; set; }
        public decimal precio { get; set; }
        public decimal descuento { get; set; }
    }
}
