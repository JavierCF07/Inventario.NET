using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InventarioAPI.Entities
{
    public class Compras
    {
        public int idCompra { get; set; }
        [Required]
        public int numeroDocumento { get; set; }
        public int codigoProveedor { get; set; }
        public DateTime fecha { get; set; }
        public decimal total { get; set; }
        public Proveedores Proveedores { get; set; }
    }
}
