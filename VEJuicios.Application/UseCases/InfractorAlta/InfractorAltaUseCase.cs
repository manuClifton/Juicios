using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VEJuicios.Application.UseCases.NotificacionAlta;
using VEJuicios.Domain.Model.Infractores;
using VEJuicios.Domain.Model.Notificaciones;

namespace VEJuicios.Application.UseCases.InfractorAlta
{
    public sealed class InfractorAltaUseCase : IInfractorAltaUseCase
    {
        private readonly IInfractorAltaStorePRepository _InfractorAltaStorePRepository;
        private readonly IInfractorAltaOutputPort _InfractorAltaOutputPort;
        public InfractorAltaUseCase(
            IInfractorAltaStorePRepository infractorAltaStorePRepository,
            IInfractorAltaOutputPort infractorAltaPresenterOutputPort)
        {
            _InfractorAltaStorePRepository = infractorAltaStorePRepository;
            _InfractorAltaOutputPort = infractorAltaPresenterOutputPort;
        }
        public async Task Execute(string xml)
        {
            try
            {
                int infractorId = await _InfractorAltaStorePRepository.AltaInfractor(xml);
                if (infractorId <= 0)
                {
                    throw new Exception("No se ha podido crear el infractor");
                }
                var output = new InfractorAltaOutput(infractorId);
                this._InfractorAltaOutputPort.Standard(output);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message.ToString());
            }
        }
    }
}
