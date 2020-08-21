using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema.Web.Models.Notificaciones.ConfigMail
{
    public class ActualizarViewModel
    {
        public int idperiodo { get; set; }
        public string nameperiodo { get; set; }
        public int dia { get; set; }
        public string asunto { get; set; }
        public string cuerpomail { get; set; }
        public bool estado { get; set; }
    }
}
