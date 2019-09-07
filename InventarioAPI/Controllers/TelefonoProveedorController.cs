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
    public class TelefonoProveedorController : ControllerBase
    {
        private readonly InventarioDBContext contexto;
        private readonly IMapper mapper;

        public TelefonoProveedorController(InventarioDBContext contexto, IMapper mapper)
        {
            this.contexto = contexto;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TelefonoProveedorDTO>>> Get()
        {
            var telefonoProveedor = await contexto.TelefonoProveedores.Include("Proveedores").ToListAsync();
            var telefonoProveedorDTO = mapper.Map<List<TelefonoProveedorDTO>>(telefonoProveedor);
            return telefonoProveedorDTO;
        }

        [HttpGet("{id}", Name = "GetTelefonoProveedor")]
        public async Task<ActionResult<TelefonoProveedorDTO>> Get(int id)
        {
            var telefonoProveedor = await contexto.TelefonoProveedores.FirstOrDefaultAsync(X => X.codigoTelefono == id);
            if(telefonoProveedor == null)
            {
                return NotFound();
            }
            var telefonoProveedorDTO = mapper.Map<TelefonoProveedorDTO>(telefonoProveedor);
            return telefonoProveedorDTO;
        }

        public async Task<ActionResult> Post([FromBody]TelefonoProveedorCreacionDTO telefonoCreacion)
        {
            var telefono = mapper.Map<TelefonoProveedor>(telefonoCreacion);
            contexto.Add(telefono);
            await contexto.SaveChangesAsync();
            var telefonoDTO = mapper.Map<TelefonoProveedorDTO>(telefono);
            return new CreatedAtRouteResult("GetTelefonoProveedor", new { id = telefono.codigoTelefono }, telefonoDTO);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody]TelefonoProveedorCreacionDTO telefonoActualizar)
        {
            var telefono = mapper.Map<TelefonoProveedor>(telefonoActualizar);
            telefono.codigoTelefono = id;
            contexto.Entry(telefono).State = EntityState.Modified;
            await contexto.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<TelefonoProveedorDTO>> Delete(int id)
        {
            var codigoTelefono = await contexto.TelefonoProveedores.Select(x => x.codigoTelefono)
                .FirstOrDefaultAsync(x => x == id);
            if(codigoTelefono == default(int))
            {
                return NotFound();
            }
            contexto.Remove(new TelefonoProveedor { codigoTelefono = id });
            await contexto.SaveChangesAsync();
            return NoContent();
        }
    }
}
