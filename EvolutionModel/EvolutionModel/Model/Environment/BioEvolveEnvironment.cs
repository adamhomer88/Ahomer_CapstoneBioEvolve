using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvolutionModel.Model.Environment
{
    public class BioEvolveEnvironment
    {
        private int DEFAULT_X = 50;
        private int DEFAULT_Y = 50;
        public EnvironmentTile[,] tiles { get; set; }
        public int AbiogenesisRate { get; set; }
        public int Humidity { get; set; }

        public BioEvolveEnvironment(int x, int y)
        {
            tiles = new EnvironmentTile[x, y];
        }
        public BioEvolveEnvironment()
        {
            tiles = new EnvironmentTile[DEFAULT_X, DEFAULT_Y];
        }
    }
}
