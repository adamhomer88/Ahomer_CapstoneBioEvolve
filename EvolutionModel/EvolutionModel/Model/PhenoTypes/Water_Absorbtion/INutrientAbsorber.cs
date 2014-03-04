using EvolutionModel.Model.Environment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvolutionModel.Model.PhenoTypes.Water_Absorbtion
{
    public interface INutrientAbsorber : IPhenotype
    {
        int absorbNutrients(EnvironmentTile environment, int mass);
        int absorbWater(EnvironmentTile environment, int mass);
    }
}
