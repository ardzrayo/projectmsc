using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Sistema.Datos;
using Sistema.Web.Funciones;
using Microsoft.Extensions.DependencyInjection;

namespace Sistema.Web.Services
{
    public class WriteToFileHostedService : IHostedService, IDisposable
    {
        private readonly IServiceProvider _provider;
        private readonly IHostingEnvironment environment;
        private Timer timer;
        public WriteToFileHostedService(IHostingEnvironment environment, IServiceProvider serviceProvider)
        {
            this.environment = environment;
            _provider = serviceProvider;
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            //Inicia el evento FromSeconds(60))/FromDays(1);
            timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(60));
            return Task.CompletedTask;

        }
        private void DoWork(object state)
         {
            //var fechaActual = DateTime.Now;
            //var A = fechaActual.Year;
            //var M = fechaActual.Month;
            ////Realiza la opción principal
            //DateTime fechaEs = new DateTime(A, M, 19);
            //DateTime fechaEs2 = new DateTime(A, M, 12);
            //var resul = fechaActual.CompareTo(fechaEs);
            //if (fechaActual.ToShortDateString() == fechaEs.ToShortDateString())
            //{
            //    using (IServiceScope scope = _provider.CreateScope())
            //    {
            //        var context = scope.ServiceProvider.GetRequiredService<DbContextSistema>();
            //        var CorreosNotifi = context.Notifivpss.Where(x => x.estado == true).Select(x => x).Where(xt => xt.idnotivps > 0).Select(x => x.emailcontact_tecnico).ToList();
            //        //var Dia1 = context.Periodos.Where(x => x.idperiodo == 1).Select(x => x).Where(xt => xt.idperiodo > 0).Select(x => x.dia1);
            //        Correo.ConfigurarMail(CorreosNotifi, 1);
            //    }
            //}
            //else if (fechaActual.ToShortDateString() == fechaEs2.ToShortDateString())
            //{
            //    using (IServiceScope scope = _provider.CreateScope())
            //    {
            //        var context = scope.ServiceProvider.GetRequiredService<DbContextSistema>();
            //        var CorreosNotifi = context.Notifivpss.Where(x => x.estado == true).Select(x => x).Where(xt => xt.idnotivps > 0).Select(x => x.emailcontact_tecnico).ToList();
            //        Correo.ConfigurarMail(CorreosNotifi, 2);
            //    }
            //}
            //else
            //{
            //    Console.WriteLine("No paso nada");
            //}
            using (IServiceScope scope = _provider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<DbContextSistema>();
                var CorreosNotifi = context.Notifivpss.Where(x => x.estado == true).Select(x => x).Where(xt => xt.idnotivps > 0).Select(x => x.emailcontact_tecnico).ToList();
                var Dia1 = context.Periodos.Where(x => x.idperiodo == 1).Select(x => x).Where(xt => xt.idperiodo > 0).Select(x => x.dia1).SingleOrDefault();
                var Dia2 = context.Periodos.Where(x => x.idperiodo == 1).Select(x => x).Where(xt => xt.idperiodo > 0).Select(x => x.dia2).SingleOrDefault();

                var fechaActual = DateTime.Now;
                var A = fechaActual.Year;
                var M = fechaActual.Month;
                DateTime fechaEs = new DateTime(A, M, Dia1);
                DateTime fechaEs2 = new DateTime(A, M, Dia2);
                if (fechaActual.ToShortDateString() == fechaEs.ToShortDateString())
                {
                    Correo.ConfigurarMail(CorreosNotifi, 1);
                }
                else if (fechaActual.ToShortDateString() == fechaEs2.ToShortDateString())
                {
                    Correo.ConfigurarMail(CorreosNotifi, 2);
                }
                else
                {
                    Console.WriteLine("No paso nada");
                }
            }
        }
        public Task StopAsync(CancellationToken cancellationToken)
        {
            //Detiene el metodo
            timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }
        public void Dispose()
        {
            timer?.Dispose();
        }
    }
}
