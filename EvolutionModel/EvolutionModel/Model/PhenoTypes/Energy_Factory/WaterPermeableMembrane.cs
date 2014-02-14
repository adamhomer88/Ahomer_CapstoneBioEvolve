using EvolutionModel.Model.Environment;
using EvolutionModel.Model.PhenoTypes.Water_Absorbtion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvolutionModel.Model.PhenoTypes.Energy_Factory
{
    [Serializable]
    public class WaterPermeableMembrane : EnergyFactory, INutrientAbsorber
    {
        public override int createEnergy(out int fertility,int waterCount)
        {
            throw new NotImplementedException();
        }

        public int absorbNutrients(EnvironmentTile localEnvironment, int mass)
        {
            throw new NotImplementedException();
        }

        public override IPhenotype Mutate()
        {
            throw new NotImplementedException();
        }
    }
}
