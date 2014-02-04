using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvolutionModel.Model.Mutation.Digestive
{
    class DigestiveMutationProcess : IDigestiveMutationProcessor
    {
       private static DigestiveMutationProcess process;

       public static DigestiveMutationProcess GetInstance()
        {
            if (process == null)
                process = new DigestiveMutationProcess();
            return process;
        }
    }
}
