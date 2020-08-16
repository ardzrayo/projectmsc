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
using Sistema.Web.Models.Consumibles.OSFamily;

namespace Sistema.Web.Controllers
{
    [Authorize(Roles = "Administrador,Soporte")]
    [Route("api/[controller]")]
    [ApiController]
    public class OSFamilysController : ControllerBase
    {
        private readonly DbContextSistema _context;

        public OSFamilysController(DbContextSistema context)
        {
            _context = context;
        }

        // GET: api/OSFamilys/Listar
        [HttpGet("[action]")]
        public async Task<IEnumerable<OSFamilyViewModel>> Listar()
        {
            var osfamily = await _context.OSFamilys.ToListAsync();
            return osfamily.Select(c => new OSFamilyViewModel
            {
                idos = c.idos,
                osfamilyname = c.osfamilyname,
                estado=c.estado
            });
        }

        // GET: api/OSFamilys/Select
        [HttpGet("[action]")]
        public async Task<IEnumerable<SelectViewModel>> Select()
        {
            var osfamily = await _context.OSFamilys.Where(c=>c.estado==true).ToListAsync();
            return osfamily.Select(c => new SelectViewModel
            {
                idos = c.idos,
                osfamilyname = c.osfamilyname
            });
        }

        // GET: api/OSFamilys/Mostrar/5
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> Mostrar([FromRoute] int id)
        {

            var osfamily = await _context.OSFamilys.FindAsync(id);

            if (osfamily == null)
            {
                return NotFound();
            }

            return Ok(new OSFamilyViewModel {
                idos = osfamily.idos,
                osfamilyname = osfamily.osfamilyname,
                estado=osfamily.estado
            });
        }

        // PUT: api/OSFamilys/Actualizar
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] ActualizarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (model.idos <= 0)
            {
                return BadRequest();
            }

            var osfamily = await _context.OSFamilys.FirstOrDefaultAsync(c => c.idos == model.idos);

            if (osfamily == null)
            {
                return NotFound();
            }

            osfamily.osfamilyname = model.osfamilyname;

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

        // POST: api/OSFamilys/Crear
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            OSFamily oSFamily = new OSFamily
            {
                osfamilyname = model.osfamilyname,
                estado = true
            };
            _context.OSFamilys.Add(oSFamily);
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

        // DELETE: api/OSFamilys/Eliminar/5
        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> Eliminar([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var oSFamily = await _context.OSFamilys.FindAsync(id);
            if (oSFamily == null)
            {
                return NotFound();
            }

            _context.OSFamilys.Remove(oSFamily);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }

            return Ok(oSFamily);
        }

        // PUT: api/OSFamilys/Desactivar/1
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

            var osfamily = await _context.OSFamilys.FirstOrDefaultAsync(c => c.idos == id);

            if (osfamily == null)
            {
                return NotFound();
            }

            osfamily.estado = false;

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

            var osfamily = await _context.OSFamilys.FirstOrDefaultAsync(c => c.idos == id);

            if (osfamily == null)
            {
                return NotFound();
            }

            osfamily.estado = true;

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

        private bool OSFamilyExists(int id)
        {
            return _context.OSFamilys.Any(e => e.idos == id);
        }
    }
}