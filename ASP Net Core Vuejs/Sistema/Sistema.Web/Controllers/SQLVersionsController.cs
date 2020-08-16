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
using Sistema.Web.Models.Consumibles.SQLVersion;

namespace Sistema.Web.Controllers
{
    [Authorize(Roles = "Administrador,Soporte")]
    [Route("api/[controller]")]
    [ApiController]
    public class SQLVersionsController : ControllerBase
    {
        private readonly DbContextSistema _context;

        public SQLVersionsController(DbContextSistema context)
        {
            _context = context;
        }

        // GET: api/SQLVersions/Listar
        [HttpGet("[action]")]
        public async Task<IEnumerable<SQLVersionViewModel>> Listar()
        {
            var sqlversion = await _context.SQLVersions.Include(c => c.sqlfamily).ToListAsync();
            return sqlversion.Select(c => new SQLVersionViewModel
            {
                idsqlversion = c.idsqlversion,
                idsql = c.idsql,
                sqlfamily=c.sqlfamily.mssql,
                mssqlversion = c.mssqlversion,
                mssqldescription = c.mssqldescription,
                estado = c.estado
            });
        }

        // GET: api/SQLVersions/Select
        [HttpGet("[action]")]
        public async Task<IEnumerable<SelectViewModel>> Select()
        {
            var sqlversion = await _context.SQLVersions.Where(c => c.estado == true).ToListAsync();
            return sqlversion.Select(c => new SelectViewModel
            {
                idsqlversion = c.idsqlversion,
                mssqlversion = c.mssqlversion
            });
        }

        // GET: api/SQLVersions/Mostrar/5
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> Mostrar([FromRoute] int id)
        {

            var sqlversion = await _context.SQLVersions.Include(c => c.sqlfamily).SingleOrDefaultAsync(c => c.idsqlversion == id);

            if (sqlversion == null)
            {
                return NotFound();
            }

            return Ok(new SQLVersionViewModel {
                idsqlversion = sqlversion.idsqlversion,
                idsql = sqlversion.idsql,
                sqlfamily=sqlversion.sqlfamily.mssql,
                mssqlversion = sqlversion.mssqlversion,
                mssqldescription = sqlversion.mssqldescription,
                estado = sqlversion.estado
            });
        }

        // PUT: api/SQLVersions/Actualizar
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] ActualizarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (model.idsqlversion <= 0)
            {
                return BadRequest();
            }

            var sqlversion = await _context.SQLVersions.FirstOrDefaultAsync(c => c.idsqlversion == model.idsqlversion);

            if (sqlversion == null)
            {
                return NotFound();
            }

            sqlversion.idsql = model.idsql;
            sqlversion.mssqlversion = model.mssqlversion;
            sqlversion.mssqldescription = model.mssqldescription;

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

        // POST: api/SQLVersions/Crear
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            SQLVersion sQLVersion = new SQLVersion
            {
                idsql = model.idsql,
                mssqlversion = model.mssqlversion,
                mssqldescription = model.mssqldescription,
                estado = true
            };
            _context.SQLVersions.Add(sQLVersion);
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

        // DELETE: api/SQLVersions/Eliminar/5
        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> Eliminar([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var sQLVersion = await _context.SQLVersions.FindAsync(id);
            if (sQLVersion == null)
            {
                return NotFound();
            }

            _context.SQLVersions.Remove(sQLVersion);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                return BadRequest();
            }            

            return Ok(sQLVersion);
        }

        // PUT: api/SQLVersions/Desactivar/1
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

            var sqlversion = await _context.SQLVersions.FirstOrDefaultAsync(c => c.idsqlversion == id);

            if (sqlversion == null)
            {
                return NotFound();
            }

            sqlversion.estado = false;

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
        // PUT: api/SQLVersions/Activar/1
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

            var sqlversion = await _context.SQLVersions.FirstOrDefaultAsync(c => c.idsqlversion == id);

            if (sqlversion == null)
            {
                return NotFound();
            }

            sqlversion.estado = true;

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

        private bool SQLVersionExists(int id)
        {
            return _context.SQLVersions.Any(e => e.idsqlversion == id);
        }
    }
}