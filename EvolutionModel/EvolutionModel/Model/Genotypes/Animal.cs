using EvolutionModel.Model.PhenoTypes.Skin;
using EvolutionModel.Model.PhenoTypes.Digestion;
using EvolutionModel.Model.PhenoTypes.Head;
using EvolutionModel.Model.PhenoTypes.Limbs;
using EvolutionModel.Model.PhenoTypes.Sensory_Phenotypes;
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
        public List<IAppendage> Limbs { get; set; }
        public int Limb_Count { get; set; }
        public Head head { get; set; }
        public ISense Sensory { get; set; }
        public List<IProtectivePhenotype> Protections { get; set; }
        public List<AnimalParasite> Parasite { get; set; }
        private double favoredHungerThreshold = .75;
        private double unfavoredHungerThreshold = .3;
        private double reproductionThreshold = .8;
        Boolean hasMoved = false;

        public abstract void Move();

        public override void doTurn()
        {
            FindFavoredDiet();
            if (this.EnergyTotal / this.MaxEnergy > reproductionThreshold)
                Reproduce();
            if (!hasMoved)
                Move();
            hasMoved = false;
        }

        private void FindFavoredDiet()
        {
            Organism detectedOrganism = detectFavoredDiet();
            if (detectedOrganism != null && (this.EnergyTotal / this.MaxEnergy < favoredHungerThreshold))
            {
                Organism capturedOrganism = doCapture(detectedOrganism);
                EatOrganism(capturedOrganism);
            }
            else if (this.EnergyTotal / this.MaxEnergy < unfavoredHungerThreshold)
            {
                detectedOrganism = FindUnfavoredDiet(detectedOrganism);
            }
        }

        private Organism FindUnfavoredDiet(Organism detectedOrganism)
        {
            detectedOrganism = detectUnfavoredDiet();
            if (detectedOrganism != null)
            {
                Organism capturedOrganism = doCapture(detectedOrganism);
                EatOrganism(capturedOrganism);
            }
            return detectedOrganism;
        }

        private void EatOrganism(Organism capturedOrganism)
        {
            if (capturedOrganism != null)
            {
                int energy = digestion.Digest(capturedOrganism);
                if ((energy + this.EnergyTotal) > this.MaxEnergy)
                    this.EnergyTotal = this.MaxEnergy;
                else
                    this.EnergyTotal += energy;
            }
        }

        private Organism detectUnfavoredDiet()
        {
            throw new NotImplementedException();
        }

        private Organism detectFavoredDiet()
        {
            throw new NotImplementedException();
        }

        private Organism doCapture(Organism detectedAnimal)
        {
            throw new NotImplementedException();
        }

        public Animal Reproduce()
        {
            return null;
        }
    }
}
