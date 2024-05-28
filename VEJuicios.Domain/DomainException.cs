using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VEJuicios.Domain
{
    public class DomainException : Exception
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="DomainException" /> class.
        /// </summary>
        /// <param name="businessMessage">Message.</param>
        public DomainException(string businessMessage)
            : base(businessMessage)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="DomainException" /> class.
        /// </summary>
        public DomainException()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="DomainException" /> class.
        /// </summary>
        /// <param name="message">Message.</param>
        /// <param name="innerException">Inner Exception.</param>
        public DomainException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
