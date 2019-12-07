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
    public class EmailClienteController : ControllerBase
    {
        private readonly InventarioDBContext contexto;
        private readonly IMapper mapper;

        public EmailClienteController(InventarioDBContext contexto, IMapper mapper)
        {
            this.contexto = contexto;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmailClienteDTO>>> Get()
        {
            var email = await contexto.EmailClientes.Include("Clientes").ToListAsync();
            var emailDTO = mapper.Map<List<EmailClienteDTO>>(email);
            return emailDTO;
        }

        [HttpGet("{numeroDePagina}", Name = "GetEmailClientesPage")]
        [Route("page/{numeroDePagina}")]
        public async Task<ActionResult<EmailClientePaginacionDTO>> GetEmailClientesPage(int numeroDePagina = 0)
        {
            int cantidadDeRegistros = 5;
            var emailClientePaginacionDTO = new EmailClientePaginacionDTO();
            var query = contexto.EmailClientes.AsQueryable();
            int totalDeRegistros = query.Count();
            int totalPaginas = (int)Math.Ceiling((Double)totalDeRegistros / cantidadDeRegistros);
            emailClientePaginacionDTO.Number = numeroDePagina;
            var email = await contexto.EmailClientes
                .Include("Clientes")
                .Skip(cantidadDeRegistros * (emailClientePaginacionDTO.Number))
                .Take(cantidadDeRegistros)
                .ToListAsync();
            emailClientePaginacionDTO.TotalPages = totalPaginas;
            emailClientePaginacionDTO.Content = mapper.Map<List<EmailClienteDTO>>(email);
            
            if (numeroDePagina == 0)
            {
                emailClientePaginacionDTO.First = true;
            }
            else if (numeroDePagina == totalPaginas)
            {
                emailClientePaginacionDTO.Last = true;
            }
            return emailClientePaginacionDTO;
        }

        [HttpGet("{id}", Name = "GetEmailCliente")]
        public async Task<ActionResult<EmailClienteDTO>> Get(int id)
        {
            var email = await contexto.EmailClientes.FirstOrDefaultAsync(x => x.codigoEmail == id);
            if (email == null)
            {
                return NotFound();
            }
            var emailDTO = mapper.Map<EmailClienteDTO>(email);
            return emailDTO;
        }

        public async Task<ActionResult> Post([FromBody]EmailClienteCreacionDTo emailCreacion)
        {
            var email = mapper.Map<EmailCliente>(emailCreacion);
            contexto.Add(email);
            await contexto.SaveChangesAsync();
            var emailDTO = mapper.Map<EmailClienteDTO>(email);
            return new CreatedAtRouteResult("GetEmailCliente", new { id = email.codigoEmail }, emailDTO);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody]EmailClienteCreacionDTo emailActualizar)
        {
            var email = mapper.Map<EmailCliente>(emailActualizar);
            email.codigoEmail = id;
            contexto.Entry(email).State = EntityState.Modified;
            await contexto.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<EmailClienteDTO>> Delete(int id)
        {
            var codigoEmail = await contexto.EmailClientes.Select(x => x.codigoEmail)
                .FirstOrDefaultAsync(x => x == id);
            if(codigoEmail == default(int))
            {
                return NotFound();
            }
            contexto.Remove(new EmailCliente { codigoEmail = id });
            await contexto.SaveChangesAsync();
            return NoContent();
        }
    }
}
