using InventarioAPI.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InventarioAPI.Models
{
    public class ProductoDTO
    {
        public int codigoProducto { get; set; }
        public int codigoCategoria { get; set; }
        public int codigoEmpaque { get; set; }
        [Required]
        public string descripcion { get; set; }
        public decimal precioUnitario { get; set; }
        public decimal precioPorDocena { get; set; }
        public decimal precioPorMayor { get; set; }
        public int existencia { get; set; }
        public string imagen { get; set; }
        public Categoria Categoria { get; set; }
        public TipoEmpaque TipoEmpaque { get; set; }
    }
}
