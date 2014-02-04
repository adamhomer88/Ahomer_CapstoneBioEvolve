using EvolutionModel.Model.PhenoTypes.Head;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvolutionModel.Model.Mutation.Head
{
    class HeadMutationProcessor : IHeadMutationProcessor
    {
        private static HeadMutationProcessor process;

        public Genotypes.Animal NewHead(Genotypes.Animal organism)
        {
            if (organism.head == null)
                organism.head = new PhenoTypes.Head.Head();
            else
                organism = ModifyHead(organism);
            return organism;
        }

        public Genotypes.Animal ModifyHead(Genotypes.Animal organism)
        {
            if (organism.head.Protection == null)
                organism.head.Protection = new Horns();
            else
                organism.head.Protection.Mutate();
            return organism;
        }

        public static HeadMutationProcessor GetInstance()
        {
            if (process == null)
                process = new HeadMutationProcessor();
            return process;
        }
    }
}
