using EvolutionModel.Model.Environment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvolutionModel.Model.Genotypes
{
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

        public override void doTurn(EnvironmentTile localEnvironment)
        {
            Digestion.Digest(Host);
            this.Host.Parasites.Add((Parasite)this.Reproduce(this));
        }

        public override Organism mutate(Organism baseOrganism)
        {
            throw new NotImplementedException();
        }


        public override Organism complexMutate(Organism baseOrganism)
        {
            throw new NotImplementedException();
        }
    }
}
