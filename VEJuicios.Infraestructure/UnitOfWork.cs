using VEJuicios.Infrastructure;
using VEJuicios.Domain;
using System;
using System.Threading.Tasks;

namespace VEJuicios.Infrastructure
{
    public sealed class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly SQLServerContext _context;
        private bool _disposed;

        public UnitOfWork(SQLServerContext context)
        {
            this._context = context;
        }

        /// <inheritdoc />
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <inheritdoc />
        public async Task<int> Save()
        {
            int affectedRows = 0;
            try
            {
                affectedRows = await this._context
                    .SaveChangesAsync()
                    .ConfigureAwait(false);
            }
            catch (Exception e)
            {

            }
            return affectedRows;
        }

        private void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    this._context.Dispose();
                }
            }
            this._disposed = true;
        }
    }
}
