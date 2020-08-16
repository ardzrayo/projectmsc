using System.ComponentModel.DataAnnotations;

namespace Sistema.Web.Models.Consumibles.SQLFamily
{
    public class ActualizarViewModel
    {
        [Required]
        public int idsql { get; set; }
        [Required(ErrorMessage = "Ingrese un nombre SQL.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "El nombre no debe tener más de 50 caracteres, ni menos de 3 caracteres.")]
        public string mssql { get; set; }
    }
}
