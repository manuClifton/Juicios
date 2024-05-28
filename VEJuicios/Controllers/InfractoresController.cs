using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using VEJuicios.Application.UseCases.InfractorAlta;
using VEJuicios.Application.UseCases.NotificacionAlta;
using VEJuicios.Presenters;

namespace VEJuicios.Controllers
{
    [ApiController]
    [Route("Infractores")]
    [EnableCors]
    public class InfractoresController : ControllerBase
    {

        private readonly IInfractorAltaUseCase _InfractorAltaUseCase;
        private readonly InfractorAltaPresenter _InfractorAltaPresenter;

        public InfractoresController(
            IInfractorAltaUseCase infractorAltaUseCase,
            InfractorAltaPresenter infractorAltaPresenter
            )
        {
            _InfractorAltaUseCase = infractorAltaUseCase;
            _InfractorAltaPresenter = infractorAltaPresenter;
        }

        [HttpPost("/InfractorAlta")]
        public async Task<ActionResult> InfractorAlta([FromForm] string xml)
        {
            int infractorId;
            try
            {
                await this._InfractorAltaUseCase.Execute(xml);
                infractorId = _InfractorAltaPresenter.InfractorId;
            }
            catch (Exception e)
            {
                return NotFound(e.Message.ToString());
            }
            return Ok(infractorId);
        }
    }
}
