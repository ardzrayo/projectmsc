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
using Sistema.Web.Models.Consumibles.SQLFamily;

namespace Sistema.Web.Controllers
{
    [Authorize(Roles = "Administrador,Soporte")]
    [Route("api/[controller]")]
    [ApiController]
    public class SQLFamilysController : ControllerBase
    {
        private readonly DbContextSistema _context;

        public SQLFamilysController(DbContextSistema context)
        {
            _context = context;
        }

        // GET: api/SQLFamilys/Listar
        [HttpGet("[action]")]
        public async Task<IEnumerable<SQLFamilyViewModel>> Listar()
        {
            var sqlfamily = await _context.SQLFamilys.ToListAsync();
            return sqlfamily.Select(c => new SQLFamilyViewModel
            {
                idsql = c.idsql,
                mssql = c.mssql,
                estado = c.estado
            });
        }

        // GET: api/SQLFamilys/Select
        [HttpGet("[action]")]
        public async Task<IEnumerable<SelectViewModel>> Select()
        {
            var sqlfamily = await _context.SQLFamilys.Where(c => c.estado == true).ToListAsync();
            return sqlfamily.Select(c => new SelectViewModel
            {
                idsql = c.idsql,
                mssql = c.mssql
            });
        }

        // GET: api/SQLFamilys/Mostrar/5
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> Mostrar([FromRoute] int id)
        {

            var sqlfamily = await _context.SQLFamilys.FindAsync(id);

            if (sqlfamily == null)
            {
                return NotFound();
            }

            return Ok(new SQLFamilyViewModel {
                idsql = sqlfamily.idsql,
                mssql = sqlfamily.mssql,
                estado = sqlfamily.estado
            });
        }

        // PUT: api/SQLFamilys/Actualizar
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] ActualizarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (model.idsql <= 0)
            {
                return BadRequest();
            }

            var sqlfamily = await _context.SQLFamilys.FirstOrDefaultAsync(c => c.idsql == model.idsql);

            if (sqlfamily == null)
            {
                return NotFound();
            }

            sqlfamily.mssql = model.mssql;

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

        // POST: api/SQLFamilys/Crear
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            SQLFamily sQLFamily = new SQLFamily
            {
                mssql = model.mssql,
                estado = true
            };
            _context.SQLFamilys.Add(sQLFamily);
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

        // DELETE: api/SQLFamilys/Eliminar/5
        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> Eliminar([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var sQLFamily = await _context.SQLFamilys.FindAsync(id);
            if (sQLFamily == null)
            {
                return NotFound();
            }

            _context.SQLFamilys.Remove(sQLFamily);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                return BadRequest();
            }           

            return Ok(sQLFamily);
        }

        // PUT: api/SQLFamilys/Desactivar/1
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

            var sqlfamily = await _context.SQLFamilys.FirstOrDefaultAsync(c => c.idsql == id);

            if (sqlfamily == null)
            {
                return NotFound();
            }

            sqlfamily.estado = false;

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
        // PUT: api/SQLFamilys/Activar/1
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

            var sqlfamily = await _context.SQLFamilys.FirstOrDefaultAsync(c => c.idsql == id);

            if (sqlfamily == null)
            {
                return NotFound();
            }

            sqlfamily.estado = true;

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

        private bool SQLFamilyExists(int id)
        {
            return _context.SQLFamilys.Any(e => e.idsql == id);
        }
    }
}