using System;
using Wrench.Domain.Entities.Identity;

namespace Wrench.Domain.Entities
{
    public class Avaliacao
    {
        public int IdAvaliacao { get; set; }
        public int IdRegistroServico { get; set; }
        public Guid UsuarioId { get; set; }
        public int Nota { get; set; }
        public DateTime EnviadoEm { get; set; }

        public virtual RegistroServico RegistroServico { get; set; }
        public virtual AppUser Usuario { get; set; }
    }
}