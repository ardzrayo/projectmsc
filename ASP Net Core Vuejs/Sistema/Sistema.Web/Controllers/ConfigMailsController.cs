using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sistema.Datos;
using Sistema.Web.Models.Notificaciones.ConfigMail;

namespace Sistema.Web.Controllers
{
    [Authorize(Roles = "Administrador,Gerente")]
    [Route("api/[controller]")]
    [ApiController]
    public class ConfigMailsController : ControllerBase
    {
        private readonly DbContextSistema _context;

        public ConfigMailsController(DbContextSistema context)
        {
            _context = context;
        }

        // GET: api/ConfigMails/Listar
        [HttpGet("[action]")]
        public async Task<IEnumerable<ConfigMailViewModel>> Listar()
        {
            var periodo = await _context.ConfigMails.ToListAsync();
            return periodo.Select(c => new ConfigMailViewModel
            {
                idperiodo = c.idperiodo,
                nameperiodo = c.nameperiodo,
                dia = c.dia,
                asunto = c.asunto,
                cuerpomail = c.cuerpomail,
                estado = c.estado
            });
        }

        // GET: api/ConfigMails/Mostrar/5
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> Mostrar([FromRoute] int id)
        {

            var configmail = await _context.ConfigMails.FindAsync(id);

            if (configmail == null)
            {
                return NotFound();
            }

            return Ok(new ConfigMailViewModel
            {
                idperiodo = configmail.idperiodo,
                nameperiodo = configmail.nameperiodo,
                dia = configmail.dia,
                asunto = configmail.asunto,
                cuerpomail = configmail.cuerpomail,
                estado = configmail.estado
            });
        }

        // PUT: api/ConfigMails/Actualizar
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] ActualizarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (model.idperiodo <= 0)
            {
                return BadRequest();
            }

            var configmail = await _context.ConfigMails.FirstOrDefaultAsync(c => c.idperiodo == model.idperiodo);

            if (configmail == null)
            {
                return NotFound();
            }

            configmail.dia = model.dia;
            configmail.asunto = model.asunto;
            configmail.cuerpomail = model.cuerpomail;

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

        // DELETE: api/ConfigMails/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteConfigMail([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var configMail = await _context.ConfigMails.FindAsync(id);
            if (configMail == null)
            {
                return NotFound();
            }

            _context.ConfigMails.Remove(configMail);
            await _context.SaveChangesAsync();

            return Ok(configMail);
        }

        private bool ConfigMailExists(int id)
        {
            return _context.ConfigMails.Any(e => e.idperiodo == id);
        }
    }
}