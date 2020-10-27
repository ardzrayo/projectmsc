using System.ComponentModel.DataAnnotations;

namespace Sistema.Entidades.Notificaciones
{
    public class ConfigMail
    {
        public int idperiodo { get; set; }
        [Required]
        public string nameperiodo { get; set; }
        public int dia { get; set; }
        [Required]
        public string asunto { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string cuerpomail { get; set; }
        public bool estado { get; set; }
    }
}
