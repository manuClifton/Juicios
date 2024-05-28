using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VEJuicios.Domain.Model.Notificaciones;
using VEJuicios.Domain.Notificaciones;
using VEJuicios.Infrastructure;

namespace VEJuicios.Infraestructure.Repositories
{
    public class VNotificacionDatosParaGenerarCedulaRepository : IVNotificacionDatosParaGenerarCedulaRepository
    {
        private readonly SQLServerContext _context;
        public VNotificacionDatosParaGenerarCedulaRepository(SQLServerContext context) => this._context = context ?? throw new ArgumentNullException(nameof(context));


        public void Add(Domain.Model.Notificaciones.VNotificacionDatosParaGenerarCedula entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(string id)
        {
            throw new NotImplementedException();
        }

        public void Update(VNotificacionDatosParaGenerarCedulaRepository entity)
        {
            throw new NotImplementedException();
        }

        public void Update(VNotificacionDatosParaGenerarCedula entity)
        {
            throw new NotImplementedException();
        }

        VNotificacionDatosParaGenerarCedula IVNotificacionDatosParaGenerarCedulaRepository.GetById(int Id)
        {
            try
            {
                // SqlConnection.ClearAllPools();
                var elemento = this._context.VNotificacionDatosParaGenerarCedula.FirstOrDefault(x => x.NotificacionId.Equals(Id));
                return elemento;
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }
    }
}
