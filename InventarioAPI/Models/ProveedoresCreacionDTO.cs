﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InventarioAPI.Models
{
    public class ProveedoresCreacionDTO
    {
        [Required]
        public string nit { get; set; }
        public string razonSocial { get; set; }
        public string direccion { get; set; }
        public string paginaWeb { get; set; }
        public string contactoPrincipal { get; set; }
    }
}
