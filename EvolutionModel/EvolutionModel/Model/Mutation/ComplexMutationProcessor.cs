using EvolutionModel.Model.Genotypes;
using EvolutionModel.Model.Mutation.Digestive;
using EvolutionModel.Model.Mutation.Energy_Factory;
using EvolutionModel.Model.Mutation.Head;
using EvolutionModel.Model.Mutation.Limb;
using EvolutionModel.Model.Mutation.Skin;
using EvolutionModel.Model.Mutation.Water_Absorbtion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace EvolutionModel.Model.Mutation
{
    [Serializable]
    class ComplexMutationProcessor : IMutationProcessor
    {
        private static ComplexMutationProcessor processor;
        IDigestiveMutationProcessor digestiveProcessor;
        IHeadMutationProcessor headProcessor;
        ILimbMutationProcessor limbProcessor;
        ISkinMutationProcessor skinProcessor;
        IEnergyFactoryMutationProcessor energyFactoryProcessor;
        IWaterAbsorbtionMutationProcessor absorbtionProcessor;

        Random generator;

        public ComplexMutationProcessor()
        {
            generator = OrganismFactory.random;
            digestiveProcessor = DigestiveMutationProcess.GetInstance();
            headProcessor = HeadMutationProcessor.GetInstance();
            skinProcessor = SkinMutationProcessor.GetInstance();
            limbProcessor = LimbMutationProcessor.GetInstance();
            energyFactoryProcessor = EnergyFactoryMutationProcessor.GetInstance();
            absorbtionProcessor = WaterAbsorbtionMutationProcessor.GetInstance();
        }

        public Genotypes.Organism MutateAnimal(Genotypes.Organism organism)
        {
            Animal mutatee = (Animal)(organism);
            int number = OrganismFactory.random.Next(2);
            if (number == 1)
                mutatee = limbProcessor.Mutate(mutatee);
            else
                mutatee = digestiveProcessor.Mutate(mutatee);
            return mutatee;
        }

        public Genotypes.Organism MutatePlant(Genotypes.Organism organism)
        {
            Plant mutatee = (Plant)(organism);
            int number = OrganismFactory.random.Next(2);
            if (number == 1)
                mutatee = energyFactoryProcessor.Mutate(mutatee);
            else
                mutatee = absorbtionProcessor.Mutate(mutatee);
            return mutatee;
        }

        public Genotypes.Organism MutateParasite(Genotypes.Organism organism)
        {
            Parasite mutatee = (Parasite)(organism);
            return mutatee;
        }


        public static IMutationProcessor GetInstance()
        {
            if(processor == null)
                processor = new ComplexMutationProcessor();
            return processor;
        }
    }
}
