using DoctorWebServiciosWCF.Model;
using DoctorWebServiciosWCF.Models.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace DoctorWebServiciosWCF.Services
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "INotificacionService" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface INotificacionService
    {
        [OperationContract]
        [WebGet(UriTemplate = "/obtenerTodos?nombre={nombre}&indice={indicePagina}&filas={numeroFilas}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        ResultadoServicioPaginado<Notificacion> ObtenerTodos(string nombre, int indicePagina = 0, int numeroFilas = 30);

        [OperationContract]
        [WebGet(UriTemplate = "/obtener/{codigo}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        ResultadoServicio<Notificacion> Obtener(string codigo);

        [OperationContract]
        [WebInvoke(UriTemplate = "/guardar", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped)]
        ResultadoProceso Guardar(Notificacion notificacion);

        [OperationContract]
        [WebInvoke(UriTemplate = "/borrar/{codigo}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped, Method = "DELETE")]
        ResultadoProceso Borrar(string codigo);

        [OperationContract]
        [WebGet(UriTemplate = "/EnviarEcho?a={correo}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        ResultadoProceso EnviarEcho(string correo);

        [OperationContract]
        [WebGet(UriTemplate = "/EnviarSaludo?a={correo}&nombre={nombre}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        ResultadoProceso EnviarSaludo(string correo, string nombre);
    }
}
