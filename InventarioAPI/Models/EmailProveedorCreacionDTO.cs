using InventarioAPI.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InventarioAPI.Models
{
    public class EmailProveedorCreacionDTO
    {
        [Required]
        public string email { get; set; }
        public int codigoProveedor { get; set; }
    }
}
