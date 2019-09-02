using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InventarioAPI.Entities
{
    public class TelefonoCliente
    {
        public int codigoTelefono { get; set; }
        public string numero { get; set; }
        [Required]
        public string descripcion { get; set; }
        public string nit { get; set; }
        public Clientes Clientes { get; set; }
    }
}
