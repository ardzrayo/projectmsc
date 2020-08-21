using Microsoft.EntityFrameworkCore;
using Sistema.Datos.Mapping.Clientes;
using Sistema.Datos.Mapping.Consumibles;
using Sistema.Datos.Mapping.Notificaciones;
using Sistema.Datos.Mapping.Servidores;
using Sistema.Datos.Mapping.Usuarios;
using Sistema.Entidades.Clientes;
using Sistema.Entidades.Consumibles;
using Sistema.Entidades.Notificaciones;
using Sistema.Entidades.Servidores;
using Sistema.Entidades.Usuarios;

namespace Sistema.Datos
{
    public class DbContextSistema : DbContext
    {
        public DbSet<VMClient> VMClients { get; set; }
        public DbSet<NetworkBond> NetworkBonds { get; set; }
        public DbSet<OSFamily> OSFamilys { get; set; }
        public DbSet<OSVersion> OSVersions { get; set; }
        public DbSet<SQLFamily> SQLFamilys { get; set; }
        public DbSet<SQLVersion> SQLVersions { get; set; }
        public DbSet<VMType> VMTypes { get; set; }
        public DbSet<Pools> Poolss { get; set; }
        public DbSet<VPS> VPSs { get; set; }
        public DbSet<Rol> Rols { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Notifivps> Notifivpss { get; set; }
        public DbSet<NotifiHisto> NotifiHistos { get; set; }
        public DbSet<Periodo> Periodos { get; set; }
        public DbSet<ConfigMail> ConfigMails { get; set; }
        public DbContextSistema(DbContextOptions<DbContextSistema> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new VMClientMap());
            modelBuilder.ApplyConfiguration(new NetworkBondMap());
            modelBuilder.ApplyConfiguration(new OSFamilyMap());
            modelBuilder.ApplyConfiguration(new OSVersionMap());
            modelBuilder.ApplyConfiguration(new SQLFamilyMap());
            modelBuilder.ApplyConfiguration(new SQLVersionMap());
            modelBuilder.ApplyConfiguration(new VMTypeMap());
            modelBuilder.ApplyConfiguration(new PoolsMap());
            modelBuilder.ApplyConfiguration(new VPSMap());
            modelBuilder.ApplyConfiguration(new RolMap());
            modelBuilder.ApplyConfiguration(new UsuarioMap());
            modelBuilder.ApplyConfiguration(new NotifivpsMap());
            modelBuilder.ApplyConfiguration(new NotifiHistoMap());
            modelBuilder.ApplyConfiguration(new PeriodoMap());
            modelBuilder.ApplyConfiguration(new ConfigMailMap());
        }
    }
}
