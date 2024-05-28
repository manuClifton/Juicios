using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VEJuicios.Domain
{
    public interface IUnitOfWork
    {
        Task<int> Save();
    }
}
