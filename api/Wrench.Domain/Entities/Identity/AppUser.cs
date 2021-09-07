using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

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

        public virtual ICollection<Tag> Tags { get; set; }

        public void AdicionarTag(Tag tag)
        {
            if (Tags.Contains(tag))
                throw new InvalidOperationException("Não pode inserir a mesma tag mais de uma vez.");

            Tags.Add(tag);
        }

        public void RemoverTag(Tag tag)
        {
            if (!Tags.Contains(tag))
                throw new InvalidOperationException("Tag não está associada ao usuário.");

            Tags.Remove(tag);
        }
    }
}