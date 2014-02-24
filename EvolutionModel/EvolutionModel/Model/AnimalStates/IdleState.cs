using EvolutionModel.Model.Environment;
using EvolutionModel.Model.Genotypes;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvolutionModel.Model.AnimalStates
{
    [Serializable]
    class IdleState : IAnimalState
    {
        public Animal animal { get; set; }
        private BioEvolveEnvironment environment;

        public IdleState(Animal model, BioEvolveEnvironment environment)
        {
            this.animal = model;
            this.environment = environment;
        }

        public void ExecuteBehavior()
        {
            if (animal.EnergyTotal > animal.favoredHungerThreshold)
            {
                Wander();
            }
            else
            {
                animal.State = new LookingForPreyState(animal, environment);
                animal.State.ExecuteBehavior();
            }
        }

        private void Wander()
        {
            int speed = animal.getTotalSpeed();
            Random randomGen = OrganismFactory.random;
            int xChange = randomGen.Next((-speed+1),speed);
            int yChange = randomGen.Next((-speed+1),speed);
            Point originalLocation = animal.Location;
            animal.Location = new Point(originalLocation.X+xChange,originalLocation.Y+yChange);
        }
    }
}
