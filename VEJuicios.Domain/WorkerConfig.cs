namespace VEJuicios.Domain
{
    public class WorkerConfig
    {
        public int IntervalInMilliseconds { get; set; }
        public int RestartInMilliseconds { get; set; }
        public int MaxQueriesPerInterval { get; set; }
        public int MaxNotificationsInMemory { get; set; }
    }
}
