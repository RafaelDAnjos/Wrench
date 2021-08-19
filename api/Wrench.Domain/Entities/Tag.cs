using System;
using System.Collections.Generic;
using Wrench.Domain.Entities.Identity;

namespace Wrench.Domain.Entities
{
    public class Tag
    {
        protected Tag()
        {

        }

        public Tag(string nome)
        {
            Ativo = true;
            Nome = nome;
        }

        public int IdTag { get; set; }
        public string Nome { get; set; }
        public bool Ativo { get; set; }

        public ICollection<AppUser> AtribuidosPara { get; set; }

        public void AlterarInformacao(string nome)
        {
            if (string.IsNullOrEmpty(nome))
                throw new ArgumentOutOfRangeException("Nome inválido.");

            Nome = nome;
        }

        public void Inativar()
        {
            Ativo = false;
        }
    }
}