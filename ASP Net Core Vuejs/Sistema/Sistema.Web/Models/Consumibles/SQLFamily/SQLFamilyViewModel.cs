using Sistema.Entidades.Consumibles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema.Web.Models.Consumibles.SQLFamily
{
    public class SQLFamilyViewModel
    {
        public int idsql { get; set; }
        public string mssql { get; set; }
        public bool estado { get; set; }
        //public virtual ICollection<SQLVersion> sqlversion { get; set; }
    }
}
