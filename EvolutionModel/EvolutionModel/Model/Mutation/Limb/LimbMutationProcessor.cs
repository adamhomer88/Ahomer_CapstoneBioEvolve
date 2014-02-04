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

        public Genotypes.Animal MutateLimbCount(Genotypes.Animal animal)
        {
            int limbCount = animal.Limb_Count;
            int newLimbCount;
            int randomNum = OrganismFactory.random.Next();
            if (randomNum % loseLimbFrequency == 0){
                newLimbCount = removeLimb(ref animal, limbCount);
            }
            else
                newLimbCount = limbCount + 1;
            animal.Limb_Count = newLimbCount;
            return animal;
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
            IAppendage vestigialLimb = animal.Limbs[randomNum];
            animal.Limbs.RemoveAt(randomNum);
            animal.VestigialLimbs.Add(vestigialLimb);
            return animal;
        }

        public Genotypes.Animal MutateExistingLimb(Genotypes.Animal animal)
        {
            int randomNum = OrganismFactory.random.Next(animal.Limbs.Count);
            animal.Limbs[randomNum] = (IAppendage)animal.Limbs[randomNum].Mutate();
            return animal;
        }

        public Genotypes.Animal MutateNewLimb(Genotypes.Animal animal)
        {
            throw new NotImplementedException();
        }
    }
}
