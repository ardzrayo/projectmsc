using System.ComponentModel.DataAnnotations;

namespace Sistema.Web.Models.Clientes.VMClient
{
    public class CrearViewModel
    {
        [Required(ErrorMessage = "Ingrese un nombre de cliente.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "El nombre no debe tener más de 50 caracteres, ni menos de 3 caracteres.")]
        public string clientname { get; set; }
        public string clientfullname { get; set; }
        public string clientemail { get; set; }
        public string clientphone { get; set; }
        public string clientcontact { get; set; }
        [Required(ErrorMessage = "Ingrese un correo electronico de contacto técnico.")]
        [EmailAddress]
        public string emailcontact_tecnico { get; set; }
        public bool estado { get; set; }
    }
}
