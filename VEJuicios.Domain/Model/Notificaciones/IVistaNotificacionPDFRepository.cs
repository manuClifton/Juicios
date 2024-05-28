using VEJuicios.Domain.Model;
using VEJuicios.Domain.Model.JuiciosNotificaciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VEJuicios.Domain.Notificaciones
{
    public interface IVistaNotificacionPDFRepository : IRepository<VistaNotificacionPDF>
    {
        VistaNotificacionPDF GetById(int Id);
    }
}
