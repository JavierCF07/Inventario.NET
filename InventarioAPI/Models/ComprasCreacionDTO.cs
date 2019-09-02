using InventarioAPI.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InventarioAPI.Models
{
    public class ComprasCreacionDTO
    {
        [Required]
        public int numeroDocumento { get; set; }
        public int codigoProveedor { get; set; }
        public DateTime fecha { get; set; }
        public decimal total { get; set; }
        public virtual ICollection<Proveedores> Proveedores { get; set; }
    }
}
