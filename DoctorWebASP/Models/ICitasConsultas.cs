using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using DoctorWebASP.Controllers;

namespace DoctorWebASP.Models
{
    public interface ICitasConsultas
    {
        CentroMedico ObtenerCentroMedico(int centroMedicoId);
        Paciente ObtenerPaciente(string userId);
        Calendario ObtenerCalendario(int calendarioId);
        void GuardarCita(Cita cita, Calendario calendario);
        string ObtenerUsuarioLoggedIn(CitasController citasController);
        Medico ObtenerMedico(string userId);
        List<Cita> ObtenerListaCitas(string userId);
        List<Cita> ObtenerCitasDoctor(string userId);
        SelectList ObtenerSelectListCentrosMedicos();
        CentroMedico ObtenerSingleCentroMedico(string centroMedico);
        CentroMedico ObtenerSingleCentroMedico(int centroMedicoId);
        SelectList ObtenerEsMedicasPorMedicosEnCentroMedico(CentroMedico cMedico);
        EspecialidadMedica ObtenerEspecialidadMedica(int espMedica);
        SelectList ObtenerSelectListMedicosQueTrabajanEnCentroMedico(int centroMedicoId, int espMedica);
        Cita ObtenerCita(int id);
        void EliminarCita(Cita cita, Calendario calendario);
    }
}
