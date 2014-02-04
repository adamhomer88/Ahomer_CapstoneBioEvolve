using EvolutionModel.Model.Genotypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvolutionModel.Model.PhenoTypes.Limbs
{
    class LimbFactory : ILimbFactory
    {
        ILimbProcessor processor { get; set; }
        Dictionary<int, Func<IAppendage>> limbOptions { get; set; }

        public LimbFactory()
        {
            processor = new BasicLimbProcessor();
            limbOptions = createDictionary();
        }

        private Dictionary<int, Func<IAppendage>> createDictionary()
        {
            return new Dictionary<int, Func<IAppendage>>()
            {
                {0,processor.createPredatoryLimb},
                {1,processor.createUtilityLimb}
            };
        }

        public IAppendage RandomLimb()
        {
            int randomNum = OrganismFactory.random.Next(limbOptions.Count);
            return limbOptions[randomNum].Invoke();
        }
    }
}
