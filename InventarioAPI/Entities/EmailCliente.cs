using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InventarioAPI.Entities
{
    public class EmailCliente
    {
        public int codigoEmail{ get; set; }
        [Required]
        public string email { get; set; }
        public string nit { get; set; }
        public Clientes Clientes { get; set; }
    }
}
