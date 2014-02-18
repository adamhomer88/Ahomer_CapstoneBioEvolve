using EvolutionModel.Model.Environment;
using EvolutionModel.Model.Mutation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvolutionModel.Model.Genotypes
{
    [Serializable]
    public class Parasite : Organism
    {
        public Organism Host { get; set; }

        #region DefaultBasePhenotypes
        #endregion

        public Parasite()
        {
            this.MaxEnergy = 20;
            this.EnergyTotal = (int)(this.MaxEnergy * .6);
            this.Mass = 1;
            this.EnergyPerTurn = 2;
            this.Generation = 1;
        }

        public override void doTurn()
        {
            
        }

        public override Organism basicMutate(Organism baseOrganism)
        {
            Mutator basicMutator = Mutator.GetBasicInstance();
            Organism newMutatedOrganism = basicMutator.Mutate(baseOrganism);
            return newMutatedOrganism;
        }

        public override Organism complexMutate(Organism baseOrganism)
        {
            Mutator complexMutator = Mutator.GetComplexInstance();
            Organism newMutatedOrganism = complexMutator.Mutate(baseOrganism);
            return newMutatedOrganism;
        }
    }
}
