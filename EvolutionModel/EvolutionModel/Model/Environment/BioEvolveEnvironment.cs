using EvolutionModel.Model.Genotypes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace EvolutionModel.Model.Environment
{
    public class BioEvolveEnvironment : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private OrganismFactory AbiogenesisFactory = new OrganismFactory();
        private const int ANIMAL_PARASITE_CHANCE = 2;
        private int _abiogenesisRate;
        private int _humidity;
        private int _interval = 5000;
        private int ABIOGENESIS_CHANCE = 105;
        private int DEFAULT_X = 50;
        private int DEFAULT_Y = 50;
        public int X_Size { get; set; }
        public int Y_Size { get; set; }
        private int Season_Max = 4;
        private int _season;
        private Timer seasonTimer;

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
            EnvironmentPlantLife = new Dictionary<EnvironmentTile, Plant>();
            seasonTimer = new Timer(Interval);
            seasonTimer.Elapsed += new ElapsedEventHandler(Season_End);
            X_Size = x;
            Y_Size = y;
        }
       
        public BioEvolveEnvironment()
        {
            EnvironmentPlantLife = new Dictionary<EnvironmentTile, Plant>();
            seasonTimer = new Timer(Interval);
            X_Size = DEFAULT_X;
            Y_Size = DEFAULT_Y;
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
            seasonTimer.Start();
        }

        private void Season_End(object sender, ElapsedEventArgs e)
        {
            Abiogenesis();
            PlantReproduction();
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
            if (organism is Animal)
                AddAnimalToEnvironment(organism);
            if (organism is Plant)
                AddPlantToEnvironment(organism);
        }

        private void AddPlantToEnvironment(Organism organism)
        {
            List<EnvironmentTile> EnvironmentsWithoutPlantLife = (from item in EnvironmentPlantLife
                                                                 where item.Value == null
                                                                 select item.Key).ToList();
            int randomNumber = OrganismFactory.random.Next(EnvironmentsWithoutPlantLife.Count);
            EnvironmentPlantLife.Add(EnvironmentsWithoutPlantLife[randomNumber], (Plant)organism);
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
        }

        private void AddParasiteToEnvironment(Organism organism)
        {
            int randomNumber = OrganismFactory.random.Next(3);
            if (randomNumber < ANIMAL_PARASITE_CHANCE)
                AttachParasiteToAnimal((Parasite)organism);
            else
                AttachParasiteToPlant((Parasite)organism);
        }

        private int AttachParasiteToPlant(Parasite organism)
        {
            List<Plant> Plants = (from plant in EnvironmentPlantLife.Values
                         where plant != null
                         select plant).ToList();
            int randomNumber = OrganismFactory.random.Next(Plants.Count);
            Plants[randomNumber].Parasites.Add(organism);
            return randomNumber;
        }

        private int AttachParasiteToAnimal(Parasite organism)
        {
            int randomNumber = OrganismFactory.random.Next(Animals.Count);
            Animals[randomNumber].Parasites.Add(organism);
            return randomNumber;
        }

        private void AddAnimal(Animal animal)
        {
            this.Animals.Add(animal);
            OnPropertyChanged("Animals");
        }

    }
}
