using Wrench.Domain.Enum;

namespace Wrench.Domain.Entities
{
    public class RegistroServico
    {
        public int IdRegistroServico { get; set; }
        public EstadoServico Estado { get; set; }
        public int IdDemanda { get; set; }

        public decimal ValorEstimado { get; set; }
        public decimal ValorCobrado { get; set; }

        public virtual Demanda Demanda { get; set; }
    }
}