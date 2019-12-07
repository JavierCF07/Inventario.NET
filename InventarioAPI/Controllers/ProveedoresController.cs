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
    public class ProveedoresController : ControllerBase
    {
        private readonly InventarioDBContext contexto;
        private readonly IMapper mapper;
        public ProveedoresController(InventarioDBContext contexto, IMapper mapper)
        {
            this.contexto = contexto;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProveedoresDTO>>> Get()
        {
            var proveedor = await contexto.Proveedores.ToListAsync();
            var proveedorDTO = mapper.Map<List<ProveedoresDTO>>(proveedor);
            return proveedorDTO;
        }

        [HttpGet("{numeroDePagina}", Name = "GetProveedoresPage")]
        [Route("page/{numeroDePagina}")]
        public async Task<ActionResult<ProveedoresPaginacionDTO>> GetProveedoresPage(int numeroDePagina = 0)
        {
            int cantidadDeRegistros = 5;
            var proveedorPaginacionDTO = new ProveedoresPaginacionDTO();
            var query = contexto.Proveedores.AsQueryable();
            int totalDeRegistros = query.Count();
            int totalPaginas = (int)Math.Ceiling((Double)totalDeRegistros / cantidadDeRegistros);
            proveedorPaginacionDTO.Number = numeroDePagina;
            var proveedor = await contexto.Proveedores
                .Skip(cantidadDeRegistros * (proveedorPaginacionDTO.Number))
                .Take(cantidadDeRegistros)
                .ToListAsync();
            proveedorPaginacionDTO.TotalPages = totalPaginas;
            proveedorPaginacionDTO.Content = mapper.Map<List<ProveedoresDTO>>(proveedor);
            if (numeroDePagina == 0)
            {
                proveedorPaginacionDTO.First = true;
            }
            else if (numeroDePagina == totalPaginas)
            {
                proveedorPaginacionDTO.Last = true;
            }
            return proveedorPaginacionDTO;
        }

        [HttpGet("{id}", Name = "GetProveedores")]
        public async Task<ActionResult<ProveedoresDTO>> Get(int id)
        {
            var proveedor = await this.contexto.Proveedores.FirstOrDefaultAsync(x => x.codigoProveedor == id);
            if(proveedor == null)
            {
                return NotFound();
            }
            var proveedorDTO = mapper.Map<ProveedoresDTO>(proveedor);
            return proveedorDTO;
        }
        public async Task<ActionResult> Post([FromBody] ProveedoresCreacionDTO proveedorCreacion)
        {
            var proveedor = mapper.Map<Proveedores>(proveedorCreacion);
            contexto.Add(proveedor);
            await contexto.SaveChangesAsync();
            var proveedorDTO = mapper.Map<ProveedoresDTO>(proveedor);
            return new CreatedAtRouteResult("GetProveedores", new { id = proveedor.codigoProveedor }, proveedorDTO);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] ProveedoresCreacionDTO proveedorActualizar)
        {
            var proveedor = mapper.Map<Proveedores>(proveedorActualizar);
            proveedor.codigoProveedor = id;
            contexto.Entry(proveedor).State = EntityState.Modified;
            await contexto.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ProveedoresDTO>> Delete(int id)
        {
            var codigoProveedor = await contexto.Proveedores.Select(x => x.codigoProveedor)
                .FirstOrDefaultAsync(x => x == id);
            if (codigoProveedor == default(int))
            {
                return NotFound();
            }
            contexto.Remove(new Proveedores { codigoProveedor = id });
            await contexto.SaveChangesAsync();
            return NoContent();
        }
    }
}
