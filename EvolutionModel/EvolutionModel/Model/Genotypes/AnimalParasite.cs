using EvolutionModel.Model.Environment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvolutionModel.Model.Genotypes
{
    public class AnimalParasite : Animal, IParasite
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

        public override void Move()
        {
            throw new NotImplementedException();
        }

        public override void doTurn(EnvironmentTile tile)
        {
            if (Host == null)
                searchForHost(tile);
        }

        private void searchForHost(EnvironmentTile tile)
        {
            throw new NotImplementedException();
        }
    }
}
