using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InventarioAPI.Entities
{
    public class DetalleCompra
    {
        public int idDetalle { get; set; }
        public int idCompra { get; set; }
        public int codigoProducto { get; set; }
        [Required]
        public int cantidad { get; set; }
        public decimal precio { get; set; }
        public Compras Compras { get; set; }
        public Producto Producto { get; set; }
    }
}
