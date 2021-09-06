using System;

namespace Wrench.API.ViewModels.Demandas
{
    public class EscolherDemandaViewModel
    {
        public int IdDemanda { get; set; }
        public DateTime Prazo { get; set; }
        public decimal Valor { get; set; }
        public string Mensagem { get; set; }
    }
}
