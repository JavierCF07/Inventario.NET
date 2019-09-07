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
    public class ClientesController :ControllerBase
    {
        private readonly InventarioDBContext contexto;
        private readonly IMapper mapper;

        public ClientesController(InventarioDBContext contexto, IMapper mapper)
        {
            this.contexto = contexto;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClientesDTO>>> Get()
        {
            var clientes = await contexto.Clientes.ToListAsync();
            var clientesDTO = mapper.Map<List<ClientesDTO>>(clientes);
            return clientesDTO;
        }
        [HttpGet("{id}", Name = "GetClientes")]
        public async Task<ActionResult<ClientesDTO>> Get(string id)
        {
            var clientes = await contexto.Clientes.FirstOrDefaultAsync(x => x.nit == id);
            if(clientes == null)
            {
                return NotFound();
            }
            var clientesDTO = mapper.Map<ClientesDTO>(clientes);
            return clientesDTO;
        }
        public async Task<ActionResult> Post([FromBody] ClientesCreacionDTO clientesCreacion)
        {
            var clientes = mapper.Map<Clientes>(clientesCreacion);
            contexto.Add(clientes);
            await contexto.SaveChangesAsync();
            var clientesDTO = mapper.Map<ClientesDTO>(clientes);
            return new CreatedAtRouteResult("GetClientes", new { id = clientes.nit }, clientesDTO);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(string id, [FromBody] ClientesCreacionDTO clientesActualizar)
        {
            var clientes = mapper.Map<Clientes>(clientesActualizar);
            clientes.nit = id;
            contexto.Entry(clientes).State = EntityState.Modified;
            await contexto.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ClientesDTO>> Delete(string id)
        {
            var codigoClientes = await contexto.Clientes.Select(x => x.nit)
                .FirstOrDefaultAsync(x => x == id);
            if(codigoClientes == default(string))
            {
                return NotFound();
            }
            contexto.Remove(new Clientes { nit = id });
            await contexto.SaveChangesAsync();
            return NoContent();
        }
    }
}
