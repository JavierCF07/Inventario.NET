using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InventarioAPI.Entities
{
    public class Producto
    {
        public int codigoProducto { get; set; }
        public int codigoCategoria { get; set; }
        public int codigoEmpaque { get; set; }
        [Required]
        public string descripcion { get; set; }
        public Decimal precioUnitario { get; set; }
        public Decimal precioPorDocena { get; set; }
        public Decimal precioPorMayor { get; set; }
        public int existencia { get; set; }
        public string imagen { get; set; }
        public Categoria Categoria { get; set; }
        public TipoEmpaque TipoEmpaque { get; set; }
    }
}
