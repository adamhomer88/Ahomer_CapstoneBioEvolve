﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvolutionModel.Model.PhenoTypes.Limbs
{
    interface ILimbProcessor
    {
        IAppendage createPredatoryLimb();
        IAppendage createUtilityLimb();
    }
}