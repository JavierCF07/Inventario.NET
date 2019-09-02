﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InventarioAPI.Entities
{
    public class TelefonoProveedor
    {
        public int codigoTelefono { get; set; }
        public string numero { get; set; }
        [Required]
        public string descripcion { get; set; }
        public int codigoProveedor { get; set; }
        public Proveedores Proveedores { get; set; }
    }
}
