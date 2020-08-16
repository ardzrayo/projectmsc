using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Sistema.Entidades.Usuarios
{
    public class Rol
    {
        public int idrol { get; set; }
        [Required]
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public bool estado { get; set; }
        public virtual ICollection<Usuario> usuarios { get; set; }
    }
}
