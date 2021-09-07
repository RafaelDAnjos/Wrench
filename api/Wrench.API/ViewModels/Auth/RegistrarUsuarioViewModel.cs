using Wrench.Domain.Entities.Identity;

namespace Wrench.API.ViewModels.Auth
{
    public class RegistrarUsuarioViewModel
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string ConfirmarSenha { get; set; }
        public string Identificacao { get; set; } //CPF ou CNPJ
        public TipoUsuario TipoUsuario { get; set; }
    }
}