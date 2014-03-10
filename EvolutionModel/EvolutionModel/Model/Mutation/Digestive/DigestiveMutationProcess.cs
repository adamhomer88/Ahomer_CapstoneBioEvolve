using EvolutionModel.Model.Genotypes;
using EvolutionModel.Model.PhenoTypes.Digestion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvolutionModel.Model.Mutation.Digestive
{
    [Serializable]
    class DigestiveMutationProcess : IDigestiveMutationProcessor
    {
       private static DigestiveMutationProcess process;

       public static DigestiveMutationProcess GetInstance()
        {
            if (process == null)
                process = new DigestiveMutationProcess();
            return process;
        }

       public Genotypes.Animal Mutate(Genotypes.Animal mutatee)
       {
           int number = OrganismFactory.random.Next(2);
           if (number == 1)
               mutatee = MutateExistingDigestion(mutatee);
           else
               mutatee = MutateNewDigestion(mutatee);
           return mutatee;
       }

       private Animal MutateExistingDigestion(Animal mutatee)
       {
           mutatee.favoredHungerThreshold -= .05;
           return mutatee;
       }

       private Animal MutateNewDigestion(Animal mutatee)
       {
           if (mutatee.Digestion is Carnivore)
               mutatee.Digestion = new Omnivore();
           else
               mutatee.Digestion = new Carnivore();
           return mutatee;
       }
    }
}
