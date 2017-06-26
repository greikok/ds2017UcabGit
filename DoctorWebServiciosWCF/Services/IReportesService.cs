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
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "IReportesService" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface IReportesService
    {
        [OperationContract]
        [WebGet(UriTemplate = "/Ejemplo?parametro={codigo}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        ResultadoProceso DoWork(string codigo);
    }
}
