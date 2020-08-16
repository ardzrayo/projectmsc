using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema.Web.Models.Consumibles.OSVersion
{
    public class OSVersionViewModel
    {
        public int idversion { get; set; }
        public int idos { get; set; }
        public string osfamily { get; set; }
        public string osversion { get; set; }
        public string descripcion { get; set; }
        public bool estado { get; set; }
        //public virtual OSFamily osfamily { get; set; }
    }
}
