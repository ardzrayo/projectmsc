using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema.Web.Models.Notificaciones.Periodo
{
    public class ActualizarViewModel
    {
        public int idperiodo { get; set; }
        public int dia1 { get; set; }
        public int dia2 { get; set; }
        public bool estado { get; set; }
    }
}
