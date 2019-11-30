using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InventarioAPI.Models
{
    public class TelefonoClienteCreacionDTO
    {
        public string numero { get; set; }
        [Required]
        public string descripcion { get; set; }
        public int nit { get; set; }
    }
}
