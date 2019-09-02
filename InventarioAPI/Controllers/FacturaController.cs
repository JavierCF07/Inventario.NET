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
    public class FacturaController : ControllerBase
    {
        private readonly InventarioDBContext contexto;
        private readonly IMapper mapper;

        public FacturaController(InventarioDBContext contexto, IMapper mapper)
        {
            this.contexto = contexto;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FacturaDTO>>> Get()
        {
            var factura = await contexto.Facturas.ToListAsync();
            var facturaDTO = mapper.Map<List<FacturaDTO>>(factura);
            return facturaDTO;
        }

        [HttpGet("{id}", Name = "GetFactura")]
        public async Task<ActionResult<FacturaDTO>> Get(int id)
        {
            var factura = await contexto.Facturas.FirstOrDefaultAsync(x => x.numeroFactura == id);
            if(factura == null)
            {
                return NotFound();
            }
            var facturaDTO = mapper.Map<FacturaDTO>(factura);
            return facturaDTO;
        }
        public async Task<ActionResult> Post([FromBody]FacturaCreacionDTO facturaCreacion)
        {
            var factura = mapper.Map<Factura>(facturaCreacion);
            contexto.Add(factura);
            await contexto.SaveChangesAsync();
            var facturaDTO = mapper.Map<FacturaDTO>(factura);
            return new CreatedAtRouteResult("GetFactura", new { id = factura.numeroFactura }, facturaDTO);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] FacturaCreacionDTO facturaActualizar)
        {
            var factura = mapper.Map<Factura>(facturaActualizar);
            factura.numeroFactura = id;
            contexto.Entry(factura).State = EntityState.Modified;
            await contexto.SaveChangesAsync();
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<FacturaDTO>> Delete(int id)
        {
            var numerofactura = await contexto.Facturas.Select(x => x.numeroFactura)
                .FirstOrDefaultAsync(x => x == id);
            if(numerofactura == default(int))
            {
                return NotFound();
            }
            contexto.Remove(new Factura { numeroFactura = id });
            await contexto.SaveChangesAsync();
            return NoContent();
        }
    }
}
