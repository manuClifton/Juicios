using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VEJuicios.Application.UseCases.InfractorAlta
{
    public sealed class InfractorAltaOutput : Output
    {
        public int InfractorId { get; }
        public InfractorAltaOutput(int infractorId)
        {
            InfractorId = infractorId;
        }
    }
}
