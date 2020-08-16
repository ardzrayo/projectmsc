using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sistema.Datos;
using Sistema.Entidades.Servidores;
using Sistema.Web.Models.Servidores.Pools;

namespace Sistema.Web.Controllers
{
    [Authorize(Roles = "Administrador,Soporte")]
    [Route("api/[controller]")]
    [ApiController]
    public class PoolssController : ControllerBase
    {
        private readonly DbContextSistema _context;

        public PoolssController(DbContextSistema context)
        {
            _context = context;
        }

        // GET: api/Poolss/Listar
        [HttpGet("[action]")]
        public async Task<IEnumerable<PoolsViewModel>> Listar()
        {
            var pools = await _context.Poolss.ToListAsync();
            return pools.Select(c => new PoolsViewModel
            {
                idpool = c.idpool,
                poolname = c.poolname,
                pooldescripcion = c.pooldescripcion,
                poolestado = c.poolestado
            });
        }

        // GET: api/Poolss/Select
        [HttpGet("[action]")]
        public async Task<IEnumerable<SelectViewModel>> Select()
        {
            var pool = await _context.Poolss.Where(c => c.poolestado == true).ToListAsync();
            return pool.Select(c => new SelectViewModel
            {
                idpool = c.idpool,
                poolname = c.poolname
            });
        }

        // GET: api/Poolss/Mostrar/5
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> Mostrar([FromRoute] int id)
        {

            var pools = await _context.Poolss.FindAsync(id);

            if (pools == null)
            {
                return NotFound();
            }

            return Ok(new PoolsViewModel {
                idpool = pools.idpool,
                poolname = pools.poolname,
                pooldescripcion = pools.pooldescripcion,
                poolestado = pools.poolestado
            });
        }

        // PUT: api/Poolss/Actualizar
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] ActualizarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (model.idpool <= 0)
            {
                return BadRequest();
            }

            var pool = await _context.Poolss.FirstOrDefaultAsync(c => c.idpool == model.idpool);

            if (pool == null)
            {
                return NotFound();
            }

            pool.poolname = model.poolname;
            pool.pooldescripcion = model.pooldescripcion;

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

        // POST: api/Poolss/Crear
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Pools pools = new Pools
            {
                poolname = model.poolname,
                pooldescripcion = model.pooldescripcion,
                poolestado = true
            };
            _context.Poolss.Add(pools);
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

        // DELETE: api/Poolss/Eliminar/5
        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> Eliminar([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var pools = await _context.Poolss.FindAsync(id);
            if (pools == null)
            {
                return NotFound();
            }

            _context.Poolss.Remove(pools);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }           

            return Ok(pools);
        }

        // PUT: api/Poolss/Desactivar/1
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

            var pool = await _context.Poolss.FirstOrDefaultAsync(c => c.idpool == id);

            if (pool == null)
            {
                return NotFound();
            }

            pool.poolestado = false;

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

        // PUT: api/Poolss/Activar/1
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

            var pool = await _context.Poolss.FirstOrDefaultAsync(c => c.idpool == id);

            if (pool == null)
            {
                return NotFound();
            }

            pool.poolestado = true;

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
        private bool PoolsExists(int id)
        {
            return _context.Poolss.Any(e => e.idpool == id);
        }
    }
}