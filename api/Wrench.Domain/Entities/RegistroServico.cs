using System;
using System.Collections.Generic;
using System.Linq;
using Wrench.Domain.Entities.Identity;
using Wrench.Domain.Enum;

namespace Wrench.Domain.Entities
{
    public class RegistroServico
    {
        public enum TipoUsuario
        {
            DEMANDANTE,
            PRESTADOR
        }

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

            Estado = EstadoServico.SELECIONADO;
        }

        public int IdRegistroServico { get; set; }
        public int IdDemanda { get; set; }
        public Guid IdDemandante { get; set; }
        public Guid IdPrestador { get; set; }
        public bool CheckDemandante { get; set; }
        public bool CheckPrestador { get; set; }
        public EstadoServico Estado { get; set; }
        public DateTime Prazo { get; set; }
        public string Mensagem { get; set; }
        public decimal ValorEstimado { get; set; }
        public decimal ValorCobrado { get; set; }

        public virtual Demanda Demanda { get; set; }
        public virtual AppUser Demandante { get; set; }
        public virtual AppUser Prestador { get; set; }

        public virtual ICollection<Avaliacao> Avaliacoes { get; set; }

        public void Cancelar()
        {
            Estado = EstadoServico.CANCELADO;
        }

        public void Agendar()
        {
            Estado = EstadoServico.TOPADO;
        }

        public void Concluir(TipoUsuario tipoUsuario)
        {
            if (tipoUsuario == TipoUsuario.DEMANDANTE)
                CheckDemandante = true;
            else if (tipoUsuario == TipoUsuario.PRESTADOR)
                CheckPrestador = true;

            if (CheckDemandante && CheckPrestador)
                Estado = EstadoServico.CONCLUIDO;
        }

        public void Avaliar(Avaliacao avaliacao)
        {
            var EhPrestadorOuDemandante = IdDemandante == avaliacao.IdUsuario || IdPrestador == avaliacao.IdUsuario;

            if (EhPrestadorOuDemandante && !Avaliacoes.Any(x => x.IdUsuario == avaliacao.IdUsuario))
                Avaliacoes.Add(avaliacao);

            if (Avaliacoes.Count == 2)
                Estado = EstadoServico.AVALIADO;
        }

        public bool EhConcluido()
        {
            return Estado == EstadoServico.CONCLUIDO;
        }

        public bool EhAvaliado()
        {
            if (Avaliacoes.Count == 2)
                return true;

            return false;
        }
    }
}