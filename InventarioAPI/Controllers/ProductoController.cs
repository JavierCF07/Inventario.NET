using AutoMapper;
using InventarioAPI.Contexts;
using InventarioAPI.Entities;
using InventarioAPI.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventarioAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ProductoController : ControllerBase
    {
        private readonly InventarioDBContext contexto;
        private readonly IMapper mapper;
        public ProductoController(InventarioDBContext contexto, IMapper mapper)
        {
            this.contexto = contexto;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductoDTO>>> Get()
        {
            var productos = await contexto.Productos.Include("Categoria").Include("TipoEmpaque").ToListAsync();
            var productosDTO = mapper.Map<List<ProductoDTO>>(productos);
            return productosDTO;
        }

        [HttpGet("{numeroDePagina}", Name = "GetProductoPage")]
        [Route("page/{numeroDePagina}")]
        public async Task<ActionResult<ProductoPaginacionDTO>> GetProductoPage(int numeroDePagina = 0)
        {
            int cantidadDeRegistros = 5;
            var productoPaginacionDTO = new ProductoPaginacionDTO();
            var query = contexto.Productos.AsQueryable();
            int totalDeRegistros = query.Count();
            int totalPaginas = (int)Math.Ceiling((Double)totalDeRegistros / cantidadDeRegistros);
            productoPaginacionDTO.Number = numeroDePagina;
            var productos = await contexto.Productos
                .Include("Categoria").Include("TipoEmpaque")
                .Skip(cantidadDeRegistros * (productoPaginacionDTO.Number))
                .Take(cantidadDeRegistros)
                .ToListAsync();
            productoPaginacionDTO.TotalPages = totalPaginas;
            productoPaginacionDTO.Content = mapper.Map<List<ProductoDTO>>(productos);
            
            if (numeroDePagina == 0)
            {
                productoPaginacionDTO.First = true;
            }
            else if (numeroDePagina == totalPaginas)
            {
                productoPaginacionDTO.Last = true;
            }
            return productoPaginacionDTO;
        }

        [HttpGet("{id}", Name = "GetProducto")]
        public async Task<ActionResult<ProductoDTO>> Get(int id)
        {
            var productos = await contexto.Productos.FirstOrDefaultAsync(x => x.codigoProducto == id);
            if(productos == null)
            {
                return NotFound();
            }
            var productosDTO = mapper.Map<ProductoDTO>(productos);
            return productosDTO;
        }

        //agregar
        public async Task<ActionResult> Post([FromBody] ProductoCreacionDTO productoCreacion)
        {
            var producto = mapper.Map<Producto>(productoCreacion);
            contexto.Add(producto);
            await contexto.SaveChangesAsync();
            var productoDTO = mapper.Map<ProductoDTO>(producto);
            return new CreatedAtRouteResult("GetProducto", new { id = producto.codigoProducto }, productoDTO);
        }

        //actualizar
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] ProductoCreacionDTO productoActualizar)
        {
            var producto = this.mapper.Map<Producto>(productoActualizar);
            producto.codigoProducto = id;
            contexto.Entry(producto).State = EntityState.Modified;
            await contexto.SaveChangesAsync();
            return NoContent();
        }

        //eliminar
        [HttpDelete("{id}")]
        public async Task<ActionResult<ProductoDTO>> Delete(int id)
        {
            var codigoProducto = await contexto.Productos.Select(x => x.codigoProducto)
                .FirstOrDefaultAsync(x => x == id);
            if(codigoProducto == default(int))
            {
                return NotFound();
            }
            contexto.Remove(new Producto { codigoProducto = id });
            await contexto.SaveChangesAsync();
            return NoContent();
        }
    }
}
