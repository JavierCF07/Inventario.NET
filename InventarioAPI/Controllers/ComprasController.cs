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
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InventarioAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ComprasController : ControllerBase
    {
        private readonly InventarioDBContext contexto;
        private readonly IMapper mapper;

        public ComprasController(InventarioDBContext contexto, IMapper mapper)
        {
            this.contexto = contexto;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ComprasDTO>>> Get()
        {
            var compras = await contexto.Compras.ToListAsync();
            var comprasDTO = mapper.Map<List<ComprasDTO>>(compras);
            return comprasDTO;
        }

        [HttpGet("{numeroDePagina}", Name = "GetComprasPage")]
        [Route("page/{numeroDePagina}")]
        public async Task<ActionResult<CompraPaginacionDTO>> GetComprasPage(int numeroDePagina = 0)
        {
            int cantidadDeRegistros = 5;
            var compraPaginacionDTO = new CompraPaginacionDTO();
            var query = contexto.Compras.AsQueryable();
            int totalDeRegistros = query.Count();
            int totalPaginas = (int)Math.Ceiling((Double)totalDeRegistros / cantidadDeRegistros);
            compraPaginacionDTO.Number = numeroDePagina;
            var compras = await contexto.Compras
                .Include("Proveedores")
                .Skip(cantidadDeRegistros * (compraPaginacionDTO.Number))
                .Take(cantidadDeRegistros)
                .ToListAsync();
            compraPaginacionDTO.TotalPages = totalPaginas;
            compraPaginacionDTO.Content = mapper.Map<List<ComprasDTO>>(compras);

            if (numeroDePagina == 0)
            {
                compraPaginacionDTO.First = true;
            } else if (numeroDePagina == totalPaginas)
            {
                compraPaginacionDTO.Last = true;
            }
            return compraPaginacionDTO;
        }

        [HttpGet("{id}",Name = "GetCompras")]
        public async Task<ActionResult<ComprasDTO>> Get(int id)
        {
            var compras = await contexto.Compras.FirstOrDefaultAsync(x => x.idCompra == id);
            if(compras == null)
            {
                return NotFound();
            }
            var comprasDTO = mapper.Map<ComprasDTO>(compras);
            return comprasDTO;
        }

        public async Task<ActionResult> Post([FromBody]ComprasCreacionDTO comprasCreacion)
        {
            var compras = mapper.Map<Compras>(comprasCreacion);
            contexto.Add(compras);
            await contexto.SaveChangesAsync();
            var comprasDTO = mapper.Map<ComprasDTO>(compras);
            return new CreatedAtRouteResult("GetCompras", new { id = compras.idCompra }, comprasCreacion);
        }
        
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] ComprasCreacionDTO comprasActualizar)
        {
            var compras = mapper.Map<Compras>(comprasActualizar);
            compras.idCompra = id;
            contexto.Entry(compras).State = EntityState.Modified;
            await contexto.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ComprasDTO>> Delete(int id)
        {
            var idCompra = await contexto.Compras.Select(x => x.idCompra)
                .FirstOrDefaultAsync(x => x == id);
            if(idCompra == default(int))
            {
                return NotFound();
            }
            contexto.Remove(new Compras { idCompra = id });
            await contexto.SaveChangesAsync();
            return NoContent();
        }
    }
}
