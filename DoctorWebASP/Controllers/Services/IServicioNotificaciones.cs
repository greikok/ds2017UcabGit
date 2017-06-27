﻿using DoctorWebASP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorWebASP.Controllers.Services
{
    public interface IServicioNotificaciones
    {
        List<Notificacion> ObtenerTodos(out int cantidadPaginas, string nombre, int pagina, int cantidadFilas);

        Notificacion Obtener(int codigo);

        bool Guardar(Notificacion notificacion, out string mensaje);
        
        bool Borrar(int codigo, out string mensaje);
    }
}
