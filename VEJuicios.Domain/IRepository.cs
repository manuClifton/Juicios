using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VEJuicios.Domain
{
    public interface IRepository<T>
    {
        void Add(T entity);
        void Update(T entity);
        void Delete(string id);
    }
}
