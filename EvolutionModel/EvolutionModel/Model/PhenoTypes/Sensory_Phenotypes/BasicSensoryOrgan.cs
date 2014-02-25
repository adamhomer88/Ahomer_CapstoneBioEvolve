using EvolutionModel.Model.Environment;
using EvolutionModel.Model.Genotypes;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvolutionModel.Model.PhenoTypes.Sensory_Phenotypes
{
    [Serializable]
    class BasicSensoryOrgan : ISense
    {
        public const int DEFAULT_SENSE_DISTANCE = 60;
        public int SenseDistance { get; set; }

        public BasicSensoryOrgan()
        {
            SenseDistance = DEFAULT_SENSE_DISTANCE;
        }

        public BasicSensoryOrgan(int SenseDistance)
        {
            this.SenseDistance = SenseDistance;
        }

        public IPhenotype Mutate()
        {
            int randomNumber = OrganismFactory.random.Next(10);
            return new BasicSensoryOrgan(randomNumber + this.SenseDistance);
        }

        public Genotypes.Organism FindFavoredFood(Type DigestionType, BioEvolveEnvironment environment, Point Location)
        {
            Organism prey = FindFoodWithinSight(DigestionType, environment, Location);
            return prey;
        }

        private Organism FindFoodWithinSight(Type DigestionType, BioEvolveEnvironment environment, Point Location)
        {
            Organism food = null;
            int posX = Location.X + SenseDistance;
            int posY = Location.Y + SenseDistance;
            int negX = Location.Y - SenseDistance;
            int negY = Location.Y - SenseDistance;
            if(DigestionType == typeof(Plant))
            {
                IEnumerable<KeyValuePair<EnvironmentTile,Plant>> buffet = FindPlantsWithinSight(environment, posX, posY, negX, negY);
                food = FindClosestPlant(buffet);
            }
            else
            {
                IEnumerable<Organism> buffet = FindAnimalsWithinSight(environment, posX, posY, negX, negY);
                food = FindClosestAnimal(buffet);
            }
            return food;
        }

        private Organism FindClosestAnimal(IEnumerable<Organism> buffet)
        {
            Animal food = null;
            foreach (Animal animal in buffet)
            {
                if (food == null)
                    food = animal;
                if (newAnimalIsCloser(food, animal))
                    food = animal;
            }
            return food;
        }

        private bool newAnimalIsCloser(Animal food, Animal animal)
        {
            System.Windows.Vector foodVector = new System.Windows.Vector(food.Location.X, food.Location.Y);
            System.Windows.Vector animalVector = new System.Windows.Vector(animal.Location.X, animal.Location.Y);
            return animalVector.Length < foodVector.Length;
        }

        private Organism FindClosestPlant(IEnumerable<KeyValuePair<EnvironmentTile,Plant>> buffet)
        {
            KeyValuePair<EnvironmentTile, Plant> food = new KeyValuePair<EnvironmentTile,Plant>();
            foreach (KeyValuePair<EnvironmentTile, Plant> kvp in buffet)
            {
                if (food.Equals(new KeyValuePair<EnvironmentTile, Plant>()))
                    food = kvp;
                if (newPlantIsCloser(food, kvp))
                    food = kvp;
            }
            return food.Value;
        }

        private bool newPlantIsCloser(KeyValuePair<EnvironmentTile, Plant> food, KeyValuePair<EnvironmentTile, Plant> kvp)
        {
            System.Windows.Vector foodDistance = new System.Windows.Vector(food.Key.X * 32, kvp.Key.Y * 32);
            System.Windows.Vector newFoodDistance = new System.Windows.Vector(kvp.Key.X * 32, kvp.Key.Y * 32);
            return newFoodDistance.Length < foodDistance.Length;
        }

        private IEnumerable<Organism> FindAnimalsWithinSight(BioEvolveEnvironment environment, int posX, int posY, int negX, int negY)
        {
            return from animals in environment.Animals
                   where animals.Location.X < posX && animals.Location.X > negX && animals.Location.Y < posY && animals.Location.Y > negY
                   select animals;
        }

        private static IEnumerable<KeyValuePair<EnvironmentTile,Plant>> FindPlantsWithinSight(BioEvolveEnvironment environment, int posX, int posY, int negX, int negY)
        {
            IEnumerable<KeyValuePair<EnvironmentTile, Plant>> plantLife = from plants in environment.EnvironmentPlantLife
                                                                          where environment.EnvironmentPlantLife.Values != null
                                                                          select plants;
            return from plants in plantLife
                   where plants.Key.X * 32 < posX && plants.Key.X * 32 > negX && plants.Key.Y * 32 < posY && plants.Key.Y * 32 > negY
                   select plants;
        }

        public Organism FindFood(BioEvolveEnvironment environment, Point Location)
        {
            throw new NotImplementedException();
        }

        public Organism FindMate(BioEvolveEnvironment environment, Point Location)
        {
            throw new NotImplementedException();
        }
    }
}
