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
    public class CategoriaController : ControllerBase
    {
        private readonly InventarioDBContext contexto;
        private readonly IMapper mapper;

        public CategoriaController(InventarioDBContext contexto, IMapper mapper)
        {
            this.contexto = contexto;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<CategoriaPaginacionDTO>> Get(int numeroDePaginas = 1, int cantidadDeRegistros = 5)
        {
            var categoriaPaginacionDTO = new CategoriaPaginacionDTO();
            var query = contexto.Categorias.AsQueryable();
            int totalDeRegistros = query.Count();
            int totalPaginas = (int)Math.Ceiling((Double)totalDeRegistros / cantidadDeRegistros);

            var categoria = await contexto.Categorias
                .Skip(cantidadDeRegistros * (numeroDePaginas - 1))
                .Take(cantidadDeRegistros)
                .ToListAsync();

            categoriaPaginacionDTO.Content = mapper.Map <List<CategoriaDTO>>(categoria);
            if (numeroDePaginas == 1)
            {
                categoriaPaginacionDTO.First = true;

            }else if (numeroDePaginas == totalPaginas)
            {
                categoriaPaginacionDTO.Last = true;
            }
            categoriaPaginacionDTO.Number = 0;
            return categoriaPaginacionDTO;
        }

        [HttpGet("{id}", Name = "GetCategoria")]
        public async Task<ActionResult<CategoriaDTO>> Get(int id)
        {
            var categoria = await contexto.Categorias.FirstOrDefaultAsync(x => x.codigoCategoria == id);
            if (categoria == null)
            {
                return NotFound();
            }
            var categoriaDTO = mapper.Map<CategoriaDTO>(categoria);
            return categoriaDTO;
        }

        public async Task<ActionResult> Post([FromBody]CategoriaCreacionDTO categoriaCreacion)
        {
            var categoria = mapper.Map<Categoria>(categoriaCreacion);
            contexto.Add(categoria);
            await contexto.SaveChangesAsync();
            var categoriaDTO = mapper.Map<CategoriaDTO>(categoria);
            return new CreatedAtRouteResult("GetCategoria", new { id = categoria.codigoCategoria }, categoriaDTO);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] CategoriaCreacionDTO categoriaActualizar)
        {
            var categoria = mapper.Map<Categoria>(categoriaActualizar);
            categoria.codigoCategoria = id;
            contexto.Entry(categoria).State = EntityState.Modified;
            await contexto.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<CategoriaDTO>> Delete(int id)
        {
            var codigoCategoria = await contexto.Categorias.Select(x => x.codigoCategoria)
                .FirstOrDefaultAsync(x => x == id);
            if(codigoCategoria == default(int))
            {
                return NotFound();
            }
            contexto.Remove(new Categoria { codigoCategoria = id });
            await contexto.SaveChangesAsync();
            return NoContent();
        }
    }
}
