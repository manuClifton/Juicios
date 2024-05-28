using VEJuicios.Domain.Model;
using VEJuicios.Domain.Model.JuiciosNotificaciones;
using VEJuicios.Domain.Notificaciones;
using VEJuicios.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace VEJuicios.Infraestructure.Repositories
{

    public class VistaNotificacionPDFRepository : IVistaNotificacionPDFRepository
    {
        private readonly SQLServerContext _context;
        public VistaNotificacionPDFRepository(SQLServerContext context) => this._context = context ?? throw new ArgumentNullException(nameof(context));


        public void Add(VistaNotificacionPDF entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(string id)
        {
            throw new NotImplementedException();
        }

        public void Update(VistaNotificacionPDF entity)
        {
            throw new NotImplementedException();
        }

        public VistaNotificacionPDF GetById(int Id)
        {
            try
            {
               // SqlConnection.ClearAllPools();
                var elemento = this._context.VistaNotificacionesPDF.FirstOrDefault(x => x.NotificacionId.Equals(Id));
                return elemento;
            }
            catch (Exception e)
            {

                throw new Exception(e.ToString());
            }
        }




    }
}
