using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InventarioAPI.Entities
{
    public class Categoria
    {
        public int codigoCategoria { get;set; }
        [Required]
        public string descripcion { get; set; }

    }
}
