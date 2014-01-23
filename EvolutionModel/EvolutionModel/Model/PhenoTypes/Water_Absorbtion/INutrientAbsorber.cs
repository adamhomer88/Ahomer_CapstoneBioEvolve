using EvolutionModel.Model.Environment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvolutionModel.Model.PhenoTypes.Water_Absorbtion
{
    public interface INutrientAbsorber
    {
        int absorbNutrients(EnvironmentTile fertility, int mass);
    }
}
