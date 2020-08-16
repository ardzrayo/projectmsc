using System;
using System.Collections.Generic;
using System.Net.Mail;
namespace Sistema.Web.Funciones
{
    public class Correo
    {
        static SmtpClient smtp = new SmtpClient();
        public Correo()
        {
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new System.Net.NetworkCredential("angrdzrayo@gmail.com", "Die22coK");
            smtp.EnableSsl = true;
        }
        public static void ConfigurarMail(List<string> Receptores, int op)
        {
            try
            {
                Correo ec = new Correo();
                MailMessage mnj = new MailMessage();
                foreach (var correo in Receptores)
                //mnj.To.Add(new MailAddress(correo));
                mnj.Bcc.Add(new MailAddress(correo));
                mnj.From = new MailAddress("angrdzrayo@gmail.com", "BTU Cloud");
                switch (op)
                {
                    case 1:
                        {
                            mnj.Subject = "Notificación  BTU Cloud" + System.DateTime.Now.ToString();
                            mnj.Body = "Estimados<br/><br/>" +
                                       "Buen día, el motivo del mensaje presente es para comunicarles que no ha sido recibido el pago de renta de su servicio de Servidor Virtual Privado.<br/>" +
                                       "A partir del presente día usted cuenta con 5 días para realizar su pago antes de concluir con su periodo de pago, de lo contrario su servicio se vera suspendido.<br/><br/>"+
                                       "Saludos cordiales.<br/><br/>" +
                                       "Atte. BTU Cloud";
                            break;
                        }
                    case 2:
                        {
                            mnj.Subject = "Segundo correo" + System.DateTime.Now.ToString();
                            mnj.Body = "Estimados<br/><br/>" +
                                       "Buen día, el motivo del mensaje presente es para comunicarles que no ha sido recibido el pago de renta de su servicio de Servidor Virtual Privado.<br/>" +
                                       "A partir del presente día usted cuenta con 3 días para realizar su pago antes de concluir con su periodo de pago, de lo contrario su servicio se vera suspendido.<br/><br/>" +
                                       "Saludos cordiales.<br/>" +
                                       "Atte. BTU Cloud";
                            break;
                        }
                }
                mnj.IsBodyHtml = true;
                mnj.Priority = MailPriority.Normal;
                ec.EnviarMensaje(mnj);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        public void EnviarMensaje(MailMessage mensaje)
        {
            smtp.Send(mensaje);
        }
    }
}
