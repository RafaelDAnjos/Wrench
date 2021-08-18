using Microsoft.AspNetCore.Identity;
using System;

namespace Wrench.Domain.Entities.Identity
{
    public enum TipoUsuario
    {
        DEMANDANTE,
        PRESTADOR
    }

    public class AppUser : IdentityUser<Guid>
    {
        public string Nome { get; set; }
        public string Identificacao { get; set; } //CPF e CNPJ
        public TipoUsuario Tipo { get; set; }
    }
}