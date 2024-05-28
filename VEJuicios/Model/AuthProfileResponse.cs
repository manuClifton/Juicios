
// API: Auth
// Recurso: Profile
// Tipo: Response
using System.Collections.Generic;

public class AuthProfileResponse
{
    public string ID { get; set; }
    public string Apellido { get; set; }
    public string Nombre { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Sexo { get; set; }
    public string Cuil { get; set; }
    public string Perfil { get; set; }
    public string DependenciaRRHH { get; set; }
    public string DependenciaFuncional { get; set; }
    public string TipoContrato { get; set; }
    public int TipoOrganismo { get; set; }
    public int IdOrganismo { get; set; }
    public List<string> Atributos { get; set; }


}