using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sistema.Datos;
using Sistema.Entidades.Consumibles;
using Sistema.Web.Models.Consumibles.OSVersion;

namespace Sistema.Web.Controllers
{
    [Authorize(Roles = "Administrador,Soporte")]
    [Route("api/[controller]")]
    [ApiController]
    public class OSVersionsController : ControllerBase
    {
        private readonly DbContextSistema _context;

        public OSVersionsController(DbContextSistema context)
        {
            _context = context;
        }

        // GET: api/OSVersions/Listar
        [HttpGet("[action]")]
        public async Task<IEnumerable<OSVersionViewModel>> Listar()
        {
            var osversion = await _context.OSVersions.Include(c => c.osfamily).ToListAsync();
            return osversion.Select(c => new OSVersionViewModel
            {
                idversion = c.idversion,
                idos = c.idos,
                osfamily=c.osfamily.osfamilyname,
                osversion = c.osversion,
                descripcion = c.descripcion,
                estado = c.estado
            });
        }

        // GET: api/OSVersions/Select
        [HttpGet("[action]")]
        public async Task<IEnumerable<SelectViewModel>> Select()
        {
            var osversion = await _context.OSVersions.Where(c => c.estado == true).ToListAsync();
            return osversion.Select(c => new SelectViewModel
            {
                idversion = c.idversion,
                osversion = c.osversion
            });
        }

        // GET: api/OSVersions/Mostrar/5
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> Mostrar([FromRoute] int id)
        {

            var osversion = await _context.OSVersions.Include(c => c.osfamily).SingleOrDefaultAsync(c=>c.idversion==id);

            if (osversion == null)
            {
                return NotFound();
            }

            return Ok(new OSVersionViewModel {
                idversion = osversion.idversion,
                idos = osversion.idos,
                osfamily=osversion.osfamily.osfamilyname,
                osversion = osversion.osversion,
                descripcion = osversion.descripcion,
                estado=osversion.estado
            });
        }

        // PUT: api/OSVersions/Actualizar
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] ActualizarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (model.idversion <= 0)
            {
                return BadRequest();
            }

            var osversion = await _context.OSVersions.FirstOrDefaultAsync(c => c.idversion == model.idversion);

            if (osversion == null)
            {
                return NotFound();
            }

            osversion.idos = model.idos;
            osversion.osversion = model.osversion;
            osversion.descripcion = model.descripcion;

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

        // POST: api/OSVersions/Crear
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            OSVersion oSVersion = new OSVersion
            {
                idos = model.idos,
                osversion = model.osversion,
                descripcion = model.descripcion,
                estado = true
            };
            _context.OSVersions.Add(oSVersion);
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

        // DELETE: api/OSVersions/Eliminar/5
        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> Eliminar([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var oSVersion = await _context.OSVersions.FindAsync(id);
            if (oSVersion == null)
            {
                return NotFound();
            }

            _context.OSVersions.Remove(oSVersion);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }            

            return Ok(oSVersion);
        }

        // PUT: api/OSVersions/Desactivar/1
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

            var osversion = await _context.OSVersions.FirstOrDefaultAsync(c => c.idversion == id);

            if (osversion == null)
            {
                return NotFound();
            }

            osversion.estado = false;

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
        // PUT: api/OSVersions/Activar/1
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

            var osversion = await _context.OSVersions.FirstOrDefaultAsync(c => c.idversion == id);

            if (osversion == null)
            {
                return NotFound();
            }

            osversion.estado = true;

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

        private bool OSVersionExists(int id)
        {
            return _context.OSVersions.Any(e => e.idversion == id);
        }
    }
}