using EvolutionModel.Model.Environment;
using EvolutionModel.Model.Genotypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public void ExecuteBehavior()
        {
            Organism food = animal.Sensory.FindFavoredFood(animal.Digestion.OrganismHungryFor, environment, animal.Location);
            if (food != null)
                this.animal.State = new PursuePreyState(food, animal, environment);
        }
    }
}
