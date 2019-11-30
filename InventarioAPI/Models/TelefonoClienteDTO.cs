using InventarioAPI.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InventarioAPI.Models
{
    public class TelefonoClienteDTO
    {
        public int codigoTelefono { get; set; }
        public string numero { get; set; }
        [Required]
        public string descripcion { get; set; }
        public int nit { get; set; }
        public Clientes Clientes { get; set; }
    }
}
