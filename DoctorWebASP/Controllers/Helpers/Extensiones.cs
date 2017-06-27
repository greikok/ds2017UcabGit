using DoctorWebASP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoctorWebASP.Controllers.Helpers
{
    public static class Extensiones
    {

        #region Grupo09
        public static void RellenarCombos(this NotificacionesController controller, Notificacion model)
        {
            controller.ViewData["NotificacionEstado"] = new SelectList(
                      new List<Object>{
                           new { value = NotificacionEstado.Disponible , text = NotificacionEstado.Disponible.ToString()  },
                           new { value = NotificacionEstado.Borrada , text = NotificacionEstado.Borrada.ToString()  }
                        },
                      "value",
                      "text",
                      model.Estado
                );
        }

        internal static void Actualizar(this Notificacion model, FormCollection collection)
        {

            if (collection.AllKeys.Contains("NotificacionId"))
                model.NotificacionId = int.Parse(collection["NotificacionId"]);
            model.Estado = (NotificacionEstado)Enum.Parse(typeof(NotificacionEstado), collection["Estado"]);
            model.Nombre = collection["Nombre"];
            model.Descripcion = collection["Descripcion"];
            model.Contenido = collection["Contenido"];
            model.Asunto = collection["Asunto"];
        }

        public static bool esPaginaActual(this ViewDataDictionary bag, MenuPaginas menu)
        {
            var paginaActual = bag[$"{IndiceViewBag.PaginaActual}"];
            if (paginaActual != null && paginaActual is MenuPaginas && ((MenuPaginas)paginaActual) == menu)
                return true;
            return false;
        }

        public static string AgregarClaseSi(this HtmlHelper bag, bool expresion, string si, string sino = "")
        {
            /*var paginaActual = bag[$"{EnumViewBagItems.PaginaActual}"];
            if (paginaActual != null && paginaActual is EnumMenuItems && ((EnumMenuItems)paginaActual) == menu)
                return true;
            return false;*/
            if (expresion)
                return si;
            return sino;
        }

        public static void indicarPaginaActual(this ViewDataDictionary bag, MenuPaginas menu)
        {
            bag[$"{IndiceViewBag.PaginaActual}"] = menu;
        }
        #endregion
    }
}