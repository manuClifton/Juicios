using System.Runtime.Serialization;

namespace VEJuicios.Domain.Model
{
    [DataContract]
    public class RequestNotificaciones
    {
        [DataMember(Name = "idNotificaciones")]
        public string? idNotificaciones { get; set; }
        [DataMember(Name = "idAdjuntos")]
        public string? idAdjuntos{ get; set; }
        [DataMember(Name = "asunto")]
        public string? asunto { get; set; }
        [DataMember(Name = "cuit")]
        public string? cuit { get; set; }
        [DataMember(Name = "archivo")]
        public string? archivo { get; set; }
        [DataMember(Name = "metadata")]
        public string? metadata { get; set; }
        [DataMember(Name = "nombreDeArchivo")]
        public string? nombreDeArchivo { get; set; }
    }
}
