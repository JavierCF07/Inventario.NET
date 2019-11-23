using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InventarioAPI.Models
{
    public class CategoriaDTO
    {
        public int codigoCategoria { get; set; }
        [Required]
        public string descripcion { get; set; }
    }
}
