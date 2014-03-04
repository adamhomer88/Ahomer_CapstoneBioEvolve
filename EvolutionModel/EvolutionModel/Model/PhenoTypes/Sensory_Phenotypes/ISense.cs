using EvolutionModel.Model.Environment;
using EvolutionModel.Model.Genotypes;
using EvolutionModel.Model.PhenoTypes.Digestion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace EvolutionModel.Model.PhenoTypes.Sensory_Phenotypes
{
    public interface ISense : IPhenotype
    {
        Organism FindFavoredFood(Type DigestionType, BioEvolveEnvironment environment, Point Location);
        Organism FindFood(BioEvolveEnvironment environment, Point Location);
        Organism FindMate(BioEvolveEnvironment environment, Point Location);
    }
}
