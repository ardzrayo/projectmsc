using System;
using System.Collections.Generic;
using System.Text;

namespace Sistema.Entidades.Notificaciones
{
    public class ConfigMail
    {
        public int idperiodo { get; set; }
        public string nameperiodo { get; set; }
        public int dia { get; set; }
        public string asunto { get; set; }
        public string cuerpomail { get; set; }
        public bool estado { get; set; }
    }
}
