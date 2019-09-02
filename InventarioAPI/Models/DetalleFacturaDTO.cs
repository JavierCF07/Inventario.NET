using InventarioAPI.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InventarioAPI.Models
{
    public class DetalleFacturaDTO
    {
        public int codigoDetalle { get; set; }
        public int numeroFactura { get; set; }
        public int codigoProducto { get; set; }
        [Required]
        public int cantidad { get; set; }
        public decimal precio { get; set; }
        public decimal descuento { get; set; }
        public Factura Factura { get; set; }
        public Producto Producto { get; set; }
    }
}
