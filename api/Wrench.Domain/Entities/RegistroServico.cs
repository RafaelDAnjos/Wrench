using System;
using Wrench.Domain.Entities.Identity;
using Wrench.Domain.Enum;

namespace Wrench.Domain.Entities
{
    public class RegistroServico
    {
        public RegistroServico()
        {

        }

        public RegistroServico(Guid idPrestador, Guid idDemandante, decimal valor, DateTime prazo, string mensagem)
        {
            IdPrestador = idPrestador;
            IdDemandante = idDemandante;
            ValorEstimado = valor;
            Prazo = prazo;
            Mensagem = mensagem;

            Estado = EstadoServico.AGUARDANDO;
        }

        public int IdRegistroServico { get; set; }
        public int IdDemanda { get; set; }
        public Guid IdDemandante { get; set; }
        public Guid IdPrestador { get; set; }
        public EstadoServico Estado { get; set; }
        public DateTime Prazo { get; set; }
        public string Mensagem { get; set; }
        public decimal ValorEstimado { get; set; }
        public decimal ValorCobrado { get; set; }

        public virtual Demanda Demanda { get; set; }
        public virtual AppUser Demandante { get; set; }
        public virtual AppUser Prestador { get; set; }

        public void Cancelar()
        {
            Estado = EstadoServico.CANCELADO;
        }

        public void Agendar()
        {
            Estado = EstadoServico.AGENDADO;
        }
    }
}