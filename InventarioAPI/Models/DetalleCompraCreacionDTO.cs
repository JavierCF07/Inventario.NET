using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InventarioAPI.Models
{
    public class DetalleCompraCreacionDTO
    {
        public int idCompra { get; set; }
        public int codigoProducto { get; set; }
        [Required]
        public int cantidad { get; set; }
        public decimal precio { get; set; }
    }
}
