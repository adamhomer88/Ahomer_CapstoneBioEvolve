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
            throw new NotImplementedException();
        }

        public int Absorb(Plant plant)
        {
            throw new NotImplementedException();
        }

        public override void doTurn(Environment.EnvironmentTile localEnvironment)
        {
            throw new NotImplementedException();
        }

        public override Organism mutate(Organism baseOrganism)
        {
            throw new NotImplementedException();
        }
    }
}
