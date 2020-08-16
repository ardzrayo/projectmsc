using Sistema.Entidades.Servidores;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Sistema.Entidades.Clientes
{
    public class VMClient
    {
        public int idclient { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string clientname { get; set; }
        public string clientfullname { get; set; }
        public string clientemail { get; set; }
        public string clientphone { get; set; }
        public string clientcontact { get; set; }
        [Required]
        [EmailAddress]
        public string emailcontact_tecnico { get; set; }
        public bool estado { get; set; }
        public ICollection<VPS> vps { get; set; }
    }
}
