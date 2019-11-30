﻿using InventarioAPI.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InventarioAPI.Models
{
    public class ClientesDTO
    {
        public int nit { get; set; }
        [Required]
        public ulong DPI { get; set; }
        public string nombre { get; set; }
        public string direccion { get; set; }
    }
}
