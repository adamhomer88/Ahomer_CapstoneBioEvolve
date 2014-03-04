using EvolutionModel.Model.Environment;
using EvolutionModel.Model.PhenoTypes.Digestion;
using EvolutionModel.Model.PhenoTypes.Limbs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvolutionModel.Model.Genotypes
{
    [Serializable]
    public class OrganismFactory : IOrganismFactory
    {
        public static Random random = new Random();
        public DigestiveFactory digestion = new DigestiveFactory();
        private Dictionary<int, Func<Organism>> organismOptions;
        private IOrganismProcessor organismProcessor;

        public OrganismFactory(BioEvolveEnvironment environment)
        {
            organismProcessor = new OrganismProcessor(environment);
            organismOptions = createDictionary();
        }

        public Organism randomOrganism()
        {
            int randomNumber = random.Next(organismOptions.Count);
            Organism organism = null;
            organism = organismOptions[randomNumber].Invoke();
            return organism;
        }

        public Organism randomAnimal()
        {
            return organismOptions[0].Invoke();
        }

        public Organism randomPlant()
        {
            return organismOptions[1].Invoke();
        }

        public Organism randomParasite()
        {
            return organismOptions[2].Invoke();
        }

        public Dictionary<int, Func<Organism>> createDictionary()
        {
            return new Dictionary<int, Func<Organism>>()
            {
                {0, organismProcessor.randomAnimal},
                {1, organismProcessor.randomPlant},
                {2, organismProcessor.randomParasite}
            };
        }
    }
}
