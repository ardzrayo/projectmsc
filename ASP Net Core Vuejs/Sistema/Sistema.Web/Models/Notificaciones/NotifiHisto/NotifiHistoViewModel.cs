using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema.Web.Models.Notificaciones.NotifiHisto
{
    public class NotifiHistoViewModel
    {
        public int idnotihisto { get; set; }
        public int idnotivps { get; set; }
        public string destinatario { get; set; }
        public int asunto { get; set; }
        public DateTime fecha { get; set; }
    }
}
