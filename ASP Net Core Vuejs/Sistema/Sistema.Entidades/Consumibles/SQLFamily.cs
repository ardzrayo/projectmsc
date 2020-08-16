using Sistema.Entidades.Servidores;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Sistema.Entidades.Consumibles
{
    public class SQLFamily
    {
        public int idsql { get; set; }
        [Required(ErrorMessage = "Ingrese un nombre SQL.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "El nombre no debe tener más de 50 caracteres, ni menos de 3 caracteres.")]
        public string mssql { get; set; }
        public bool estado { get; set; }
        public virtual ICollection<SQLVersion> sqlversion { get; set; }
        public ICollection<VPS> vps { get; set; }
    }
}
