using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InventarioAPI.Entities
{
    public class EmailProveedor
    {
        public int codigoEmail { get; set; }
        [Required]
        public string email { get; set; }
        public int codigoProveedor { get; set; }
        public Proveedores Proveedores { get; set; }
    }
}
