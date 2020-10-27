using Sistema.Entidades.Servidores;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Sistema.Entidades.Consumibles
{
    public class OSFamily
    {
        public int idos { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string osfamilyname { get; set; }
        public bool estado { get; set; }
        public virtual ICollection<OSVersion> osversion { get; set; }
        public ICollection<VPS> vps { get; set; }
    }
}
