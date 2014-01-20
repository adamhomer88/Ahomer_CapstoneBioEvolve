using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EvolutionModel.Model.PhenoTypes.Digestion;
namespace EvolutionModel.Model.Genotypes
{
    public class Organism
    {
        public int Mass { get; set; }
        public int EnergyTotal { get; set; }
        public int MaxEnergy { get; set; }
        public int EnergyPerTurn { get; set; }
        public int Generation { get; set; }
        public DigestiveSystem digestion { get; set; }
    }
}
