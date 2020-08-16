using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema.Web.Models.Consumibles.VMType
{
    public class VMTypeViewModel
    {
        public int idvmtype { get; set; }
        public string vmtypename { get; set; }
        public string vmtypedescription { get; set; }
        public bool vmtypeestado { get; set; }
    }
}
