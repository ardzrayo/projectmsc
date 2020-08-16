using Sistema.Entidades.Consumibles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema.Web.Models.Consumibles.OSFamily
{
    public class OSFamilyViewModel
    {
        public int idos { get; set; }
        public string osfamilyname { get; set; }
        public bool estado { get; set; }
        //public virtual ICollection<OSVersion> osversion { get; set; }
    }
}
