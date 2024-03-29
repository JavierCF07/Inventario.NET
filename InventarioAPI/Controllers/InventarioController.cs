﻿using AutoMapper;
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
    public class InventarioController : ControllerBase
    {
        private readonly InventarioDBContext contexto;
        private readonly IMapper mapper;

        public InventarioController(InventarioDBContext contexto, IMapper mapper)
        {
            this.contexto = contexto;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<InventarioDTO>>> Get()
        {
            var inventario = await contexto.Inventarios.ToListAsync();
            var inventarioDTO = mapper.Map<List<InventarioDTO>>(inventario);
            return inventarioDTO;
        }

        [HttpGet("{id}", Name = "GetInventario")]
        public async Task<ActionResult<InventarioDTO>> Get(int id)
        {
            var inventario = await contexto.Inventarios.FirstOrDefaultAsync(x => x.codigoProducto == id);
            if(inventario == null)
            {
                return NotFound();
            }
            var inventarioDTO = mapper.Map<InventarioDTO>(inventario);
            return inventarioDTO;
        }
        
        public async Task<ActionResult> Post([FromBody]InventarioCreacionDTO inventarioCreacion)
        {
            var inventario = mapper.Map<Inventario>(inventarioCreacion);
            contexto.Add(inventario);
            await contexto.SaveChangesAsync();
            var inventarioDTO = mapper.Map<InventarioDTO>(inventario);
            return new CreatedAtRouteResult("GetInventario", new { id = inventario.codigoInventario }, inventarioDTO);
        }

        public async Task<ActionResult> Put(int id, [FromBody] InventarioCreacionDTO inventarioActualizar)
        {
            var inventario = mapper.Map<Inventario>(inventarioActualizar);
            inventario.codigoInventario = id;
            contexto.Entry(inventario).State = EntityState.Modified;
            await contexto.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<InventarioDTO>>Delete(int id)
        {
            var codigoInventario = await contexto.Inventarios.Select(x => x.codigoInventario)
                .FirstOrDefaultAsync(x => x == id);
            if (codigoInventario == default(int))
            {
                return NotFound();
            }
            contexto.Remove(new Inventario { codigoInventario = id });
            await contexto.SaveChangesAsync();
            return NoContent();
        }
    }
}
