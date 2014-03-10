using EvolutionModel.Model.Environment;
using EvolutionModel.Model.Genotypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace EvolutionModel.Model.PhenoTypes.Sensory_Phenotypes
{
    [Serializable]
    class BasicSensoryOrgan : ISense
    {
        public const int DEFAULT_SENSE_DISTANCE = 60;
        public int SenseDistance { get; set; }
        public Animal Owner { get; set; }

        public BasicSensoryOrgan(Animal owner)
        {
            SenseDistance = DEFAULT_SENSE_DISTANCE;
            this.Owner = owner;
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
            if(DigestionType == typeof(Plant))
            {
                IEnumerable<KeyValuePair<EnvironmentTile, Plant>> buffet = FindPlantsWithinSight(environment, Location);
                food = FindClosestPlant(buffet);
            }
            else
            {
                IEnumerable<Organism> buffet = FindAnimalsWithinSight(environment, Location);
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

        private IEnumerable<Organism> FindAnimalsWithinSight(BioEvolveEnvironment environment, Point location)
        {
            List<Animal> animalsInSight = addAnimalsInSightToList(location, environment);
            return animalsInSight;
        }

        private List<Animal> addAnimalsInSightToList(Point location, BioEvolveEnvironment environment)
        {
            List<Animal> animals = new List<Animal>();
            foreach (Animal animal in environment.Animals)
            {
                if (DistanceFormula(animal.Location.X, location.X, animal.Location.Y, location.Y) < this.SenseDistance && animal!=Owner)
                {
                    animals.Add(animal);
                }
            }
            return animals;
        }

        private List<KeyValuePair<EnvironmentTile, Plant>> FindPlantsWithinSight(BioEvolveEnvironment environment, Point location)
        {
            IEnumerable<KeyValuePair<EnvironmentTile, Plant>> plantLife = (from plants in environment.EnvironmentPlantLife
                                                                          where plants.Value != null
                                                                          select plants).ToList();
            List<KeyValuePair<EnvironmentTile, Plant>> PlantLocations = addPlantsWithinSightToList(location, plantLife);
            
            return PlantLocations;
        }

        private List<KeyValuePair<EnvironmentTile, Plant>> addPlantsWithinSightToList(Point location, IEnumerable<KeyValuePair<EnvironmentTile, Plant>> plantLife)
        {
            List<KeyValuePair<EnvironmentTile, Plant>> plants = new List<KeyValuePair<EnvironmentTile, Plant>>();
            foreach (KeyValuePair<EnvironmentTile, Plant> PlantEnvironment in plantLife)
            {
                if (((int)DistanceFormula(PlantEnvironment.Value.Location.X, location.X, PlantEnvironment.Value.Location.Y, location.Y)) < SenseDistance)
                {
                    plants.Add(PlantEnvironment);
                }
            }
        return plants;
        }

        public double DistanceFormula(double x1, double x2, double y1, double y2)
        {
            int distance = (int)Math.Sqrt(Math.Pow(Math.Abs(x1 - x2), 2) + Math.Pow(Math.Abs(y1 - y2), 2));
            return distance;
        }

        public override string ToString()
        {
            return "Basic Senses";
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
