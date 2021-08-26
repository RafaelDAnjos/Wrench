using System.Collections.Generic;

namespace Wrench.API.ViewModels.Demandas
{
    public class CriarDemandaViewModel
    {
        public string Titulo { get; set; }
        public string Descricao { get; set; }

        public ICollection<string> Tags { get; set; }
    }
}
