using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using System;
using System.Threading;
using System.Threading.Tasks;

using WorkerMon.Client;
using WorkerMon.Client.Utils;

using VEJuicios.Workers.Common.Configurations;
using VEJuicios.Workers.Common.Contracts;

namespace VEJuicios.Workers.Common
{
    public abstract class WorkerBase<T> : BackgroundService, IWorkerBase<T> where T : class
    {
        protected readonly WorkerMonOptions _options;
        private readonly ILogger _logger;
        public bool Exec { get; set; }
        public DateTime StartAt { get; set; }

        protected WorkerBase(ILogger<T> logger, IOptions<WorkerMonOptions> workerMonOptions)
        {
            _options = workerMonOptions?.Value ?? throw new ArgumentNullException(nameof(workerMonOptions));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task Waiting(CancellationToken stoppingToken)
        {
            if (StartAt < DateTime.Now)
            {
                StartAt = DateTime.Now.AddSeconds(StringTimeUtil.StringToSeconds(_options.NextStartTime));
                _logger.LogInformation("New Start At: {0}", StartAt.ToShortTimeString());
            }

            (Exec, StartAt) = await WorkmonToken.SendIsAlive(_options.Uri, _options.WorkerId, Exec, StartAt, _options.SleepTime, _options.Tolerance, _options.MaxProcesingTime, _options.Secret);

            var delay = StringTimeUtil.StringToSeconds(_options.SleepTime) * 1000;
            await Task.Delay(delay, stoppingToken);
        }
    }
}
