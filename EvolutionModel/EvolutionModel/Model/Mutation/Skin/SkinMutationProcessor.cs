using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvolutionModel.Model.Mutation.Skin
{
    class SkinMutationProcessor : ISkinMutationProcessor
    {
        private static SkinMutationProcessor process;

        public static SkinMutationProcessor GetInstance()
        {
            if (process == null)
                process = new SkinMutationProcessor();
            return process;
        }
    }
}
