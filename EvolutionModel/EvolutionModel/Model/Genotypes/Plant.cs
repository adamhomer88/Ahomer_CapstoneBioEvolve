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
        public int MaxWaterLevel { get; set; }

        #region Phenotypes
        public IEnergyFactory EnergyFactory { get; set; }
        public INutrientAbsorber NutrientAbsorbtion { get; set; }
        #endregion

        
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
        }

        public override Organism mutate(Organism baseOrganism)
        {
            throw new NotImplementedException();
        }
    }
}
