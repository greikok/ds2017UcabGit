using DoctorWebServiciosWCF.Controllers.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading;
using System.Web;

namespace DoctorWebServiciosWCF.Model
{
    public class Notificacion
    {
        public int NotificacionId { get; set; }
        [Required]
        public NotificacionEstado Estado { get; set; }
        [Required]
        [StringLength(60)]
        public string Nombre { get; set; }
        [Required]
        [StringLength(255)]
        public string Descripcion { get; set; }
        [Required]
        [StringLength(128)]
        public string Asunto { get; set; }
        [Required]
        public string Contenido { get; set; }

        public void Enviar(string destinatario, object parametros = null)
        {
            var direccion = new MailAddress(destinatario);
            this.Enviar(new[] { direccion }, parametros);
        }

        public void Enviar(string[] destinatarios, object parametros = null)
        {
            List<MailAddress> direcciones = new List<MailAddress>();
            if (destinatarios.Length == 0)
                throw new ArgumentException("Debe indicar al menos 1 destinatario");
            foreach (var destinatario in destinatarios)
            {
                direcciones.Add(new MailAddress(destinatario));
            }
            this.Enviar(direcciones.ToArray(), parametros);
        }

        private void Enviar(MailAddress[] destinatarios, object parametros = null)
        {
            ThreadPool.QueueUserWorkItem(new WaitCallback((obj) =>
            {
                try
                {
                    var host = Utilidades.ObtenerClave("SMTPServerHost");
                    var port = Utilidades.ObtenerClave("SMTPServerPost");
                    var fromName = Utilidades.ObtenerClave("SMTPFromName");
                    var user = Utilidades.ObtenerClave("SMTPUserId");
                    var pass = Utilidades.ObtenerClave("SMTPUserPassword");

                    if (!String.IsNullOrEmpty(host) &&
                        !String.IsNullOrEmpty(port) &&
                        !String.IsNullOrEmpty(fromName) &&
                        !String.IsNullOrEmpty(user) &&
                        !String.IsNullOrEmpty(pass))
                    {

                        using (SmtpClient client = new SmtpClient(host: host, port: int.Parse(port)))
                        {
                            var credential = new NetworkCredential(userName: user, password: pass);
                            client.UseDefaultCredentials = false;
                            client.Credentials = credential;
                            client.DeliveryMethod = SmtpDeliveryMethod.Network;
                            client.EnableSsl = true;

                            var from = new MailAddress(address: user, displayName: fromName);
                            
                            var message = new MailMessage();
                            message.From = from;
                            foreach(var destinatario in destinatarios) { 
                                message.To.Add(destinatario);
                            }
                            message.Subject = Asunto;

                            var contenido = Contenido.ColocarParametros(parametros);

                            message.Body = contenido;
                            message.IsBodyHtml = true;

                            client.Send(message);
                        }
                    }
                }
                catch (Exception ex)
                {
                    File.AppendAllLines("notificaciones.log", new[] { ex.Message, ex.StackTrace });
                }
            }));
        }
    }

    public enum NotificacionEstado : byte
    {
        Disponible, Borrada
    }

}