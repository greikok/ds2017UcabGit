using DoctorWebASP.Controllers.Helpers;
using DoctorWebASP.Models.Service;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DoctorWebASP.Models
{
    public class Notificacion
    {

        #region Estatico : Servicios Web
        public static List<Notificacion> ObtenerTodos(out int cantidadPaginas, string nombre = null, int pagina = 0, int cantidadFilas = 30)
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
                    if (resultado != null)
                    {
                        cantidad = resultado.CantidadPaginas;
                        lista = resultado.Contenido.ToList();
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            cantidadPaginas = cantidad;
            return lista;
        }

        internal static Notificacion Obtener(int codigo)
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
                    if (resultado != null)
                    {
                        
                        return resultado.Contenido;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return null;
        }

        internal static bool Guardar(Notificacion notificacion, out string mensaje)
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
                    if (resultado != null)
                    {
                        mensaje = resultado.Mensaje;
                        return resultado.SinProblemas;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            mensaje = "El proceso no concluyo correctamente.";
            return false;
        }

        internal static bool Borrar(int codigo, out string mensaje)
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
                    if (resultado != null)
                    {
                        mensaje = resultado.Mensaje;
                        return resultado.SinProblemas;
                    }
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

        #region Instancia
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
        #endregion
    }
}