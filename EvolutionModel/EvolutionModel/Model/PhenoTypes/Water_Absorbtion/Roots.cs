﻿using EvolutionModel.Model.Environment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvolutionModel.Model.PhenoTypes.Water_Absorbtion
{
    [Serializable]
    class Roots : INutrientAbsorber
    {
        public int absorbNutrients(EnvironmentTile localEnvironment, int mass)
        {
            throw new NotImplementedException();
        }

        public IPhenotype Mutate()
        {
            throw new NotImplementedException();
        }


        public int absorbWater(EnvironmentTile environment, int mass)
        {
            throw new NotImplementedException();
        }
    }
}
