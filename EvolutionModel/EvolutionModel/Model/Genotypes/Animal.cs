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
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using EvolutionModel.Model.Mutation;

namespace EvolutionModel.Model.Genotypes
{
    public class Animal : Organism
    {
        Boolean hasMoved = false;

        #region BasicPhenotypes
        public double favoredHungerThreshold { get; set; }
        public double unfavoredHungerThreshold { get; set; }
        public double reproductionThreshold { get; set; }
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
        public List<IAppendage> VestigialLimbs { get; set; }
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
            hasMoved = HuntForFood();
            if (this.EnergyTotal / this.MaxEnergy > reproductionThreshold)
            {
                Animal childAnimal = (Animal)Reproduce(this);
                addToEnvironment(localEnvironment, childAnimal);
            }
            if (!hasMoved)
                Move();
            hasMoved = false;

            resolveParasites(localEnvironment);
        }

        private void addToEnvironment(EnvironmentTile localEnvironment, Animal childAnimal)
        {
            throw new NotImplementedException();
        }

        private void resolveParasites(EnvironmentTile localEnvironment)
        {
            foreach (Parasite p in Parasites)
                p.doTurn(localEnvironment);
        }

        private bool HuntForFood()
        {
            bool hasMoved = false;
            Organism detectedOrganism = detectFavoredDiet();
            if (detectedOrganism != null && (isHungryForFavoredDiet()))
            {
                hasMoved = true;
                Organism capturedOrganism = doCapture(detectedOrganism);
                if(capturedOrganism != null)
                    EatOrganism(capturedOrganism);
            }
            else if (detectedOrganism==null && isHungryForUnfavoredDiet())
            {
                detectedOrganism = detectUnfavoredDiet();
                if (detectedOrganism != null)
                {
                    Organism capturedOrganism = doCapture(detectedOrganism);
                    if(capturedOrganism != null)
                        EatOrganism(capturedOrganism);
                }
            }
            return hasMoved;
        }

        private bool isHungryForUnfavoredDiet()
        {
            return this.EnergyTotal / this.MaxEnergy < unfavoredHungerThreshold;
        }

        private bool isHungryForFavoredDiet()
        {
            return this.EnergyTotal / this.MaxEnergy < favoredHungerThreshold;
        }

        private void EatOrganism(Organism capturedOrganism)
        {
            
                int energy = Digestion.Digest(capturedOrganism);
                if ((energy + this.EnergyTotal) >= this.MaxEnergy)
                    this.EnergyTotal = this.MaxEnergy;
                else
                    this.EnergyTotal += energy;
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

        public override Organism basicMutate(Organism baseOrganism)
        {
            Mutator basicMutator = Mutator.GetBasicInstance();
            Organism newMutatedOrganism = basicMutator.Mutate(baseOrganism);
            return newMutatedOrganism;
        }

        public override Organism complexMutate(Organism baseOrganism)
        {
            Mutator complexMutator = Mutator.GetComplexInstance();
            Organism newMutatedOrganism = complexMutator.Mutate(baseOrganism);
            return newMutatedOrganism;
        }
    }
}
