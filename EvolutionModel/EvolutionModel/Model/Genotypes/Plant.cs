using EvolutionModel.Model.Environment;
using EvolutionModel.Model.PhenoTypes.Energy_Factory;
using EvolutionModel.Model.PhenoTypes.Water_Absorbtion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvolutionModel.Model.Genotypes
{
    public class Plant : Organism
    {
        public double growthThresholdToNutrients { get; set; }
        public double growthRate { get; set; }
        public int NutrientTotal { get; set; }
        public int MaxNutrient { get; set; }
        public int WaterTotal { get; set; }
        public int MaxWater { get; set; }

        #region DefaultBasePhenotypes
        public const double DEFAULT_GROWTH_THRESHOLD = .6;
        public const double DEFAULT_GROWTH_RATE = 1.05;
        #endregion

        #region Parasites
        public List<Parasite> Parasites { get; set; }
        #endregion

        #region Phenotypes
        public IEnergyFactory EnergyFactory { get; set; }
        public INutrientAbsorber NutrientAbsorbtion { get; set; }
        #endregion

        public Plant()
        {
            this.MaxEnergy = 50;
            this.EnergyTotal = (int)(this.MaxEnergy * .6);
            this.Mass = 1;
            this.MaxNutrient = 50;
            this.MaxWater = 50;
            this.WaterTotal = (int)(this.MaxWater * .6);
            this.NutrientTotal = (int)(this.MaxNutrient*.6);
        }
        
        public Plant Reproduce()
        {
            throw new NotImplementedException();
        }

        public void Grow()
        {
            this.EnergyTotal -= this.MaxEnergy * this.Mass;
            this.Mass += (int)(this.growthRate * this.Mass);
        }

        public override void doTurn(EnvironmentTile localEnvironment)
        {
            int waterLevel = localEnvironment.waterLevel;
            int fertilityLevel = localEnvironment.fertilityLevel;

            int nutrientsAbsorbed = NutrientAbsorbtion.absorbNutrients(localEnvironment, this.Mass);
            int newWaterTotal = WaterTotal;
            int energyCreated = EnergyFactory.createEnergy(out newWaterTotal, this.Mass);

            if ((NutrientTotal / MaxNutrient) > growthThresholdToNutrients)
                Grow();

            resolveParasites();
        }

        private void resolveParasites()
        {
            foreach (Parasite p in Parasites)
                p.Digestion.Digest(this);
        }

        public override Organism mutate(Organism baseOrganism)
        {
            throw new NotImplementedException();
        }
    }
}
