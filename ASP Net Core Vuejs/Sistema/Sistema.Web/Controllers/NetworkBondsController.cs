using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sistema.Datos;
using Sistema.Entidades.Consumibles;
using Sistema.Web.Models.Consumibles.NetworkBond;

namespace Sistema.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NetworkBondsController : ControllerBase
    {
        private readonly DbContextSistema _context;

        public NetworkBondsController(DbContextSistema context)
        {
            _context = context;
        }

        // GET: api/NetworkBonds/Listar
        [Authorize(Roles = "Administrador,Soporte")]
        [HttpGet("[action]")]
        public async Task<IEnumerable<NetworkBondViewModel>> Listar()
        {
            var networkbond = await _context.NetworkBonds.ToListAsync();
            return networkbond.Select(c => new NetworkBondViewModel
            {
                idnw = c.idnw,
                nwbond = c.nwbond,
                nwestado = c.nwestado
            });
        }

        // GET: api/NetworkBonds/Select
        [HttpGet("[action]")]
        public async Task<IEnumerable<SelectViewModel>> Select()
        {
            var networkbond = await _context.NetworkBonds.Where(c => c.nwestado == true).ToListAsync();
            return networkbond.Select(c => new SelectViewModel
            {
                idnw = c.idnw,
                nwbond = c.nwbond
            });
        }

        // GET: api/NetworkBonds/Mostrar/5
        [Authorize(Roles = "Administrador,Soporte")]
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> Mostrar([FromRoute] int id)
        {

            var networkbond = await _context.NetworkBonds.FindAsync(id);

            if (networkbond == null)
            {
                return NotFound();
            }

            return Ok(new NetworkBondViewModel {
                idnw = networkbond.idnw,
                nwbond = networkbond.nwbond,
                nwestado = networkbond.nwestado
            });
        }

        // PUT: api/NetworkBonds/Actualizar/
        [Authorize(Roles = "Administrador,Soporte")]
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] ActualizarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (model.idnw <= 0)
            {
                return BadRequest();
            }

            var networkbond = await _context.NetworkBonds.FirstOrDefaultAsync(c => c.idnw == model.idnw);

            if (networkbond == null)
            {
                return NotFound();
            }

            networkbond.nwbond = model.nwbond;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                //Guardar Excepción
                return BadRequest();
            }

            return Ok();
        }

        // POST: api/NetworkBonds/Crear
        [Authorize(Roles = "Administrador,Soporte")]
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            NetworkBond networkBond = new NetworkBond
            {
                nwbond = model.nwbond,
                nwestado = true
            };
            _context.NetworkBonds.Add(networkBond);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }

            return Ok();
        }

        // DELETE: api/NetworkBonds/Eliminar/5
        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> Eliminar([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var networkBond = await _context.NetworkBonds.FindAsync(id);
            if (networkBond == null)
            {
                return NotFound();
            }

            _context.NetworkBonds.Remove(networkBond);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }

            return Ok(networkBond);
        }

        // PUT: api/NetworkBonds/Desactivar/1
        [Authorize(Roles = "Administrador,Soporte")]
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Desactivar([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id <= 0)
            {
                return BadRequest();
            }

            var networkbond = await _context.NetworkBonds.FirstOrDefaultAsync(c => c.idnw == id);

            if (networkbond == null)
            {
                return NotFound();
            }

            networkbond.nwestado = false;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                //Guardar Excepción
                return BadRequest();
            }

            return Ok();
        }
        // PUT: api/NetworkBonds/Activar/1
        [Authorize(Roles = "Administrador,Soporte")]
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Activar([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id <= 0)
            {
                return BadRequest();
            }

            var networkbond = await _context.NetworkBonds.FirstOrDefaultAsync(c => c.idnw == id);

            if (networkbond == null)
            {
                return NotFound();
            }

            networkbond.nwestado = true;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                //Guardar Excepción
                return BadRequest();
            }

            return Ok();
        }
        private bool NetworkBondExists(int id)
        {
            return _context.NetworkBonds.Any(e => e.idnw == id);
        }
    }
}