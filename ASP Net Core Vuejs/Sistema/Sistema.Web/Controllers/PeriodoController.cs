using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sistema.Datos;
using Microsoft.AspNetCore.Authorization;
using Sistema.Web.Models.Notificaciones.Periodo;

namespace Sistema.Web.Controllers
{
    //[Authorize(Roles = "Administrador,Gerente")]
    [Route("api/[controller]")]
    [ApiController]
    public class PeriodoController : ControllerBase
    {
        private readonly DbContextSistema _context;
        public PeriodoController(DbContextSistema context)
        {
            _context = context;
        }

        // GET: api/Periodo/Listar
        [HttpGet("[action]")]
        public async Task<IEnumerable<PeriodoViewModel>> Listar()
        {
            var periodo = await _context.Periodos.ToListAsync();
            return periodo.Select(c => new PeriodoViewModel
            {
                idperiodo = c.idperiodo,
                dia1 = c.dia1,
                dia2 = c.dia2,
                estado = c.estado
            });
        }

        // GET: api/Periodo/Mostrar/5
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> Mostrar([FromRoute] int id)
        {

            var periodo = await _context.Periodos.FindAsync(id);

            if (periodo == null)
            {
                return NotFound();
            }

            return Ok(new PeriodoViewModel
            {
                idperiodo = periodo.idperiodo,
                dia1 = periodo.dia1,
                dia2 = periodo.dia2,
                estado = periodo.estado
            });
        }

        // PUT: api/Periodo/Actualizar
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

            var periodo = await _context.Periodos.FirstOrDefaultAsync(c => c.idperiodo == model.idperiodo);

            if (periodo == null)
            {
                return NotFound();
            }

            periodo.dia1 = model.dia1;
            periodo.dia2 = model.dia2;

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

    }
}
