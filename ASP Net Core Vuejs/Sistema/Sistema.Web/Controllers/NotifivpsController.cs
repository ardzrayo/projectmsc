using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sistema.Datos;
using Sistema.Web.Models.Notificaciones.Notifivps;

namespace Sistema.Web.Controllers
{
    //[Authorize(Roles = "Administrador,Soporte,Gerente")]
    [Route("api/[controller]")]
    [ApiController]
    public class NotifivpsController : ControllerBase
    {
        private readonly DbContextSistema _context;
        public NotifivpsController(DbContextSistema context)
        {
            _context = context;
        }
        // GET: api/Notifivps/Listar
        [HttpGet("[action]")]
        public async Task<IEnumerable<NotifivpsViewModel>> Listar()
        {
            var notifivps = await _context.Notifivpss.ToListAsync();
            //var CorreosNotifi = await _context.Notifivpss.Where(x => x.estado == true).Select(x => x).Where(xt => xt.idnotivps > 0).Select(x => x.emailcontact_tecnico).ToListAsync();
            return notifivps.Select(c => new NotifivpsViewModel
            {
                idnotivps = c.idnotivps,
                idvps = c.idvps,
                vmname = c.vmname,
                idclient = c.idclient,
                clientname = c.clientname,
                clientcontact = c.clientcontact,
                emailcontact_tecnico = c.emailcontact_tecnico,
                estado = c.estado
            });      
        }
        // GET: api/Notifivps/Select
        [HttpGet("[action]")]
        public async Task<IEnumerable<SelectViewModel>> Select()
        {
            var notifivps = await _context.Notifivpss.Where(c => c.estado == true).ToListAsync();

            return notifivps.Select(c => new SelectViewModel
            {
                idnotivps = c.idnotivps,
                vmname = c.vmname
            });
        }
        // GET: api/Notifivps/Mostrar/5
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> Mostrar([FromRoute] int id)
        {
            var notifivps = await _context.Notifivpss.FindAsync(id);

            if (notifivps == null)
            {
                return NotFound();
            }
            return Ok(new NotifivpsViewModel
            {
                idnotivps=notifivps.idnotivps,
                idvps=notifivps.idvps,
                vmname=notifivps.vmname,
                idclient = notifivps.idclient,
                clientname = notifivps.clientname,
                clientcontact = notifivps.clientcontact,
                emailcontact_tecnico = notifivps.emailcontact_tecnico,
                estado = notifivps.estado
            });
        }
        // PUT: api/Notifivps/Desactivar/1
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Desactivar([FromRoute] int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }
            var notifivps = await _context.Notifivpss.FirstOrDefaultAsync(c => c.idnotivps == id);
            if (notifivps == null)
            {
                return NotFound();
            }
            notifivps.estado = false;
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
        // PUT: api/Notifivps/Activar/1
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Activar([FromRoute] int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }
            var notifivps = await _context.Notifivpss.FirstOrDefaultAsync(c => c.idnotivps == id);
            if (notifivps == null)
            {
                return NotFound();
            }
            notifivps.estado = true;
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
        //Verifica si existe el registro
        private bool NotifivpsExists(int id)
        {
            return _context.Notifivpss.Any(e => e.idnotivps == id);
        }
    }
}