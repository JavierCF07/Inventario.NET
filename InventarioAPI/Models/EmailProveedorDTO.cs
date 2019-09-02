using InventarioAPI.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InventarioAPI.Models
{
    public class EmailProveedorDTO
    {
        public int codigoEmail { get; set; }
        [Required]
        public string email { get; set; }
        public int codigoProveedor { get; set; }
        public Proveedores Proveedores { get; set; }
    }
}
