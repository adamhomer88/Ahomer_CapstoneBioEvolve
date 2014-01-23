using EvolutionModel.Model.Environment;
using EvolutionModel.Model.PhenoTypes.Water_Absorbtion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvolutionModel.Model.PhenoTypes.Energy_Factory
{
    public class WaterPermeableMembrane : IEnergyFactory, INutrientAbsorber
    {
        public int createEnergy(int waterCount)
        {
            throw new NotImplementedException();
        }

        public int absorbNutrients(EnvironmentTile fertility)
        {
            throw new NotImplementedException();
        }
    }
}
