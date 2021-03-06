﻿using Sistema.Entidades.Servidores;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Sistema.Entidades.Consumibles
{
    public class SQLVersion
    {
        public int idsqlversion { get; set; }
        public int idsql { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string mssqlversion { get; set; }
        public string mssqldescription { get; set; }
        public bool estado { get; set; }
        public virtual SQLFamily sqlfamily { get; set; }
        public ICollection<VPS> vps { get; set; }
    }
}
