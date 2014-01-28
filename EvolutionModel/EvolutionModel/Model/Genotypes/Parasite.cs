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

        public int Absorb(Animal animal)
        {
            return this.Digestion.Digest(animal);
        }

        public int Absorb(Plant plant)
        {
            return this.Digestion.Digest(plant);
        }

        public int Absorb(Organism organism)
        {
            int energy = 0;
            if (organism is Plant)
                energy = Absorb(organism as Plant);
            else
                energy = Absorb(organism as Animal);
            return energy;
        }

        public override void doTurn(EnvironmentTile localEnvironment)
        {
            Absorb(Host);
        }

        public override Organism mutate(Organism baseOrganism)
        {
            throw new NotImplementedException();
        }
    }
}
