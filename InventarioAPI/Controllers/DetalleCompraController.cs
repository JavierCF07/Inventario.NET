using AutoMapper;
using InventarioAPI.Contexts;
using InventarioAPI.Entities;
using InventarioAPI.Models;
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
    public class DetalleCompraController : ControllerBase
    {
        private readonly InventarioDBContext contexto;
        private readonly IMapper mapper;
        public DetalleCompraController(InventarioDBContext contexto, IMapper mapper)
        {
            this.contexto = contexto;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DetalleCompraDTO>>> Get()
        {
            var detalleCompra = await contexto.DetalleCompras.ToListAsync();
            var detalleCompraDTO = mapper.Map<List<DetalleCompraDTO>>(detalleCompra);
            return detalleCompraDTO;
        }

        [HttpGet("{id}", Name = "GetDetalleCompra")]
        public async Task<ActionResult<DetalleCompraDTO>>Get(int id)
        {
            var detalleCompra = await contexto.DetalleCompras.FirstOrDefaultAsync(x => x.idDetalle == id);
            if(detalleCompra == null)
            {
                return NotFound();
            }
            var detalleCompraDTO = mapper.Map<DetalleCompraDTO>(detalleCompra);
            return detalleCompraDTO;
        }

        public async Task<ActionResult> Post([FromBody]DetalleCompraCreacionDTO compraCreacion)
        {
            var detalleCompra = mapper.Map<DetalleCompra>(compraCreacion);
            contexto.Add(detalleCompra);
            await contexto.SaveChangesAsync();
            var detalleCompraDTO = mapper.Map<DetalleCompraDTO>(detalleCompra);
            return new CreatedAtRouteResult("GetDetalleCompra", new { id = detalleCompra.idDetalle}, detalleCompraDTO);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] DetalleCompraCreacionDTO DetalleCompraActualizar)
        {
            var DetalleCompra = mapper.Map<DetalleCompra>(DetalleCompraActualizar);
            DetalleCompra.idDetalle = id;
            contexto.Entry(DetalleCompra).State = EntityState.Modified;
            await contexto.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<DetalleCompraDTO>> Delete(int id)
        {
            var codigoDetalleCompra = await contexto.DetalleCompras.Select(x => x.idDetalle)
                .FirstOrDefaultAsync(x => x == id);
            if (codigoDetalleCompra == default(int))
            {
                return NotFound();
            }
            contexto.Remove(new DetalleCompra { idDetalle = id });
            await contexto.SaveChangesAsync();
            return NoContent();
        }
    }
}
