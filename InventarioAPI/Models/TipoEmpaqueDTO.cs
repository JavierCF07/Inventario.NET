using InventarioAPI.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InventarioAPI.Models
{
    public class TipoEmpaqueDTO
    {
        public int codigoEmpaque { get; set; }
        [Required]
        public string descripcion { get; set; }
    }
}
