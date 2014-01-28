using EvolutionModel.Model.Genotypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvolutionModel.Model.PhenoTypes.Digestion
{
    public class DigestiveFactory
    {
        private const int animalDigestiveOptions = 2;
        private const int plantDigestiveOptions = 1;
        private const int parasiteDigestiveOptions = 1;

        public static DigestiveSystem getAnimalDigestiveSystem()
        {
            int randomNum = OrganismFactory.random.Next(animalDigestiveOptions);
            DigestiveSystem digestiveSystem = null;
            switch (randomNum)
            {
                case 0: digestiveSystem = new Herbivore();
                    break;
                case 1: digestiveSystem = new Carnivore();
                    break;
            }
            return digestiveSystem;
        }

        public static DigestiveSystem getPlantDigestiveSystem()
        {
            int randomNum = OrganismFactory.random.Next(plantDigestiveOptions);
            DigestiveSystem digestiveSystem = null;
            switch (randomNum)
            {
                default: break;
            }
            return digestiveSystem;
        }

        public static DigestiveSystem getParasiteDigestiveSystem()
        {
            int randomNum = OrganismFactory.random.Next(parasiteDigestiveOptions);
            DigestiveSystem digestiveSystem = null;
            switch(randomNum)
            {
               case 0: digestiveSystem = new ParasiticDigestiveSystem();
                   break;
            }

            return digestiveSystem;
        }
    }
}
