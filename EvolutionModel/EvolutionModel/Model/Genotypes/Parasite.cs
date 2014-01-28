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
        
        public int Absorb(Animal animal)
        {
            return this.digestion.Digest(animal);
        }

        public int Absorb(Plant plant)
        {
            return this.digestion.Digest(plant);
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
