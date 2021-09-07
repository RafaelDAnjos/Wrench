using System;
using Wrench.Domain.Entities.Identity;

namespace Wrench.Domain.Entities
{
    public class Avaliacao
    {
        public Avaliacao()
        {

        }

        public Avaliacao(Nota nota, Guid userId)
        {
            ValorNota = nota;
            IdUsuario = userId;
            EnviadoEm = DateTime.Now;
        }

        public enum Nota
        {
            PESSIMO = 1,
            RUIM,
            RAZOAVEL,
            BOM,
            EXCELENTE
        }
        public int IdAvaliacao { get; set; }
        public int IdRegistroServico { get; set; }
        public Guid IdUsuario { get; set; }
        public Nota ValorNota { get; set; }
        public DateTime EnviadoEm { get; set; }

        public virtual RegistroServico RegistroServico { get; set; }
        public virtual AppUser Usuario { get; set; }
    }
}