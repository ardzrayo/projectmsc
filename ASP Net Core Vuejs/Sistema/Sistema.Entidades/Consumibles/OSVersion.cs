using Sistema.Entidades.Servidores;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Sistema.Entidades.Consumibles
{
    public class OSVersion
    {
        public int idversion { get; set; }
        public int idos { get; set; }
        [Required(ErrorMessage = "Ingrese un nombre de Network Bond.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "El nombre no debe tener más de 50 caracteres, ni menos de 3 caracteres.")]
        public string osversion { get; set; }
        public string descripcion { get; set; }
        public bool estado { get; set; }
        public virtual OSFamily osfamily { get; set; }
        //public OSFamily osfamily { get; set; }
        public ICollection<VPS> vps { get; set; }
    }
}
