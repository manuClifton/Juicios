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
    public class vNotificacionesAConfirmarRepository : IvNotificacionesAConfirmarRepository
    {
        private readonly SQLServerContext _context;

        public vNotificacionesAConfirmarRepository(SQLServerContext context) => this._context = context ?? throw new ArgumentNullException(nameof(context));

        public void Add(Domain.Model.Notificaciones.vNotificacionesAConfirmar entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(string id)
        {
            throw new NotImplementedException();
        }

        public void Update(Domain.Model.Notificaciones.vNotificacionesAConfirmar entity)
        {
            throw new NotImplementedException();
        }

        public bool GetCorrectoParaConfirmar(int notificacionId)
        {
            try
            {
                bool correctoParaConfirmar = false;
                bool cedulaOk = false;
                bool adjuntosOk = false;
                var notificacionesAConfirmar = this._context.VNotificacionesAConfirmar.Where(x => x.NotificacionId.Equals(notificacionId)).ToList();
                if (notificacionesAConfirmar != null)
                {
                    foreach (var notificacion in notificacionesAConfirmar)
                    {
                        if (notificacion.ArchivoTemporalRelacionadoCedula && cedulaOk == false)
                        {
                            cedulaOk = true;
                        }
                        if (notificacion.TipoNotificacionCodigo == "LIQUIDACION")
                        {
                            if (notificacion.ArchivoTemporalRelacionadoCedula == false && adjuntosOk == false)
                            {
                                adjuntosOk = true;
                            }
                        }
                        else
                        {
                            adjuntosOk = true;
                        }
                    }
                }
                if (cedulaOk && adjuntosOk)
                {
                    correctoParaConfirmar = true;
                }
                return correctoParaConfirmar;
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }

        public bool GetCorrectoParaGrabarYConfirmar(int notificacionId, int[] relacionadosYARelacionarID, string tipoNotificacion, int cedulaId)
        {
            try
            {
                bool correctoParaConfirmar = false;
                bool cedulaOk = false;
                bool adjuntosOk = false;
                //Se valida la existencia de una cédula
                if (cedulaId != 0)
                {
                    cedulaOk = true;
                }
                if (tipoNotificacion.ToUpper() == "TRASLADO LIQUIDACIÓN")
                {
                    //En caso de un TRASLADO LIQUIDACIÓN, se valida q exista algún adjunto q no sea la cédula
                    if (relacionadosYARelacionarID.Any(item => item != cedulaId))
                    {
                        adjuntosOk = true;
                    }
                }
                else
                {
                    adjuntosOk = true;
                }
                if (cedulaOk && adjuntosOk)
                {
                    correctoParaConfirmar = true;
                }
                return correctoParaConfirmar;
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }
    }
}
