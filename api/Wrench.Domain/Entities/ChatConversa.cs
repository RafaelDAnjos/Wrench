using System;
using Wrench.Domain.Entities.Identity;

namespace Wrench.Domain.Entities
{
    public class ChatConversa
    {
        public int IdChatConversa { get; set; }
        public int IdChat { get; set; }
        public Guid De { get; set; }
        public Guid Para { get; set; }
        public string Mensagem { get; set; }
        public DateTime EnviadoEm { get; set; }

        public virtual Chat Chat { get; set; }
        public virtual AppUser UserDe { get; set; }
        public virtual AppUser UserPara { get; set; }
    }
}