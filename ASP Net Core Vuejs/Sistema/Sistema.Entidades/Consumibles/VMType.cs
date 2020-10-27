using Sistema.Entidades.Servidores;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Sistema.Entidades.Consumibles
{
    public class VMType
    {
        public int idvmtype { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string vmtypename { get; set; }
        public string vmtypedescription { get; set; }
        public bool vmtypeestado { get; set; }
        public ICollection<VPS> vps { get; set; }
    }
}
