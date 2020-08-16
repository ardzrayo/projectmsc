using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema.Web.Models.Servidores.Pools
{
    public class PoolsViewModel
    {
        public int idpool { get; set; }
        public string poolname { get; set; }
        public string pooldescripcion { get; set; }
        public bool poolestado { get; set; }
    }
}
