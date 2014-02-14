using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvolutionModel.Model.Environment
{
    public class EnvironmentGenerator
    {
        private static Random generator = new Random();
        private const int JUNGLE_MAX_WATER = 1000;
        private const int JUNGLE_MIN_WATER = 600;
        private const int JUNGLE_MAX_FERTILITY = 1000;
        private const int JUNGLE_MIN_FERTILITY = 600;
        private const int JUNGLE_ABIOGENESIS_DEFAULT = 5;
        private const int JUNGLE_HUMIDITY_DEFAULT = 75;

        public static BioEvolveEnvironment Jungle(int x, int y)
        {
            BioEvolveEnvironment environment = new BioEvolveEnvironment(x, y);
            configureEnvironment(environment, JUNGLE_MAX_WATER, JUNGLE_MIN_WATER, JUNGLE_MAX_FERTILITY, JUNGLE_MIN_FERTILITY);
            return environment;
        }

        private static void configureEnvironment(BioEvolveEnvironment environment, int maxWater, int minWater, int maxFertility, int minFertility)
        {
            for (int i = 0; i < environment.Y_Size; i++)
            {
                for (int j = 0; j < environment.X_Size; j++)
                {
                    int waterLevel = generator.Next(minWater, maxWater);
                    int fertilityLevel = generator.Next(minFertility, maxFertility);
                    environment.EnvironmentPlantLife.Add(new EnvironmentTile(waterLevel, fertilityLevel,j,i),null);
                }
            }
            environment.AbiogenesisRate = JUNGLE_ABIOGENESIS_DEFAULT;
            environment.Humidity = JUNGLE_HUMIDITY_DEFAULT;
        }

        public static BioEvolveEnvironment Jungle()
        {
            BioEvolveEnvironment environment = new BioEvolveEnvironment();
            configureEnvironment(environment, JUNGLE_MAX_WATER, JUNGLE_MIN_WATER, JUNGLE_MAX_FERTILITY, JUNGLE_MIN_FERTILITY);
            return environment;
        }
    }
}
