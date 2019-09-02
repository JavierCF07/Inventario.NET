using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InventarioAPI.Models
{
    public class InventarioCreacionDTO
    {
        [Required]
        public int codigoProducto { get; set; }
        public DateTime fecha { get; set; }
        public string tipoRegistro { get; set; }
        public decimal precio { get; set; }
        public int entradas { get; set; }
        public int salidas { get; set; }
    }
}
