using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VEJuicios.Application.UseCases.Errors
{
    public class FaltanCamposObligatoriosException : Exception
    {
        public FaltanCamposObligatoriosException() : base() { }

        public FaltanCamposObligatoriosException(string message) : base(message) { }
        public FaltanCamposObligatoriosException(string message, Exception inner) : base(message, inner) { }
    }
}
