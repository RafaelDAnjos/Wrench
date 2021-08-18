using System;
using Wrench.Domain.Entities.Identity;
using Wrench.Domain.Enum;

namespace Wrench.Domain.Entities
{
    public class Demanda
    {
        public int IdDemanda { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public Guid DemandanteId { get; set; }
        public EstadoDemanda Estado { get; set; }

        public virtual AppUser Demandante { get; set; }
    }
}