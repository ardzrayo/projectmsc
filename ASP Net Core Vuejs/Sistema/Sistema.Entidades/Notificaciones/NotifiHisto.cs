using System;
using System.Collections.Generic;
using System.Text;

namespace Sistema.Entidades.Notificaciones
{
    public class NotifiHisto
    {
        public int idnotihisto { get; set; }
        public int idnotivps { get; set; }
        public string destinatario { get; set; }
        public int asunto { get; set; }
        public DateTime fecha { get; set; }
        public virtual Notifivps notifivps { get; set; }
    }
}
