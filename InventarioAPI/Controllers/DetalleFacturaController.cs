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
    public class DetalleFacturaController : ControllerBase
    {
        private readonly InventarioDBContext contexto;
        private readonly IMapper mapper;

        public DetalleFacturaController(InventarioDBContext contexto, IMapper mapper)
        {
            this.contexto = contexto;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DetalleFacturaDTO>>> Get()
        {
            var detalle = await contexto.DetalleFacturas.ToListAsync();
            var detalleDTO = mapper.Map<List<DetalleFacturaDTO>>(detalle);
            return detalleDTO;
        }

        [HttpGet("{id}", Name = "GetDetalleFactura")]
        public async Task<ActionResult<DetalleFacturaDTO>> Get(int id)
        {
            var detalle = await contexto.DetalleFacturas.FirstOrDefaultAsync(x => x.codigoDetalle == id);
            if(detalle == null)
            {
                return NotFound();
            }
            var detalleDTO = mapper.Map<DetalleFacturaDTO>(detalle);
            return detalleDTO;
        }
        public async Task<ActionResult> Post([FromBody] DetalleFacturaCreacionDTO detalleFacturaCreacion)
        {
            var detalle = mapper.Map<DetalleFactura>(detalleFacturaCreacion);
            contexto.Add(detalle);
            await contexto.SaveChangesAsync();
            var detalleDTO = mapper.Map<DetalleFacturaDTO>(detalle);
            return new CreatedAtRouteResult("GetDetalleFactura", new { id = detalle.codigoDetalle }, detalleDTO);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] DetalleFacturaCreacionDTO detalleActualizar)
        {
            var detalle = mapper.Map<DetalleFactura>(detalleActualizar);
            detalle.codigoDetalle = id;
            contexto.Entry(detalle).State = EntityState.Modified;
            await contexto.SaveChangesAsync();
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<DetalleFacturaDTO>> Delete(int id)
        {
            var codigoDetalle = await contexto.DetalleFacturas.Select(x => x.codigoDetalle)
                .FirstOrDefaultAsync(x => x == id);
            if(codigoDetalle == default(int))
            {
                return NotFound();
            }
            contexto.Remove(new DetalleFactura { codigoDetalle = id });
            await contexto.SaveChangesAsync();
            return NoContent();
        } 
    }
}
