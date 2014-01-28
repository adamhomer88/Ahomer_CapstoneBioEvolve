using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EvolutionModel.Model.Genotypes
{
    public interface IOrganismProcessor
    {
        Organism randomAnimal();
        Organism randomPlant();
        Organism randomParasite();
    }
}
