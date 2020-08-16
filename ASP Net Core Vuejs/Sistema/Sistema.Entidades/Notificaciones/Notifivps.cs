using System;
using System.Collections.Generic;
using System.Text;

namespace Sistema.Entidades.Notificaciones
{
    public class Notifivps
    {
        public int idnotivps { get; set; }
        public int idvps { get; set; }
        public string vmname { get; set; }
        public int idclient { get; set; }
        public string clientname { get; set; }
        public string clientcontact { get; set; }
        public string emailcontact_tecnico { get; set; }
        public bool estado { get; set; }
        public ICollection<NotifiHisto> notifihisto { get; set; }
    }
}
