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
using EvolutionModel.Model.Environment;

namespace EvolutionModel.Model.Genotypes
{
    public class Animal : Organism
    {
        Boolean hasMoved = false;
        public double favoredHungerThreshold { get; set; }
        public double unfavoredHungerThreshold { get; set; }
        public double reproductionThreshold { get; set; }
       
        #region Parasites
        public List<Parasite> Parasites { get; set; }
        #endregion

        #region DefaultBasePhenotypes
        public const double DEFAULT_FAVORED_HUNGER_THRESHOLD = .6;
        public const double DEFAULT_UNFAVORED_HUNGER_THRESHOLD = .2;
        public const double DEFAULT_REPRODUCTION_THRESHOLD = .8;
        #endregion

        #region Phenotypes
        public int Limb_Count { get; set; }
        public Head head { get; set; }
        public ISense Sensory { get; set; }
        public List<IProtectivePhenotype> Skin { get; set; }
        public List<IAppendage> Limbs { get; set; }
        public Boolean isColdBlooded { get; set; }
        #endregion

        public Animal()
        {
            this.Mass = 1;
            this.MaxEnergy = 50;
            this.EnergyTotal = (int)(this.MaxEnergy*.6);
            this.EnergyPerTurn = 5;
            this.Generation = 1;
        }
        
        public void Move()
        {
            throw new NotImplementedException();
        }

        public override void doTurn(EnvironmentTile localEnvironment)
        {
            EatFavoredDiet();
            if (this.EnergyTotal / this.MaxEnergy > reproductionThreshold)
                Reproduce();
            if (!hasMoved)
                Move();
            hasMoved = false;

            resolveParasites();
        }

        private void resolveParasites()
        {
            foreach (Parasite p in Parasites)
                p.Digestion.Digest(this);
        }

        private void EatFavoredDiet()
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
                int energy = Digestion.Digest(capturedOrganism);
                if ((energy + this.EnergyTotal) > this.MaxEnergy)
                    this.EnergyTotal = this.MaxEnergy;
                else
                    this.EnergyTotal += energy;
            }
        }

        private Organism detectUnfavoredDiet()
        {
            Type favoredDiet = Digestion.OrganismHungryFor;
            throw new NotImplementedException();
        }

        private Organism detectFavoredDiet()
        {
            Type favoredDiet = Digestion.OrganismHungryFor;
            throw new NotImplementedException();
        }

        private Organism doCapture(Organism detectedOrganism)
        {
            Boolean isCaptured;
            if (detectedOrganism is Animal)
                isCaptured = captureAnimal();
            else
                isCaptured = capturePlant();
            if (isCaptured)
                return detectedOrganism;
            else
                return null;
        }

        private bool captureAnimal()
        {
            throw new NotImplementedException();
        }

        private bool capturePlant()
        {
            throw new NotImplementedException();
        }

        public Animal Reproduce()
        {
            throw new NotImplementedException();
        }

        public override Organism mutate(Organism baseOrganism)
        {
            throw new NotImplementedException();
        }
    }
}
