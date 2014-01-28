using EvolutionModel.Model.PhenoTypes.Digestion;
using EvolutionModel.Model.PhenoTypes.Limbs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvolutionModel.Model.Genotypes
{
    public class OrganismFactory : IOrganismFactory
    {
        public static Random random = new Random();
        public DigestiveFactory digestion = new DigestiveFactory();
        private const int totalOrganismOptions = 3;
        private Dictionary<int, Func<Organism>> organismOptions;
        private IOrganismProcessor organismProcessor;

        public OrganismFactory()
        {
            organismProcessor = new OrganismProcessor();
            organismOptions = createDictionary();
        }

        public Organism randomOrganism()
        {
            int randomNumber = random.Next(totalOrganismOptions);
            Organism organism = null;
            organism = organismOptions[randomNumber].Invoke();
            return organism;
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
