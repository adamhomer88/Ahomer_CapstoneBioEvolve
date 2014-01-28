using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EvolutionModel.Model.PhenoTypes.Digestion;
using EvolutionModel.Model.Environment;
namespace EvolutionModel.Model.Genotypes
{
    public abstract class Organism
    {
        public int Mass { get; set; }
        public int EnergyTotal { get; set; }
        public int MaxEnergy { get; set; }
        public int EnergyPerTurn { get; set; }
        public int Generation { get; set; }
        public DigestiveSystem Digestion { get; set; }
        public abstract void doTurn(EnvironmentTile localEnvironment);
        public abstract Organism mutate(Organism baseOrganism);
    }
}
