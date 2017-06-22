using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using DoctorWebServiciosWCF.Model;
using DoctorWebServiciosWCF.Models.Service;

namespace DoctorWebServiciosWCF.Services
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "NotificacionService" en el código, en svc y en el archivo de configuración a la vez.
    // NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione NotificacionService.svc o NotificacionService.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class NotificacionService : INotificacionService
    {
        private readonly NotificacionController controller = new NotificacionController();

        public ResultadoProceso Borrar(string codigo)
        {
            return controller.Borrar(codigo);
        }

        public ResultadoProceso EnviarEcho(string correo)
        {
            var resultado = new ResultadoProceso();
            try
            {
                var respuesta = controller.Obtener("Echo"); // id = 1

                if (respuesta.SinProblemas)
                {
                    respuesta.Contenido.Enviar("rasc.19@gmail.com");
                    resultado.SinProblemas = true;
                }
                else
                {
                    throw new Exception("Al obtener la notificacion, el proceso no concluyo correctamente.");
                }

            }
            catch (Exception ex)
            {
                resultado.Mensaje = ex.Message;
            }
            return resultado;
        }

        public ResultadoProceso EnviarSaludo(string correo, string nombre)
        {
            var resultado = new ResultadoProceso();
            try
            {
                var respuesta = controller.Obtener(2); // id = 1

                if (respuesta.SinProblemas)
                {
                    respuesta.Contenido.Enviar("rasc.19@gmail.com", new { nombre = nombre });
                    resultado.SinProblemas = true;
                }
                else
                {
                    throw new Exception("Al obtener la notificacion, el proceso no concluyo correctamente.");
                }

            }
            catch (Exception ex)
            {
                resultado.Mensaje = ex.Message;
            }
            return resultado;
        }

        public ResultadoProceso Guardar(Notificacion notificacion)
        {
            return controller.Guardar(notificacion);
        }

        public ResultadoServicio<Notificacion> Obtener(string codigo)
        {
            var resultado = new ResultadoServicio<Notificacion>();
            try
            {
                int id = 0;
                if (!int.TryParse(codigo, out id))
                    throw new FormatException("el codigo debe ser un numero.");

                return controller.Obtener(id);
            }
            catch (Exception ex)
            {
                resultado.Mensaje = ex.Message;
            }
            return resultado;
        }

        public ResultadoServicioPaginado<Notificacion> ObtenerTodos(string nombre, int pagina = 0, int numeroFilas = 30)
        {
            return controller.ObtenerTodos(nombre, pagina, numeroFilas);
        }

        public void Prueba(string codigo)
        {
            throw new NotImplementedException();
        }
    }
}
