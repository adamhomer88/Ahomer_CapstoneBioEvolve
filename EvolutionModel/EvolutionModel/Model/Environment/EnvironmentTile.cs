using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvolutionModel.Model.Environment
{
    public class EnvironmentTile
    {
        public int waterLevel { get; set; }
        public int fertilityLevel { get; set; }
        public List<DeadOrganism> Carcasses = new List<DeadOrganism>();

        public EnvironmentTile(int waterLevel, int fertilityLevel)
        {
            this.waterLevel = waterLevel;
            this.fertilityLevel = fertilityLevel;
        }

        public void addWater(int water)
        {
            waterLevel += water;
        }

        public void addFertility(int fertility)
        {
            fertilityLevel += fertility;
        }
    }
}
