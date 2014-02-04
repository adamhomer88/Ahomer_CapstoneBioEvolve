using EvolutionModel.Model.Genotypes;
using EvolutionModel.Model.Mutation.Digestive;
using EvolutionModel.Model.Mutation.Head;
using EvolutionModel.Model.Mutation.Limb;
using EvolutionModel.Model.Mutation.Skin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace EvolutionModel.Model.Mutation
{
    class ComplexMutationProcessor : IMutationProcessor
    {
        private static ComplexMutationProcessor processor;
        IDigestiveMutationProcessor digestiveProcessor;
        IHeadMutationProcessor headProcessor;
        ILimbMutationProcessor limbProcessor;
        ISkinMutationProcessor skinProcessor;

        Random generator;

        public ComplexMutationProcessor()
        {
            generator = OrganismFactory.random;
            digestiveProcessor = DigestiveMutationProcess.GetInstance();
            headProcessor = HeadMutationProcessor.GetInstance();
            skinProcessor = SkinMutationProcessor.GetInstance();
            limbProcessor = LimbMutationProcessor.GetInstance();
        }

        public Genotypes.Organism MutateAnimal(Genotypes.Organism organism)
        {
            MessageBox.Show("Animal Speciated");
            Animal mutatee = (Animal)(organism);
            int randomNum = generator.Next();
            throw new NotImplementedException();
        }

        public Genotypes.Organism MutatePlant(Genotypes.Organism organism)
        {
            MessageBox.Show("Plant Speciated");
            Plant mutatee = (Plant)(organism);
            throw new NotImplementedException();
        }

        public Genotypes.Organism MutateParasite(Genotypes.Organism organism)
        {
            MessageBox.Show("Parasite Speciated");
            Parasite mutatee = (Parasite)(organism);
            throw new NotImplementedException();
        }


        public static IMutationProcessor GetInstance()
        {
            if(processor == null)
                processor = new ComplexMutationProcessor();
            return processor;
        }
    }
}
