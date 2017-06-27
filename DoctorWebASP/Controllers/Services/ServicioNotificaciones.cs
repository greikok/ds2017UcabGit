using DoctorWebASP.Controllers.Helpers;
using DoctorWebASP.Models;
using DoctorWebASP.Models.Service;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoctorWebASP.Controllers.Services
{
    public class ServicioNotificaciones : IServicioNotificaciones
    {
        #region Instancia
        List<Notificacion> IServicioNotificaciones.ObtenerTodos(out int cantidadPaginas, string nombre, int pagina, int cantidadFilas)
        {
            var lista = new List<Notificacion>();
            var client = new RestClient(baseUrl: Utilidades.GetUrlBase("NotificacionService"));
            var cantidad = 0;
            try
            {
                var action = "ObtenerTodos";
                var request = new RestRequest(resource: action, method: Method.GET);

                if (!String.IsNullOrEmpty(nombre))
                    request.AddQueryParameter("nombre", nombre);
                request.AddQueryParameter("indice", pagina.ToString());
                request.AddQueryParameter("filas", cantidadFilas.ToString());

                var response = client.Execute(request);

                if (response != null && response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var datos = (JObject)JsonConvert.DeserializeObject(response.Content);
                    var resultado = datos["ObtenerTodosResult"].ToObject<ResultadoServicioPaginado<Notificacion>>();
                    if (resultado != null && resultado.SinProblemas)
                    {
                        cantidad = resultado.CantidadPaginas;
                        lista = resultado.Contenido.ToList();
                    }
                    else
                        throw new DoctorWebException(resultado.Mensaje);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            cantidadPaginas = cantidad;
            return lista;
        }

        Notificacion IServicioNotificaciones.Obtener(int codigo)
        {
            var client = new RestClient(baseUrl: Utilidades.GetUrlBase("NotificacionService"));
            try
            {
                var action = $"Obtener/{codigo}";
                var request = new RestRequest(resource: action, method: Method.GET);

                var response = client.Execute(request);

                if (response != null && response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var datos = (JObject)JsonConvert.DeserializeObject(response.Content);
                    var resultado = datos["ObtenerResult"].ToObject<ResultadoServicio<Notificacion>>();
                    if (resultado != null && resultado.SinProblemas)
                    {
                        return resultado.Contenido;
                    }
                    else
                        throw new DoctorWebException(resultado.Mensaje);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return null;
        }

        bool IServicioNotificaciones.Guardar(Notificacion notificacion, out string mensaje)
        {
            var lista = new List<Notificacion>();
            var client = new RestClient(baseUrl: Utilidades.GetUrlBase("NotificacionService"));
            try
            {
                var action = "Guardar";
                var request = new RestRequest(resource: action, method: Method.POST);
                var body = new { notificacion = notificacion };
                var json = JsonConvert.SerializeObject(body);

                request.AddHeader("Content-Type", "application/json");
                request.AddJsonBody(body);
                var response = client.Execute(request);

                if (response != null && response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var datos = (JObject)JsonConvert.DeserializeObject(response.Content);
                    var resultado = datos["GuardarResult"].ToObject<ResultadoProceso>();
                    if (resultado != null && resultado.SinProblemas)
                    {
                        mensaje = resultado.Mensaje;
                        return resultado.SinProblemas;
                    }
                    else
                        throw new DoctorWebException(resultado.Mensaje);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            mensaje = "El proceso no concluyo correctamente.";
            return false;
        }

        bool IServicioNotificaciones.Borrar(int codigo, out string mensaje)
        {
            var client = new RestClient(baseUrl: Utilidades.GetUrlBase("NotificacionService"));
            try
            {
                var action = $"Borrar/{codigo}";
                var request = new RestRequest(resource: action, method: Method.DELETE);

                var response = client.Execute(request);
                if (response != null && response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var datos = (JObject)JsonConvert.DeserializeObject(response.Content);
                    var resultado = datos["BorrarResult"].ToObject<ResultadoProceso>();
                    if (resultado != null && resultado.SinProblemas)
                    {
                        mensaje = resultado.Mensaje;
                        return resultado.SinProblemas;
                    }
                    else
                        throw new DoctorWebException(resultado.Mensaje);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            mensaje = "El proceso no concluyo correctamente.";
            return false;
        }
        #endregion
    }
}