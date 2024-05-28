using System;

namespace VEJuicios.Workers.Common.Contracts
{
    public interface IWorkerBase<T> where T : class
    {
        public bool Exec { get; set; }
        public DateTime StartAt { get; set; }
    }
}
