using Sistema.Entidades.Servidores;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Sistema.Entidades.Consumibles
{
    public class NetworkBond
    {
        public int idnw { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string nwbond { get; set; }
        public bool nwestado { get; set; }
        public ICollection<VPS> vps { get; set; }
    }
}
