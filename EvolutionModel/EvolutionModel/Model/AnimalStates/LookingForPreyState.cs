using EvolutionModel.Model.Environment;
using EvolutionModel.Model.Genotypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace EvolutionModel.Model.AnimalStates
{
    [Serializable]
    public class LookingForPreyState : IAnimalState
    {
        public Animal animal { get; set; }
        private BioEvolveEnvironment environment;

        public LookingForPreyState(Animal model, BioEvolveEnvironment environment)
        {
            this.animal = model;
            this.environment = environment;
        }

        public Organism ExecuteBehavior()
        {
            Organism food = animal.Sensory.FindFavoredFood(animal.Digestion.OrganismHungryFor, environment, animal.Location);
            if (food != null)
                this.animal.State = new PursuePreyState(food, animal, environment);
            else
                Wander();
            return null;
        }

        private void Wander()
        {
            int speed = animal.getTotalSpeed();
            Random randomGen = OrganismFactory.random;
            int xChange = randomGen.Next((-speed + 1), speed);
            int yChange = randomGen.Next((-speed + 1), speed);
            Point originalLocation = animal.Location;
            animal.Location = new Point(originalLocation.X + xChange, originalLocation.Y + yChange);
        }

        public override string ToString()
        {
            return "Looking for food.";
        }
    }
}
