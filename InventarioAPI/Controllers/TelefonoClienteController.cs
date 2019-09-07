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
    public class TelefonoClienteController : ControllerBase
    {
        private readonly InventarioDBContext contexto;
        private readonly IMapper mapper;

        public TelefonoClienteController(InventarioDBContext contexto, IMapper mapper)
        {
            this.contexto = contexto;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TelefonoClienteDTO>>> Get()
        {
            var telefono = await contexto.TelefonoClientes.Include("Clientes").ToListAsync();
            var telefonoDTO = mapper.Map<List<TelefonoClienteDTO>>(telefono);
            return telefonoDTO;
        }

        [HttpGet("{id}", Name = "GetTelefonoCliente")]
        public async Task<ActionResult<TelefonoClienteDTO>> Get(int id)
        {
            var telefono = await contexto.TelefonoClientes.FirstOrDefaultAsync(x => x.codigoTelefono == id);
            if(telefono == null)
            {
                return NotFound();
            }
            var telefonoDTO = mapper.Map<TelefonoClienteDTO>(telefono);
            return telefonoDTO;
        }
        public async Task<ActionResult> Post([FromBody] TelefonoClienteCreacionDTO telefonoCreacion)
        {
            var telefono = mapper.Map<TelefonoCliente>(telefonoCreacion);
            contexto.Add(telefono);
            await contexto.SaveChangesAsync();
            var telefonoDTO = mapper.Map<TelefonoClienteDTO>(telefono);
            return new CreatedAtRouteResult("GetTelefonoCliente", new { id = telefono.codigoTelefono }, telefonoDTO);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] TelefonoClienteCreacionDTO telefonOActualizar)
        {
            var telefono = mapper.Map<TelefonoCliente>(telefonOActualizar);
            telefono.codigoTelefono = id;
            contexto.Entry(telefono).State = EntityState.Modified;
            await contexto.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<TelefonoClienteDTO>> Delete(int id)
        {
            var codigoTelefono = await contexto.TelefonoClientes.Select(x => x.codigoTelefono)
                .FirstOrDefaultAsync(x => x == id);
            if(codigoTelefono == default(int))
            {
                return NotFound();
            }
            contexto.Remove(new TelefonoCliente { codigoTelefono = id });
            await contexto.SaveChangesAsync();
            return NoContent();
        }
    }
}
