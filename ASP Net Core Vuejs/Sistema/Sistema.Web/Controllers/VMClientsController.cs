using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sistema.Datos;
using Sistema.Entidades.Clientes;
using Sistema.Web.Models.Clientes.VMClient;

namespace Sistema.Web.Controllers
{
    [Authorize(Roles ="Administrador,Soporte,Gerente")]
    [Route("api/[controller]")]
    [ApiController]
    public class VMClientsController : ControllerBase
    {
        private readonly DbContextSistema _context;

        public VMClientsController(DbContextSistema context)
        {
            _context = context;
        }

        // GET: api/VMClients/Listar
        [HttpGet("[action]")]
        public async Task <IEnumerable<VMClientViewModel>> Listar()
        {
            var vmclient = await _context.VMClients.ToListAsync();
            return vmclient.Select(c => new VMClientViewModel
            {
                idclient = c.idclient,
                clientname = c.clientname,
                clientfullname = c.clientfullname,
                clientemail = c.clientemail,
                clientphone = c.clientphone,
                clientcontact = c.clientcontact,
                emailcontact_tecnico = c.emailcontact_tecnico,
                estado = c.estado
            });
        }

        // GET: api/VMClients/Select
        [HttpGet("[action]")]
        public async Task<IEnumerable<SelectViewModel>> Select()
        {
            var vmclient = await _context.VMClients.Where(c=>c.estado==true).ToListAsync();

            return vmclient.Select(c => new SelectViewModel
            {
                idclient = c.idclient,
                clientname = c.clientname
            });
        }

        // GET: api/VMClients/Mostrar/5
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> Mostrar([FromRoute] int id)
        {
            var vmclient = await _context.VMClients.FindAsync(id);

            if (vmclient == null)
            {
                return NotFound();
            }
            return Ok(new VMClientViewModel{
                idclient = vmclient.idclient,
                clientname = vmclient.clientname,
                clientfullname = vmclient.clientfullname,
                clientemail = vmclient.clientemail,
                clientphone = vmclient.clientphone,
                clientcontact = vmclient.clientcontact,
                emailcontact_tecnico = vmclient.emailcontact_tecnico,
                estado = vmclient.estado
            });
        }

        // PUT: api/VMClients/Actualizar
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] ActualizarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (model.idclient <= 0)
            {
                return BadRequest();
            }
            var vmclient = await _context.VMClients.FirstOrDefaultAsync(c => c.idclient == model.idclient);

            if (vmclient == null)
            {
                return NotFound();
            }
            vmclient.clientname = model.clientname;
            vmclient.clientfullname = model.clientfullname;
            vmclient.clientemail = model.clientemail;
            vmclient.clientphone = model.clientphone;
            vmclient.clientcontact = model.clientcontact;
            vmclient.emailcontact_tecnico = model.emailcontact_tecnico;
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

        // POST: api/VMClients/Crear
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            VMClient vMClient = new VMClient
            {
                clientname = model.clientname,
                clientfullname = model.clientfullname,
                clientemail = model.clientemail,
                clientphone = model.clientphone,
                clientcontact = model.clientcontact,
                emailcontact_tecnico = model.emailcontact_tecnico,
                estado = true
            };
            _context.VMClients.Add(vMClient);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                return BadRequest();
            }
            return Ok();
        }

        // DELETE: api/VMClients/Eliminar/5
        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> Eliminar([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var vMClient = await _context.VMClients.FindAsync(id);
            if (vMClient == null)
            {
                return NotFound();
            }

            _context.VMClients.Remove(vMClient);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                return BadRequest();
            }            

            return Ok(vMClient);
        }

        // PUT: api/VMClients/Desactivar/1
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Desactivar([FromRoute] int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }
            var vmclient = await _context.VMClients.FirstOrDefaultAsync(c => c.idclient == id);
            if (vmclient == null)
            {
                return NotFound();
            }
            vmclient.estado = false;
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

        // PUT: api/VMClients/Activar/1
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Activar([FromRoute] int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }
            var vmclient = await _context.VMClients.FirstOrDefaultAsync(c => c.idclient == id);
            if (vmclient == null)
            {
                return NotFound();
            }
            vmclient.estado = true;
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
        private bool VMClientExists(int id)
        {
            return _context.VMClients.Any(e => e.idclient == id);
        }
    }
}