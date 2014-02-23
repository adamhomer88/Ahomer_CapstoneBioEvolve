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
        Dictionary<int, Func<Limb>> limbOptions { get; set; }

        public LimbFactory()
        {
            processor = new BasicLimbProcessor();
            limbOptions = createDictionary();
        }

        private Dictionary<int, Func<Limb>> createDictionary()
        {
            return new Dictionary<int, Func<Limb>>()
            {
                {0,processor.createPredatoryLimb},
                {1,processor.createUtilityLimb}
            };
        }

        public Limb RandomLimb()
        {
            int randomNum = OrganismFactory.random.Next(limbOptions.Count);
            return limbOptions[randomNum].Invoke();
        }
    }
}
