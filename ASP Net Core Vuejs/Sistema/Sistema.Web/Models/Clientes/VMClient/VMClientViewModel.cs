using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema.Web.Models.Clientes.VMClient
{
    public class VMClientViewModel
    {
        public int idclient { get; set; }
        public string clientname { get; set; }
        public string clientfullname { get; set; }
        public string clientemail { get; set; }
        public string clientphone { get; set; }
        public string clientcontact { get; set; }
        public string emailcontact_tecnico { get; set; }
        public bool estado { get; set; }
    }
}
