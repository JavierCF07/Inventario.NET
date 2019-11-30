using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InventarioAPI.Entities
{
    public class Clientes
    {
        public int nit { get; set; }
        [Required]
        public ulong DPI { get; set; }
        public string nombre { get; set; }
        public string direccion { get; set; }
    }
}
