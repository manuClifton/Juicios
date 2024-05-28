using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using WorkerMon.Client;
using WorkerMon.Client.Utils;
using WorkerMon.Client.Model;

namespace Tramites.Workers.DNAS
{
    public class WorkBase<T> : BackgroundService, IWorkBase<T> where T : class
    {
        private string _uri;
        private string _sleepTime;
        private string _tolerance;
        private string _maxprocesing;
        private string _nextStartTime;

        private string _secret;

        public WorkBase(ILogger<T> logger, IConfiguration config)
        {
            this.Logger = logger;
            this.Config = config;

            _uri = Config.GetSection("WorkerMon:Uri").Value;
            _sleepTime = Config.GetSection("WorkerMon:SleepTime").Value;
            _tolerance = Config.GetSection("WorkerMon:Tolerance").Value;
            _maxprocesing = Config.GetSection("WorkerMon:MaxProcesingTime").Value;
            _nextStartTime = Config.GetSection("WorkerMon:NextStartTime").Value;

            _secret = Config.GetSection("WorkerMon:Secret").Value;
        }

        public ILogger<T> Logger { get; set; }
        public IConfiguration Config { get; set; }
        public bool Exec { get; set; }
        public DateTime StartAt { get; set; }
        public string WorkerId { get; set; }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            throw new NotImplementedException();
        }

        public async Task Waiting(CancellationToken stoppingToken)
        {

            if (StartAt < DateTime.Now)
            {
                //tiempo para volver a ejecutar
                StartAt = DateTime.Now.AddSeconds(StringTimeUtil.StringToSeconds(_nextStartTime));
                Logger.LogInformation("New Start At: {0}", StartAt.ToShortTimeString());
            }


            //(Exec, StartAt) = await WorkmonToken.SendIsAlive(_uri,WorkerId, Exec, StartAt, _sleepTime_seg);
            (Exec, StartAt) = await WorkmonToken.SendIsAlive(_uri, WorkerId, Exec, StartAt, _sleepTime, _tolerance, _maxprocesing, _secret);

            await Task.Delay(StringTimeUtil.StringToSeconds(_sleepTime) * 1000, stoppingToken); //tiempo de sleeping (work dead)
        }

    }
}