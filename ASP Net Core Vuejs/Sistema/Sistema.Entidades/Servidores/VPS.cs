using System;
using System.ComponentModel.DataAnnotations;
using Sistema.Entidades.Clientes;
using Sistema.Entidades.Consumibles;
using Sistema.Entidades.Usuarios;

namespace Sistema.Entidades.Servidores
{
    public class VPS
    {
        public int idvps { get; set; }
        public int idclient { get; set; }
        [Required]
        public string vmname { get; set; }
        public string vm_uuid { get; set; }
        public int vcpus { get; set; }
        public int ram { get; set; }
        public int hdisk { get; set; }
        public int bandw { get; set; }
        public int idnw { get; set; }
        public int idos { get; set; }
        public int idversion { get; set; }
        public int idsql { get; set; }
        public int idsqlversion { get; set; }
        public string internal_ip { get; set; }
        public string external_ip { get; set; }
        public DateTime createon { get; set; }
        public int idusuario { get; set; }
        public string dnsname { get; set; }
        public int idvmtype { get; set; }
        public int idpool { get; set; }
        public string notes { get; set; }
        public int? rmtaccesssal { get; set; }
        public bool estado { get; set; }
        public virtual VMClient vmclient { get; set; }
        public virtual NetworkBond networkbond { get; set; }
        public virtual OSFamily osfamily { get; set; }
        public virtual OSVersion osversion { get; set; }
        public virtual SQLFamily sqlfamily { get; set; }
        public virtual SQLVersion sqlversion { get; set; }
        public virtual Usuario usuario { get; set; }
        public virtual VMType vmtype { get; set; }
        public virtual Pools pools { get; set; }
    }
}