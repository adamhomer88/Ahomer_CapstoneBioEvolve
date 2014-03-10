using EvolutionModel.Model.Genotypes;
using EvolutionModel.ObserverPattern;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Linq;
using System.Timers;
using System.Threading;
using System.Threading.Tasks;

namespace EvolutionModel.Model.Environment
{
    [Serializable]
    public class BioEvolveEnvironment : INotifyPropertyChanged, Observable, DeathObserver, IDisposable, DeathObservable
    {
        [NonSerialized]private System.Timers.Timer seasonTimer;
        [field:NonSerialized()]
        public event PropertyChangedEventHandler PropertyChanged;
        [field: NonSerialized()]
        private List<Observer> Observers = new List<Observer>();
        [field: NonSerialized]
        private List<DeathObserver> DeathObservers = new List<DeathObserver>();
        private OrganismFactory AbiogenesisFactory;
        private const int ANIMAL_PARASITE_CHANCE = 2;
        private int _abiogenesisRate;
        private int _humidity;
        private int _interval = 2000;
        private int ABIOGENESIS_CHANCE = 102;
        private int DEFAULT_X = 15;
        private int DEFAULT_Y = 15;
        public int X_Size { get; set; }
        public int Y_Size { get; set; }
        private int _season;
        private const int FERTILITY_MULTIPLIER = 25;

        #region AbiogenesisBooleans
        private bool plantIsCreated = false;
        private bool herbivoreIsCreated = false;
        private bool carnivoreIsCreated = false;
        #endregion

        public Dictionary<EnvironmentTile,Plant> EnvironmentPlantLife { get; set; }
        public List<Plant> Plants { get; set; }
        public List<Animal> Animals { get; set; }
        public bool IsPaused { get; set; }
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
        public bool MarkedForClear { get; set; }
        
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
            Plants = new List<Plant>();
            X_Size = x;
            Y_Size = y;
        }
       
        private void ConfigureTimer()
        {
            seasonTimer = new System.Timers.Timer(Interval);
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
            if (!MarkedForClear)
            {
                ReadyForClear = false;
                seasonTimer.Stop();
                seasonTimer.Enabled = false;
                Abiogenesis();
                OrganismTurns();
                OrganismsEnergyBurn();
                PlantReproduction();
                AnimalReproduction();
                ParasiteReproduction();
                this.Season++;
                seasonTimer.Start();
                seasonTimer.Enabled = true;
                rainfall();
            }
            ReadyForClear = true;
        }

        private void rainfall()
        {
            foreach (EnvironmentTile tile in EnvironmentPlantLife.Keys)
            {
                tile.WaterLevel += (int)(this.Humidity * 0.25);
            }
        }

        private void OrganismsEnergyBurn()
        {
            List<Organism> DeadOrganisms = new List<Organism>();
            DeadOrganisms.AddRange(AnimalEnergyBurn());
            DeadOrganisms.AddRange(PlantEnergyBurn());
            ParasiteEnergyBurn();
            ProcessDeadOrganisms(DeadOrganisms);
        }

        private void ProcessDeadOrganisms(List<Organism> DeadOrganisms)
        {
            foreach (Organism o in DeadOrganisms)
                o.Die();
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

        private List<Organism> PlantEnergyBurn()
        {
            List<Organism> DeadPlants = new List<Organism>();
            IEnumerable<Plant> plants = from plant in EnvironmentPlantLife
                                        where plant.Value != null
                                        select plant.Value;
            IEnumerator<Plant> plantsEnumerator = plants.GetEnumerator();
            while (plantsEnumerator.MoveNext())
            {
                Organism DeadPlant = plantsEnumerator.Current.BurnEnergy();
                if (DeadPlant != null)
                    DeadPlants.Add(DeadPlant);
            }
            return DeadPlants;
        }

        private List<Organism> AnimalEnergyBurn()
        {
            List<Organism> DeadAnimals = new List<Organism>();
            foreach (Animal a in Animals)
            {
                Organism animal = a.BurnEnergy();
                if (animal != null)
                    DeadAnimals.Add(animal);
            }
            return DeadAnimals;
        }

        private void AnimalReproduction()
        {
            if (!MarkedForClear)
            {
                List<Animal> NewAnimals = new List<Animal>();
                foreach (Animal a in Animals)
                {
                    Animal childAnimal = a.resolveReproduction();
                    if (childAnimal != null)
                        NewAnimals.Add(childAnimal);
                }
                foreach (Animal a in NewAnimals)
                    this.AddAnimalToEnvironmentThroughReproduction(a);
            }
        }

        private void AddAnimalToEnvironmentThroughReproduction(Animal a)
        {
            Point parentLocation = a.Location;
            a.Location = new Point(a.Location.X, a.Location.Y);
            this.notifyObservers(a as Animal);
            a.SetDeathObserver(this);
            this.AddAnimal(a);
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

        private async void AnimalTurns()
        {
            //CreateAnimalTurnThreads();
            List<Organism> deadOrganisms = new List<Organism>();
            if (Animals.Count > 3)
            {
                int multiple = Animals.Count/4;
                Task firstSet = animalDoTurnAsynch(Animals, 0, multiple);
                Task secondSet = animalDoTurnAsynch(Animals, multiple, multiple*2);
                Task thirdSet = animalDoTurnAsynch(Animals, multiple*2, multiple*3);
                Task fourthSet = animalDoTurnAsynch(Animals, multiple * 3, Animals.Count);
                await firstSet;
                await secondSet;
                await thirdSet;
                await fourthSet;
            }
            else
            {
                animalDoTurn(deadOrganisms);
            }
            
        }

        private void animalDoTurn(List<Organism> deadOrganisms)
        {
            foreach (Animal a in Animals)
            {
                Organism eatenOrganism = a.doTurn();
                if (eatenOrganism != null)
                    deadOrganisms.Add(eatenOrganism);
            }
            foreach (Organism o in deadOrganisms)
            {
                o.Die();
            }
        }

        private void CreateAnimalTurnThreads()
        {
            int count = 0;
            if (Animals.Count >= 2 && Animals.Count < 20)
            {
                Thread newThread = new Thread(()=>animalDoTurnAsynch(Animals,0,count = Animals.Count/2));
                Thread newThread2 = new Thread(()=>animalDoTurnAsynch(Animals,count,Animals.Count));
                newThread.Start();
                newThread.Join();
                newThread2.Start();
                newThread2.Join();
            }
            else if (Animals.Count >= 20 && Animals.Count < 40)
            {
                Thread newThread = new Thread(()=>animalDoTurnAsynch(Animals,0,count = Animals.Count/4));
                Thread newThread2 = new Thread(()=>animalDoTurnAsynch(Animals,count,count = Animals.Count/4+count));
                Thread newThread3 = new Thread(()=>animalDoTurnAsynch(Animals, count, count = Animals.Count/4+count));
                Thread newThread4 = new Thread(()=>animalDoTurnAsynch(Animals,count, Animals.Count));
                newThread.Start();
                newThread.Join();
                newThread2.Start();
                newThread2.Join();
                newThread3.Start();
                newThread3.Join();
                newThread4.Start();
                newThread4.Join();
            }
            else
            {
                if (Animals.Count > 0)
                {
                    Thread thread1 = new Thread(() => animalDoTurnAsynch(Animals, 0, Animals.Count));
                    thread1.Start();
                    thread1.Join();
                }
            }
        }

        private async Task animalDoTurnAsynch(List<Animal> animals, int start, int end)
        {
            List<Organism> deadOrganisms = new List<Organism>();
            for (int i = start; i < end; i++)
            {
                if (i < animals.Count)
                {
                    Organism eatenOrganism = animals[i].doTurn();
                    if (eatenOrganism != null)
                        deadOrganisms.Add(eatenOrganism);
                }
            }
            foreach (Organism o in deadOrganisms)
            {
                o.Die();
            }
        }

        private async void PlantTurns()
        {
            List<Plant> plants = EnvironmentPlantLife.Values.ToList();
            Task firstSet = plantDoTurnAsynch(plants, 0, EnvironmentPlantLife.Values.Count / 3);
            Task secondSet = plantDoTurnAsynch(plants, EnvironmentPlantLife.Values.Count / 3, EnvironmentPlantLife.Values.Count);
            Task thirdSet = plantDoTurnAsynch(plants, EnvironmentPlantLife.Values.Count / 3 + EnvironmentPlantLife.Values.Count / 3, EnvironmentPlantLife.Values.Count);
            await firstSet;
            await secondSet;
            await thirdSet;
        }

        private async Task plantDoTurnAsynch(List<Plant> valueCollection, int start, int end)
        {
            for (int i = start; i < end; i++)
            {
                if (i < valueCollection.Count&&valueCollection[i]!=null)
                    valueCollection[i].doTurn();
            }
        }

        private void PlantReproduction()
        {
            if (!MarkedForClear)
            {
                List<Plant> NewPlants = new List<Plant>();
                foreach (Plant p in EnvironmentPlantLife.Values)
                {
                    if (p != null)
                    {
                        Plant plant = p.resolveReproduction();
                        if (plant != null)
                            NewPlants.Add(plant);
                    }
                }
                foreach (Plant p in NewPlants)
                    AddPlantFromReproduction(p);
            }
        }

        private void AddPlantToEnvironment(Organism organism)
        {
            if (organism != null)
            {
                List<EnvironmentTile> EnvironmentsWithoutPlantLife = (from item in EnvironmentPlantLife
                                                                      where item.Value == null
                                                                      select item.Key).ToList();
                AddPlantToLocalEnvironment(organism, EnvironmentsWithoutPlantLife);
                organism.SetDeathObserver(this);
            }
        }

        private void AddPlantFromReproduction(Plant p)
        {
            List<EnvironmentTile> nearbyTiles = getAllNearTiles(p);
            this.AddPlantToLocalEnvironment(p, nearbyTiles);
            p.SetDeathObserver(this);
        }

        private List<EnvironmentTile> getAllNearTiles(Plant p)
        {
            IEnumerable<EnvironmentTile> nearbyTiles = from tile in this.EnvironmentPlantLife
                                                                            where (tile.Value == null)
                                                                            && ((Math.Abs(tile.Key.X - p.Location.X)) <= 1)
                                                                            && ((Math.Abs(tile.Key.Y - p.Location.Y)) <= 1)
                                                                            select tile.Key;
            return nearbyTiles.ToList();
        }

        private void Abiogenesis()
        {
            Organism organism = null;
            organism = GenerateOrganism(organism);
            if (organism != null)
                AddOrganismToEnvironment(organism);
        }

        private Organism GenerateOrganism(Organism organism)
        {
            if (!plantIsCreated)
            {
                organism = AbiogenesisFactory.randomPlant();
                plantIsCreated = true;
            }
            else if (!herbivoreIsCreated)
            {
                organism = AbiogenesisFactory.randomHerbivore();
                herbivoreIsCreated = true;
            }
            else if (!carnivoreIsCreated)
            {
                organism = AbiogenesisFactory.randomCarnivore();
                carnivoreIsCreated = true;
            }
            else
            {
                int randomNumber = OrganismFactory.random.Next(ABIOGENESIS_CHANCE - AbiogenesisRate);
                if (randomNumber == 0)
                    organism = AbiogenesisFactory.randomOrganism();
            }
            return organism;
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
            int randomX = OrganismFactory.random.Next(Max_X-EnvironmentTile.TILE_SIZE_IN_PIXELS);
            int randomY = OrganismFactory.random.Next(Max_Y-EnvironmentTile.TILE_SIZE_IN_PIXELS);
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
            animal.SetDeathObserver(this);
            this.Animals.Add(animal);
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

        public void Dispose()
        {
            this.seasonTimer.Dispose();
        }

        public void notifyOfDeath(Organism organism)
        {
            if (organism is Animal)
            {
                this.Animals.Remove(organism as Animal);
                Animal deadAnimal = organism as Animal;
                notifyDeathObservers(deadAnimal);
            }
            else if (organism is Plant)
            {
                Plant deadPlant = organism as Plant;
                EnvironmentTile tile = deadPlant.localEnvironment;
                tile.FertilityLevel += deadPlant.Mass*FERTILITY_MULTIPLIER;
                EnvironmentPlantLife[tile] = null;
                notifyDeathObservers(deadPlant);
            }
        }

        public void notifyDeathObservers(Organism organism)
        {
            foreach (DeathObserver DO in DeathObservers)
                DO.notifyOfDeath(organism);
        }

        internal void AddDeathObserver(DeathObserver observer)
        {
            this.DeathObservers.Add(observer);
        }

        private void ParasiteTurns()
        {
            //IEnumerable<List<Parasite>> animalParasites = getAllParasitesInAnimals();
            //IEnumerable<List<Parasite>> plantParasites = getAllParasitesInPlants();
            //ParasiteDoTurn(plantParasites);
            //ParasiteDoTurn(animalParasites);
        }

        public void Pause()
        {
            this.IsPaused = true;
            this.seasonTimer.Enabled = false;
            this.seasonTimer.Stop();
        }

        public void UnPause()
        {
            this.IsPaused = false;
            this.seasonTimer.Enabled = true;
            this.seasonTimer.Start();
        }

        public bool ReadyForClear { get; set; }
    }
}
