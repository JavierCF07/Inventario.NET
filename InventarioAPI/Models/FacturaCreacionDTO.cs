﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InventarioAPI.Models
{
    public class FacturaCreacionDTO
    {
        [Required]
        public string nit { get; set; }
        public DateTime fecha { get; set; }
        public decimal total { get; set; }
    }
}