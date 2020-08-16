using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Sistema.Entidades.Servidores
{
    public class Pools
    {
        public int idpool { get; set; }
        [Required(ErrorMessage = "Ingrese un tipo de maquina virtual.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "El nombre no debe tener más de 50 caracteres, ni menos de 3 caracteres.")]
        public string poolname { get; set; }
        public string pooldescripcion { get; set; }
        public bool poolestado { get; set; }
        public ICollection<VPS> vps { get; set; }
    }
}
