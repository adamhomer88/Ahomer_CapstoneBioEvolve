using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvolutionModel.Model.Genotypes
{
    public interface IOrganismFactory
    {
        Organism randomOrganism();
        Dictionary<int, Func<Organism>> createDictionary();
    }
}
