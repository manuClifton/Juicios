namespace VEJuicios.Application.UseCases
{
    public class Input
    {
        public Input(string usuario)
        {
            this.Usuario = usuario;
        }
        public string Usuario { get; }
    }
}