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
using Sistema.Web.Models.Servidores.VPS;

namespace Sistema.Web.Controllers
{
    //[Authorize(Roles = "Administrador")]
    [Route("api/[controller]")]
    [ApiController]
    public class VPSsController : ControllerBase
    {
        private readonly DbContextSistema _context;

        public VPSsController(DbContextSistema context)
        {
            _context = context;
        }

        // GET: api/VPSs
        [HttpGet("[action]")]
        public async Task<IEnumerable<VPSViewModel>> Listar()
        {
            var vps = await _context.VPSs.
                Include(c => c.vmclient)
                .Include(c => c.networkbond)
                .Include(c => c.osfamily)
                .Include(c => c.osversion)
                .Include(c => c.sqlfamily)
                .Include(c => c.sqlversion)
                .Include(c => c.usuario)
                .Include(c => c.vmtype)
                .Include(c => c.pools)
                .OrderByDescending(c => c.idvps)
                .ToListAsync();

            return vps.Select(c => new VPSViewModel
            {
                idvps = c.idvps,
                idclient = c.idclient,
                vmclient = c.vmclient.clientname,
                vmname = c.vmname,
                vm_uuid = c.vm_uuid,
                vcpus = c.vcpus,
                ram = c.ram,
                hdisk = c.hdisk,
                bandw = c.bandw,
                idnw = c.idnw,
                networkbond = c.networkbond.nwbond,
                idos = c.idos,
                osfamily = c.osfamily.osfamilyname,
                idversion = c.idversion,
                osversion = c.osversion.osversion,
                idsql = c.idsql,
                sqlfamily = c.sqlfamily.mssql,
                idsqlversion =c.idsqlversion,
                sqlversion = c.sqlversion.mssqlversion,
                internal_ip = c.internal_ip,
                external_ip = c.external_ip,
                createon = c.createon,
                idusuario = c.idusuario,
                usuario = c.usuario.nombre,
                dnsname = c.dnsname,
                idvmtype = c.idvmtype,
                vmtype = c.vmtype.vmtypename,
                idpool = c.idpool,
                pools = c.pools.poolname,
                notes = c.notes,
                rmtaccesssal = c.rmtaccesssal,
                estado = c.estado
            });
        }

        // GET: api/VPSs/Mostrar/5
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> Mostrar([FromRoute] int id)
        {

            var vps = await _context.VPSs.FindAsync(id);

            if (vps == null)
            {
                return NotFound();
            }

            return Ok(new VPSViewModel {
                idvps = vps.idvps,
                idclient = vps.idclient,
                vmname = vps.vmname,
                vm_uuid = vps.vm_uuid,
                vcpus = vps.vcpus,
                ram = vps.ram,
                hdisk = vps.hdisk,
                bandw = vps.bandw,
                idnw = vps.idnw,
                idos = vps.idos,
                idversion = vps.idversion,
                idsql = vps.idsql,
                idsqlversion = vps.idsqlversion,
                internal_ip = vps.internal_ip,
                external_ip = vps.external_ip,
                createon = vps.createon,
                idusuario = vps.idusuario,
                dnsname = vps.dnsname,
                idvmtype = vps.idvmtype,
                idpool = vps.idpool,
                notes = vps.notes,
                rmtaccesssal = vps.rmtaccesssal,
                estado = vps.estado
            });
        }

        // PUT: api/VPSs/Actualizar
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] ActualizarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (model.idvps <= 0)
            {
                return BadRequest();
            }

            var vps = await _context.VPSs.FirstOrDefaultAsync(c => c.idvps == model.idvps);

            if (vps == null)
            {
                return NotFound();
            }

            vps.idclient = model.idclient;
            vps.vmname = model.vmname;
            vps.vm_uuid = model.vm_uuid;
            vps.vcpus = model.vcpus;
            vps.ram = model.ram;
            vps.hdisk = model.hdisk;
            vps.bandw = model.bandw;
            vps.idnw = model.idnw;
            vps.idos = model.idos;
            vps.idversion = model.idversion;
            vps.idsql = model.idsql;
            vps.idsqlversion = model.idsqlversion;
            vps.internal_ip = model.internal_ip;
            vps.external_ip = model.external_ip;
            //vps.createon = model.createon;
            vps.idusuario = model.idusuario;
            vps.dnsname = model.dnsname;
            vps.idvmtype = model.idvmtype;
            vps.idpool = model.idpool;
            vps.notes = model.notes;
            vps.rmtaccesssal = model.rmtaccesssal;

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

        // POST: api/VPSs/Crear
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var CreateON = DateTime.Now;

            VPS vPS = new VPS
            {
                idclient = model.idclient,
                vmname = model.vmname,
                vm_uuid = model.vm_uuid,
                vcpus = model.vcpus,
                ram = model.ram,
                hdisk = model.hdisk,
                bandw = model.bandw,
                idnw = model.idnw,
                idos = model.idos,
                idversion = model.idversion,
                idsql = model.idsql,
                idsqlversion = model.idsqlversion,
                internal_ip = model.internal_ip,
                external_ip = model.external_ip,
                createon = CreateON,
                idusuario = model.idusuario,
                dnsname = model.dnsname,
                idvmtype = model.idvmtype,
                idpool = model.idpool,
                notes = model.notes,
                rmtaccesssal = model.rmtaccesssal,
                estado = true
            };
            
            try
            {
                _context.VPSs.Add(vPS);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }

            return Ok();
        }

        // DELETE: api/VPSs/Eliminar/5
        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> Eliminar([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var vPS = await _context.VPSs.FindAsync(id);
            if (vPS == null)
            {
                return NotFound();
            }

            _context.VPSs.Remove(vPS);
            await _context.SaveChangesAsync();

            return Ok(vPS);
        }

        // PUT: api/VPSs/Desactivar/5
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Desactivar([FromRoute] int id)
        {
            

            if (id <= 0)
            {
                return BadRequest();
            }

            var vps = await _context.VPSs.FirstOrDefaultAsync(c => c.idvps == id);

            if (vps == null)
            {
                return NotFound();
            }

            vps.estado = false;

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

        // PUT: api/VPSs/Activar/5
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Activar([FromRoute] int id)
        {


            if (id <= 0)
            {
                return BadRequest();
            }

            var vps = await _context.VPSs.FirstOrDefaultAsync(c => c.idvps == id);

            if (vps == null)
            {
                return NotFound();
            }

            vps.estado = true;

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
        private bool VPSExists(int id)
        {
            return _context.VPSs.Any(e => e.idvps == id);
        }
    }
}