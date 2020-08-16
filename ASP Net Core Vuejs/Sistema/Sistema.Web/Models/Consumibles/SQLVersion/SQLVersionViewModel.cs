using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema.Web.Models.Consumibles.SQLVersion
{
    public class SQLVersionViewModel
    {
        public int idsqlversion { get; set; }
        public int idsql { get; set; }
        public string sqlfamily { get; set; }
        public string mssqlversion { get; set; }
        public string mssqldescription { get; set; }
        public bool estado { get; set; }
        //public virtual SQLFamily sqlfamily { get; set; }
    }
}
