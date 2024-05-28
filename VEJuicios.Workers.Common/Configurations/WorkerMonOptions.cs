namespace VEJuicios.Workers.Common.Configurations
{
    public class WorkerMonOptions
    {
        public const string Section = "WorkerMon";
        public string WorkerId { get; set; }
        public string Uri { get; set; }
        public string Secret { get; set; }
        public string SleepTime { get; set; }
        public string Tolerance { get; set; }
        public string MaxProcesingTime { get; set; }
        public string NextStartTime { get; set; }

    }
}
