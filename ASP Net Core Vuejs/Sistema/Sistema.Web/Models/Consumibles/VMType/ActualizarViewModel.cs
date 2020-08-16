using System.ComponentModel.DataAnnotations;

namespace Sistema.Web.Models.Consumibles.VMType
{
    public class ActualizarViewModel
    {
        [Required]
        public int idvmtype { get; set; }
        [Required(ErrorMessage = "Ingrese un tipo de maquina virtual.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "El nombre no debe tener más de 50 caracteres, ni menos de 3 caracteres.")]
        public string vmtypename { get; set; }
        public string vmtypedescription { get; set; }
    }
}
