using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace VEJuicios.Model
{
    [DataContract]
    public class RequestPdf
    {
        [DataMember(Name = "texto")]
        public string cadena { get; set; }
    }
}
