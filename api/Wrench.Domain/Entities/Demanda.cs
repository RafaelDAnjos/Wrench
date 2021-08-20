using System;
using System.Collections.Generic;
using Wrench.Domain.Entities.Identity;
using Wrench.Domain.Enum;

namespace Wrench.Domain.Entities
{
    public class Demanda
    {
        protected Demanda() { }

        public Demanda(string titulo, string descricao, Guid idDemandante)
        {
            Titulo = titulo;
            Descricao = descricao;
            IdDemandante = idDemandante;
            Estado = EstadoDemanda.DISPONIVEL;
            Tags = new List<Tag>();
        }

        public int IdDemanda { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public Guid IdDemandante { get; set; }
        public Guid IdPrestador { get; set; }

        public EstadoDemanda Estado { get; set; }

        public virtual AppUser Demandante { get; set; }
        public virtual AppUser Prestador { get; set; }

        public ICollection<Tag> Tags { get; set; }

        public void AdicionarTag(Tag tag)
        {
            Tags.Add(tag);
        }

        public void RemoverTag(Tag tag)
        {
            Tags.Remove(tag);
        }

        public void ToparDemanda(Guid idPrestador)
        {
            IdPrestador = idPrestador;
            Estado = EstadoDemanda.TOPADA;
        }
    }
}