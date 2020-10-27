using System;

namespace Sistema.Web.Models.Servidores.VPS
{
    public class CrearViewModel
    {
        public int idclient { get; set; }
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
    }
}
