using System;
using System.Collections.Generic;

namespace Wrench.Domain.Entities
{
    public class Chat
    {
        public int IdChat { get; set; }
        public string Titulo { get; set; }
        public DateTime CriadoEm { get; set; }
        public bool Ativo { get; set; }

        public virtual ICollection<ChatConversa> Mensagens { get; set; }
    }
}