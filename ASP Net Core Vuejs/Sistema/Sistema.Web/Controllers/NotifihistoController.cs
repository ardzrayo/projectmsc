using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sistema.Datos;
using Sistema.Entidades.Notificaciones;
using Sistema.Web.Models.Notificaciones.NotifiHisto;

namespace Sistema.Web.Controllers
{
    //[Authorize(Roles = "Administrador,Soporte,Gerente")]
    [Route("api/[controller]")]
    [ApiController]
    public class NotifihistoController : ControllerBase
    {
        private readonly DbContextSistema _context;

        public NotifihistoController(DbContextSistema context)
        {
            _context = context;
        }
        // GET: api/Notifihisto/Listar
        [HttpGet("[action]")]
        public async Task<IEnumerable<NotifiHistoViewModel>> Listar()
        {
            var notifihisto = await _context.NotifiHistos.ToListAsync();
            return notifihisto.Select(c => new NotifiHistoViewModel
            {
                idnotihisto = c.idnotihisto,
                idnotivps = c.idnotivps,
                destinatario = c.destinatario,
                asunto = c.asunto,
                fecha = c.fecha
            });
        }
        private bool NotifiHistoExists(int id)
        {
            return _context.NotifiHistos.Any(e => e.idnotihisto == id);
        }
    }
}