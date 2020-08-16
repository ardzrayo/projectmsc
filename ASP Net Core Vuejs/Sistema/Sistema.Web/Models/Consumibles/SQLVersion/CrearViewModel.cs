using System.ComponentModel.DataAnnotations;

namespace Sistema.Web.Models.Consumibles.SQLVersion
{
    public class CrearViewModel
    {
        public int idsql { get; set; }
        [Required(ErrorMessage = "Ingrese un nombre de Network Bond.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "El nombre no debe tener más de 50 caracteres, ni menos de 3 caracteres.")]
        public string mssqlversion { get; set; }
        public string mssqldescription { get; set; }
    }
}
