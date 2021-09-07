using System;
using System.Collections.Generic;
using Wrench.Domain.Entities.Identity;
using Wrench.Domain.Enum;

namespace Wrench.Domain.Entities
{
    public class Demanda
    {
        protected Demanda()
        {
            Estado = EstadoDemanda.DISPONIVEL;
            Tags = new List<Tag>();
            RegistroServicos = new List<RegistroServico>();
        }

        public Demanda(string titulo, string descricao, Guid idElaborador) : base()
        {
            Titulo = titulo;
            Descricao = descricao;
            IdElaborador = idElaborador;
            Tags = new List<Tag>();
            RegistroServicos = new List<RegistroServico>();
        }

        public int IdDemanda { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public Guid IdElaborador { get; set; }

        public EstadoDemanda Estado { get; set; }

        public virtual AppUser Elaborador { get; set; }

        public virtual ICollection<Tag> Tags { get; set; }
        public virtual ICollection<RegistroServico> RegistroServicos { get; set; }

        public void AdicionarTag(Tag tag)
        {
            Tags.Add(tag);
        }

        public void RemoverTag(Tag tag)
        {
            Tags.Remove(tag);
        }

        public void EscolherDemanda(Guid idPrestador, decimal valor, DateTime prazo, string mensagem)
        {
            var registro = new RegistroServico(idPrestador, IdElaborador, valor, prazo, mensagem);

            RegistroServicos.Add(registro);
        }

        public void ToparDemanda(int idRegistroServico)
        {
            Estado = EstadoDemanda.TOPADA;

            foreach (var registro in RegistroServicos)
            {
                if (registro.IdRegistroServico != idRegistroServico)
                    registro.Cancelar();
                else
                    registro.Agendar();
            }
        }
    }
}