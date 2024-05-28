using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VEJuicios.Application.UseCases.InfractorAlta
{
    public sealed class InfractorAltaInput
    {
        public string Nombre { get; set; }
        public string Cuit { get; set; }
        public int Ministerio { get; set; }
        public int TipoPersoneriaId { get; set; }
        public string DFDireccion { get; set; }
        public int DFProvinciaId { get; set; }
        public string DFPais { get; set; }
        public string DFCiudad { get; set; }
        public int DFCodPostal { get; set; }
        public int DFTel { get; set; }
        public int DFFormaIngreso { get; set; }
        public DateTime FechaCruceFiscal { get; set; }
        public string DCDireccion { get; set; }
        public int DCProvinciaId { get; set; }
        public string DCPais { get; set; }
        public string DCCiudad { get; set; }
        public int DCCodPostal { get; set; }
        public int DCTel { get; set; }
        public int DCFormaIngreso { get; set; }
        public DateTime FechaCruceConstituido { get; set; }
        public string DRDireccion { get; set; }
        public int DRProvinciaId { get; set; }
        public string DRPais { get; set; }
        public string DRCiudad { get; set; }
        public int DRCodPostal { get; set; }
        public int DRTel { get; set; }
        public int DRFormaIngreso { get; set; }
        public DateTime FechaCruceRealLegal { get; set; }
        public int DFEadherido { get; set; }
        public int DFEFormaIngreso { get; set; }
        public DateTime FechaCruceElectronico { get; set; }
        public string Email { get; set; }
        public string TipoDocumentoId { get; set; }
        public int NroDocumento { get; set; }
        public int CampoExtraDF { get; set; }
        public int CampoExtraDFE { get; set; }
        public int CampoExtraDC { get; set; }
        public int CampoExtraDR { get; set; }
        public DateTime FechaInsertHD { get; set; }
        public string Descripcion { get; set; }
        public string CuitVerificadoConAfip { get; set; }
        public int FormaIngresoDC { get; set; }
        public int FormaIngresoDF { get; set; }
        public int FormaIngresoDFE { get; set; }
        public int FormaIngresoDR { get; set; }
        public int OperadorId { get; set; }
    }
}
