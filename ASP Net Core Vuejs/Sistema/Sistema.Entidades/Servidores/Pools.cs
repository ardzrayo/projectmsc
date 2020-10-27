using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Sistema.Entidades.Servidores
{
    public class Pools
    {
        public int idpool { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string poolname { get; set; }
        public string pooldescripcion { get; set; }
        public bool poolestado { get; set; }
        public ICollection<VPS> vps { get; set; }
    }
}
