using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvolutionModel.Model.Genotypes
{
    public class DeadAnimal : Animal
    {
        public const double ENERGY_CONVERSION = 0.6;

        public DeadAnimal(Animal animal) : base(null)
        {
            this.Mass = 0;
            this.EnergyTotal = animal.MaxEnergy/ENERGY_CONVERSION;
        }

        public override Organism doTurn()
        {
            //be dead
            return null;
        }

        public override Animal resolveReproduction()
        {
            return null;
        }
    }
}
