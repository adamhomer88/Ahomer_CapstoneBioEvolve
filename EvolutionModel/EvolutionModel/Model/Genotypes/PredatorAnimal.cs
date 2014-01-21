using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvolutionModel.Model.Genotypes
{
    public class PredatorAnimal : Animal
    {
        public override void Move()
        {
            throw new NotImplementedException();
        }

        public override void doTurn()
        {
            Animal detectedAnimal = detect();
            if (detectedAnimal != null && (this.EnergyTotal / this.MaxEnergy < .75))
            {
                Animal capturedAnimal = doCapture(detectedAnimal);
                if (capturedAnimal != null)
                {
                    digestion.Digest(capturedAnimal);
                }

            }
        }

        private Animal doCapture(Animal detectedAnimal)
        {
            throw new NotImplementedException();
        }

        private Animal detect()
        {
            throw new NotImplementedException();
        }
    }
}
