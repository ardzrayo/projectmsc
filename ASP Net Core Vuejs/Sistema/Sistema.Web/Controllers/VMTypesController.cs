using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sistema.Datos;
using Sistema.Entidades.Consumibles;
using Sistema.Web.Models.Consumibles.VMType;

namespace Sistema.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VMTypesController : ControllerBase
    {
        private readonly DbContextSistema _context;

        public VMTypesController(DbContextSistema context)
        {
            _context = context;
        }

        // GET: api/VMTypes/Listar
        [HttpGet("[action]")]
        public async Task<IEnumerable<VMTypeViewModel>> Listar()
        {
            var vmtype = await _context.VMTypes.ToListAsync();
            return vmtype.Select(c => new VMTypeViewModel
            {
                idvmtype = c.idvmtype,
                vmtypename = c.vmtypename,
                vmtypedescription = c.vmtypedescription,
                vmtypeestado = c.vmtypeestado
            });
        }

        // GET: api/VMTypes/Select
        [HttpGet("[action]")]
        public async Task<IEnumerable<SelectViewModel>> Select()
        {
            var vmtype = await _context.VMTypes.Where(c => c.vmtypeestado == true).ToListAsync();
            return vmtype.Select(c => new SelectViewModel
            {
                idvmtype = c.idvmtype,
                vmtypename = c.vmtypename
            });
        }

        // GET: api/VMTypes/Mostrar/5
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> Mostrar([FromRoute] int id)
        {

            var vmtype = await _context.VMTypes.FindAsync(id);

            if (vmtype == null)
            {
                return NotFound();
            }

            return Ok(new VMTypeViewModel {
                idvmtype = vmtype.idvmtype,
                vmtypename = vmtype.vmtypename,
                vmtypedescription = vmtype.vmtypedescription,
                vmtypeestado = vmtype.vmtypeestado
            });
        }

        // PUT: api/VMTypes/Actualizar
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] ActualizarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (model.idvmtype <= 0)
            {
                return BadRequest();
            }

            var vmtype = await _context.VMTypes.FirstOrDefaultAsync(c => c.idvmtype == model.idvmtype);

            if (vmtype == null)
            {
                return NotFound();
            }

            vmtype.vmtypename = model.vmtypename;
            vmtype.vmtypedescription = model.vmtypedescription;

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

        // POST: api/VMTypes/Crear
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            VMType vMType = new VMType
            {
                vmtypename = model.vmtypename,
                vmtypedescription = model.vmtypedescription,
                vmtypeestado = true
            };
            _context.VMTypes.Add(vMType);
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

        // DELETE: api/VMTypes/Eliminar/5
        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> Eliminar([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var vMType = await _context.VMTypes.FindAsync(id);
            if (vMType == null)
            {
                return NotFound();
            }

            _context.VMTypes.Remove(vMType);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                return BadRequest();
            }            

            return Ok(vMType);
        }

        // PUT: api/VMTypes/Desactivar/5
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

            var vmtype = await _context.VMTypes.FirstOrDefaultAsync(c => c.idvmtype == id);

            if (vmtype == null)
            {
                return NotFound();
            }

            vmtype.vmtypeestado = false;

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

        // PUT: api/VMTypes/Activar/5
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

            var vmtype = await _context.VMTypes.FirstOrDefaultAsync(c => c.idvmtype == id);

            if (vmtype == null)
            {
                return NotFound();
            }

            vmtype.vmtypeestado = true;

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
        private bool VMTypeExists(int id)
        {
            return _context.VMTypes.Any(e => e.idvmtype == id);
        }
    }
}