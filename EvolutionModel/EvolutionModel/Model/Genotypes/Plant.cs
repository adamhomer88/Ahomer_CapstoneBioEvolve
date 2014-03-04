using EvolutionModel.Model.Environment;
using EvolutionModel.Model.Mutation;
using EvolutionModel.Model.PhenoTypes.Energy_Factory;
using EvolutionModel.Model.PhenoTypes.Water_Absorbtion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace EvolutionModel.Model.Genotypes
{
    [Serializable]
    public class Plant : Organism
    {
        public EnvironmentTile localEnvironment { get; set; }

        #region BasicPhenotypes
        public double growthThresholdToEnergy { get; set; }
        public double growthRate { get; set; }
        public int NutrientTotal { get; set; }
        public int MaxNutrient { get; set; }
        public int WaterTotal { get; set; }
        public int MaxWater { get; set; }
        public int ReproductionRate { get; set; }
        private int seasonsWaited = 0;
        #endregion

        #region DefaultBasePhenotypes
        public const double DEFAULT_GROWTH_THRESHOLD = .6;
        public const double DEFAULT_GROWTH_RATE = .25;
        public const int DEFAULT_REPRODUCTION_RATE = 3;
        private const double REPRODUCTION_THRESHOLD = .8;
        #endregion

        #region Phenotypes
        public IEnergyFactory EnergyFactory { get; set; }
        public INutrientAbsorber NutrientAbsorbtion { get; set; }
        #endregion

        public Plant()
        {
            this.Mass = 1;
            this.MaximumMass = Mass * MAX_MASS_MULTIPLIER;
            this.ChildMass = 1;
            this.EnergyTotal = (int)(this.MaxEnergy);
            this.MaxNutrient = 50;
            this.MaxWater = 50;
            this.WaterTotal = (int)(this.MaxWater * .6);
            this.NutrientTotal = (int)(this.MaxNutrient*.6);
            this.ReproductionRate = DEFAULT_REPRODUCTION_RATE;
            this.Parasites = new List<Parasite>();
        }
        
        public void Grow()
        {
            int growth = (int)(this.growthRate * this.Mass);
            if (growth < 1)
                growth = 1;
            if (this.Mass + growth > this.MaximumMass)
                this.Mass = this.MaximumMass;
            else
                this.Mass += growth;
            this.EnergyTotal -= this.Mass * this.growthRate;
        }

        public override void doTurn()
        {
            AbsorbFromEnvironment();

            if (CanGrowLarger())
                Grow();
        }

        private bool CanGrowLarger()
        {
            return (EnergyTotal / MaxEnergy) > growthThresholdToEnergy && this.Mass < this.MaximumMass;
        }

        public Plant resolveReproduction()
        {
            Plant childPlant = null;
            if (seasonsWaited >= ReproductionRate)
            {
                childPlant = (Plant)Reproduce();
                childPlant.Location = new Point(this.localEnvironment.X, this.localEnvironment.Y);
                seasonsWaited = 0;
            }
            else
                seasonsWaited++;
            return childPlant;
        }

        private void AbsorbFromEnvironment()
        {
             this.absorbNutrients(NutrientAbsorbtion.absorbNutrients(localEnvironment, this.Mass));
             this.absorbWater(NutrientAbsorbtion.absorbWater(localEnvironment, this.Mass));
             EnergyFactory.createEnergy(this);
        }


        private void absorbWater(int p)
        {
            if (this.WaterTotal + p > this.MaxWater)
                this.WaterTotal = this.MaxWater;
            else
                this.WaterTotal += p;
        }

        private void absorbNutrients(int p)
        {
            if (this.NutrientTotal + p > this.MaxNutrient)
                this.NutrientTotal = this.MaxNutrient;
            else
                this.NutrientTotal += p;
        }

        private void resolveParasites()
        {
            foreach (Parasite p in Parasites)
                p.doTurn();
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
