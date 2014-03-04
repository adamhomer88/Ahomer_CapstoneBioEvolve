using EvolutionModel.Model.Genotypes;
using EvolutionModel.ObserverPattern;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace EvolutionModel.Model.Environment
{
    [Serializable]
    public class BioEvolveEnvironment : INotifyPropertyChanged, Observable
    {
        [field:NonSerialized()]
        public event PropertyChangedEventHandler PropertyChanged;
        [field: NonSerialized()]
        private List<Observer> Observers = new List<Observer>();
        [NonSerialized]private Timer seasonTimer;
        private OrganismFactory AbiogenesisFactory;
        private const int ANIMAL_PARASITE_CHANCE = 2;
        private int _abiogenesisRate;
        private int _humidity;
        private int _interval = 2000;
        private int ABIOGENESIS_CHANCE = 102;
        private int DEFAULT_X = 50;
        private int DEFAULT_Y = 50;
        public int X_Size { get; set; }
        public int Y_Size { get; set; }
        private int Season_Max = 4;
        private int _season;

        public Dictionary<EnvironmentTile,Plant> EnvironmentPlantLife { get; set; }
        public List<Animal> Animals { get; set; }

        public int Season
        {
            get { return _season; }
            set
            {
                _season = value;
                OnPropertyChanged("Season");
            }
        }

        public int Interval
        {
            get { return _interval; }
            set
            {
                _interval = value;
                this.seasonTimer.Interval = value;
                OnPropertyChanged("Interval");
            }
        }

        public int Humidity
        {
            get { return _humidity; }
            set
            {
                _humidity = value;
                OnPropertyChanged("Humidity");
            }
        }
        public int AbiogenesisRate
        {
            get { return _abiogenesisRate; }
            set 
            {
                _abiogenesisRate = value;
                OnPropertyChanged("AbiogenesisRate");
            }
        }

        public BioEvolveEnvironment(int x, int y)
        {
            ConfigureEnvironment(x, y);
        }

        private void ConfigureEnvironment(int x, int y)
        {
            this.AbiogenesisFactory = new OrganismFactory(this);
            EnvironmentPlantLife = new Dictionary<EnvironmentTile, Plant>();
            ConfigureTimer();
            Animals = new List<Animal>();
            X_Size = x;
            Y_Size = y;
        }

        private void ConfigureTimer()
        {
            seasonTimer = new Timer(Interval);
            seasonTimer.Elapsed += new ElapsedEventHandler(Season_End);
        }
       
        public BioEvolveEnvironment()
        {
            ConfigureEnvironment(DEFAULT_X, DEFAULT_Y);
        }

        private void OnPropertyChanged(string info)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(info));
            }
        }

        public void Simulate()
        {
            seasonTimer.Enabled = true;
            seasonTimer.Start();
        }

        private void Season_End(object sender, ElapsedEventArgs e)
        {
            seasonTimer.Stop();
            seasonTimer.Enabled = false;
            Abiogenesis();
            OrganismTurns();
            OrganismsEnergyBurn();
            PlantReproduction();
            AnimalReproduction();
            ParasiteReproduction();
            seasonTimer.Start();
            seasonTimer.Enabled = true;
        }

        private void OrganismsEnergyBurn()
        {
            AnimalEnergyBurn();
            PlantEnergyBurn();
            ParasiteEnergyBurn();
        }

        private void ParasiteEnergyBurn()
        {
            IEnumerable<List<Parasite>> animalParasites = getAllParasitesInAnimals();
            IEnumerable<List<Parasite>> plantParasites = getAllParasitesInPlants();
           
            BurnEnergyFromParasiteList(animalParasites);
            BurnEnergyFromParasiteList(plantParasites);
        }

        private void BurnEnergyFromParasiteList(IEnumerable<List<Parasite>> parasites)
        {
            foreach (List<Parasite> pList in parasites)
            {
                foreach (Parasite p in pList)
                {
                    p.BurnEnergy();
                }
            }
        }

        private void PlantEnergyBurn()
        {
            foreach (Plant p in EnvironmentPlantLife.Values)
            {
                if (p != null)
                    p.BurnEnergy();
            }
        }

        private void AnimalEnergyBurn()
        {
            foreach (Animal a in Animals)
            {
                a.EnergyTotal -= a.EnergyPerTurn;
            }
        }

        private void AnimalReproduction()
        {
            List<Animal> NewAnimals = new List<Animal>();
            foreach (Animal a in Animals)
            {
                Animal childAnimal = (Animal)a.Reproduce();
                NewAnimals.Add(childAnimal);
            }
            foreach(Animal a in NewAnimals)
                this.AddAnimalToEnvironment(a);
        }

        private void ParasiteReproduction()
        {
            IEnumerable<List<Parasite>> animalParasites = getAllParasitesInAnimals();
            IEnumerable<List<Parasite>> plantParasites = getAllParasitesInPlants();

            ReproduceParasites(plantParasites);
            ReproduceParasites(animalParasites);
        }

        private void ReproduceParasites(IEnumerable<List<Parasite>> parasites)
        {
            List<Parasite> NewParasites = new List<Parasite>();
            foreach (List<Parasite> pList in parasites)
            {
                foreach (Parasite p in pList)
                {
                    Organism childParasite = p.Reproduce();
                }
            }
            foreach(Parasite p in NewParasites)
                AddParasiteToEnvironment(p);
        }
    
        private void OrganismTurns()
        {
            PlantTurns();
            AnimalTurns();
            ParasiteTurns();
        }

        private void ParasiteTurns()
        {
            IEnumerable<List<Parasite>> animalParasites = getAllParasitesInAnimals();
            IEnumerable<List<Parasite>> plantParasites = getAllParasitesInPlants();
            ParasiteDoTurn(plantParasites);
            ParasiteDoTurn(animalParasites);
        }

        private static void ParasiteDoTurn(IEnumerable<List<Parasite>> plantParasites)
        {
            foreach (List<Parasite> pList in plantParasites)
            {
                foreach (Parasite p in pList)
                {
                    p.doTurn();
                }
            }
        }

        private IEnumerable<List<Parasite>> getAllParasitesInAnimals()
        {
            IEnumerable<List<Parasite>> parasiteLists = from aLists in Animals
                                                        where aLists.Parasites.Count != 0
                                                        select aLists.Parasites;
            return parasiteLists;
        }

        private IEnumerable<List<Parasite>> getAllParasitesInPlants()
        {
            IEnumerable<List<Parasite>> parasiteLists = from aLists in EnvironmentPlantLife.Values
                                                        where aLists != null && aLists.Parasites.Count != 0
                                                        select aLists.Parasites;
            return parasiteLists;
        }

        private void AnimalTurns()
        {
            foreach (Animal a in Animals)
            {
                a.doTurn();
            }
        }

        private void PlantTurns()
        {
            foreach (Plant p in EnvironmentPlantLife.Values)
            {
                if (p != null)
                    p.doTurn();
            }
        }

        private void PlantReproduction()
        {
            List<Plant> NewPlants = new List<Plant>();
            foreach (Plant p in EnvironmentPlantLife.Values)
            {
                if (p != null)
                {
                    Plant plant = p.resolveReproduction();
                    NewPlants.Add(plant);
                }
            }
            foreach(Plant p in NewPlants)
                AddPlantToEnvironment(p);
        }

        private void Abiogenesis()
        {
            int randomNumber = OrganismFactory.random.Next(ABIOGENESIS_CHANCE - AbiogenesisRate);
            Organism organism = null;
            if(randomNumber == 0)
                organism = AbiogenesisFactory.randomOrganism();
            if (organism != null)
                AddOrganismToEnvironment(organism);
        }

        private void AddOrganismToEnvironment(Organism organism)
        {
            if(organism is Parasite)
                AddParasiteToEnvironment(organism);
            else if (organism is Animal)
                AddAnimalToEnvironment(organism);
            else if (organism is Plant)
                AddPlantToEnvironment(organism);
        }

        private void AddPlantToEnvironment(Organism organism)
        {
            if (organism != null)
            {
                List<EnvironmentTile> EnvironmentsWithoutPlantLife = (from item in EnvironmentPlantLife
                                                                     where item.Value == null
                                                                     select item.Key).ToList();
                AddPlantToLocalEnvironment(organism, EnvironmentsWithoutPlantLife);
            }
        }

        private void AddPlantToLocalEnvironment(Organism organism, List<EnvironmentTile> EnvironmentsWithoutPlantLife)
        {
            if (EnvironmentsWithoutPlantLife.Count != 0)
            {
                Plant plant = organism as Plant;
                int randomNumber = OrganismFactory.random.Next(EnvironmentsWithoutPlantLife.Count);
                EnvironmentPlantLife[EnvironmentsWithoutPlantLife[randomNumber]] = plant;
                plant.localEnvironment = EnvironmentsWithoutPlantLife[randomNumber];
                organism.Location = new Point(plant.localEnvironment.X * 32, plant.localEnvironment.Y * 32);
                this.notifyObservers(EnvironmentsWithoutPlantLife[randomNumber], organism as Plant);
            }
        }

        private void AddAnimalToEnvironment(Organism organism)
        {
            Animal animal = organism as Animal;
            int Max_X = X_Size*EnvironmentTile.TILE_SIZE_IN_PIXELS;
            int Max_Y = Y_Size*EnvironmentTile.TILE_SIZE_IN_PIXELS;
            int randomX = OrganismFactory.random.Next(Max_X);
            int randomY = OrganismFactory.random.Next(Max_Y);
            animal.Location = new Point(randomX, randomY);
            AddAnimal(animal);
            this.notifyObservers(organism as Animal);
        }

        private void AddParasiteToEnvironment(Organism organism)
        {
            int randomNumber = OrganismFactory.random.Next(3);
            if (randomNumber < ANIMAL_PARASITE_CHANCE)
                AttachParasiteToAnimal((Parasite)organism);
            else
                AttachParasiteToPlant((Parasite)organism);
        }

        private void AddParasiteToEnvironment(Organism parasite, Organism host)
        {
            if(parasite is Parasite)
                host.Parasites.Add(parasite as Parasite);
        }

        private void AttachParasiteToPlant(Parasite organism)
        {
            List<Plant> Plants = (from plant in EnvironmentPlantLife.Values
                         where plant != null
                                  select plant).ToList();
            if (Plants.Count != 0)
            {
                int randomNumber = OrganismFactory.random.Next(Plants.Count);
                Plants[randomNumber].Parasites.Add(organism);
            }
        }

        private void AttachParasiteToAnimal(Parasite organism)
        {
            if (Animals.Count != 0)
            {
                int randomNumber = OrganismFactory.random.Next(Animals.Count);
                Animals[randomNumber].Parasites.Add(organism);
            }
        }

        private void AddAnimal(Animal animal)
        {
            this.Animals.Add(animal);
            OnPropertyChanged("Animals");
        }

        public void AddObserver(Observer o)
        {
            this.Observers.Add(o);
        }

        public void notifyObservers(Animal animal)
        {
            foreach (Observer obs in Observers)
                obs.notify(animal);
        }


        public void notifyObservers()
        {
            foreach (Observer obs in Observers)
                obs.notify();
        }

        public void notifyObservers(EnvironmentTile tile, Plant plant)
        {
            foreach (Observer obs in Observers)
                obs.notify(tile, plant);
        }
    }
}
