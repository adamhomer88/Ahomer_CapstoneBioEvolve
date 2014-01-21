using EvolutionModel.Model.PhenoTypes.Defensive_Offensive_Phenotypes;
using EvolutionModel.Model.PhenoTypes.Digestion;
using EvolutionModel.Model.PhenoTypes.Head;
using EvolutionModel.Model.PhenoTypes.Limbs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvolutionModel.Model.Genotypes
{
    public abstract class Animal : Organism
    {
        public DigestiveSystem Digestion { get; set; }
        public List<ILimb> Limbs { get; set; }
        public int Limb_Count { get; set; }
        public Head head { get; set; }
        public List<IProtectivePhenotype> Protections { get; set; }
        public List<AnimalParasite> Parasite { get; set; }

        public abstract void Move();
        public abstract void doTurn();
        public Animal Reproduce()
        {
            return null;
        }
    }
}
