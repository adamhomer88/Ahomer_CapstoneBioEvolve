using EvolutionModel.Model.Genotypes;
using EvolutionModel.Model.PhenoTypes.Limbs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvolutionModel.Model.Mutation.Limb
{
    class LimbMutationProcessor : ILimbMutationProcessor
    {
        private const int loseLimbFrequency = 100;
        private LimbFactory factory;
        private static LimbMutationProcessor process;

        public LimbMutationProcessor()
        {
            factory = new LimbFactory();
        }

        private int removeLimb(ref Genotypes.Animal animal, int limbCount)
        {
            int newLimbCount;
            newLimbCount = limbCount - 1;
            animal = resolveVestigialLimb(animal);
            return newLimbCount;
        }

        private Animal resolveVestigialLimb(Animal animal)
        {
            int randomNum = OrganismFactory.random.Next(animal.Limbs.Count);
            Model.PhenoTypes.Limbs.Limb vestigialLimb = animal.Limbs[randomNum];
            animal.Limbs.RemoveAt(randomNum);
            animal.VestigialLimbs.Add(vestigialLimb);
            return animal;
        }

        public Genotypes.Animal MutateExistingLimb(Genotypes.Animal animal)
        {
            if (animal.Limbs.Count != 0)
            {
                int randomNum = OrganismFactory.random.Next(animal.Limbs.Count);
                animal.Limbs[randomNum] = (Model.PhenoTypes.Limbs.Limb)animal.Limbs[randomNum].Mutate();
            }
            else
                MutateNewLimb(animal);
            return animal;
        }

        public Genotypes.Animal MutateNewLimb(Genotypes.Animal animal)
        {
            animal.Limbs.Add(factory.RandomLimb());
            return animal;
        }

        public static LimbMutationProcessor GetInstance()
        {
            if (process == null)
                process = new LimbMutationProcessor();
            return process;
        }

        public Animal Mutate(Animal animal)
        {
            int number = OrganismFactory.random.Next(2);
            if (number == 1)
                this.MutateNewLimb(animal);
            else
                this.MutateExistingLimb(animal);
            return animal;
        }
    }
}
