using EvolutionModel.Model.PhenoTypes.Skin;
using EvolutionModel.Model.PhenoTypes.Digestion;
using EvolutionModel.Model.PhenoTypes.Head;
using EvolutionModel.Model.PhenoTypes.Limbs;
using EvolutionModel.Model.PhenoTypes.Sensory_Phenotypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EvolutionModel.Model.Environment;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using EvolutionModel.Model.Mutation;
using System.Drawing;
using System.Runtime.Serialization;
using System.ComponentModel;
using EvolutionModel.Model.AnimalStates;
using EvolutionModel.ObserverPattern;

namespace EvolutionModel.Model.Genotypes
{
    [Serializable]
    public class Animal : Organism, INotifyPropertyChanged, Observable
    {
        Boolean hasMoved = false;
        [field:NonSerialized] private Observer Observer;
        public IAnimalState State { get; set; }

        Point _location = new Point(0, 0);

        public Point Location
        {
            get { return _location; }
            set
            {
                this._location = value;
                notifyObservers();
                OnPropertyChanged("Location");
            }
        }

        #region BasicPhenotypes
        public double favoredHungerThreshold { get; set; }
        public double unfavoredHungerThreshold { get; set; }
        public double reproductionThreshold { get; set; }
        #endregion

        #region DefaultBasePhenotypes
        public const double DEFAULT_FAVORED_HUNGER_THRESHOLD = .6;
        public const double DEFAULT_UNFAVORED_HUNGER_THRESHOLD = .2;
        public const double DEFAULT_REPRODUCTION_THRESHOLD = .8;
        public const int DEFAULT_BASE_SPEED = 5;
        #endregion

        #region Phenotypes
        public int Limb_Count { get; set; }
        public Head head { get; set; }
        public ISense Sensory { get; set; }
        public List<IProtectivePhenotype> Skin { get; set; }
        public List<Limb> Limbs { get; set; }
        public List<Limb> VestigialLimbs { get; set; }
        public Boolean isColdBlooded { get; set; }
        public int BaseSpeed { get; set; }
        #endregion

        public Animal(BioEvolveEnvironment environment)
        {
            this.Mass = 1;
            this.MaxEnergy = 50;
            this.EnergyTotal = (int)(this.MaxEnergy * .6);
            this.EnergyPerTurn = 5;
            this.Generation = 1;
            this.BaseSpeed = DEFAULT_BASE_SPEED;
            initializeClassPhenotypes();
            Parasites = new List<Parasite>();
            State = new IdleState(this, environment);
        }

        private void initializeClassPhenotypes()
        {
            this.Skin = new List<IProtectivePhenotype>();
            this.Limbs = new List<Limb>();
            this.VestigialLimbs = new List<Limb>();
        }

        public override void doTurn()
        {
            State.ExecuteBehavior();
            //hasMoved = HuntForFood();
            //if (!hasMoved)
            //    Move();
            //hasMoved = false;
        }

        private void resolveParasites()
        {
            foreach (Parasite p in Parasites)
                p.doTurn();
        }

        private bool HuntForFood()
        {
            bool hasMoved = false;
            Organism detectedOrganism = detectFavoredDiet();
            if (detectedOrganism != null && (isHungryForFavoredDiet()))
            {
                hasMoved = true;
                Organism capturedOrganism = doCapture(detectedOrganism);
                if (capturedOrganism != null)
                    EatOrganism(capturedOrganism);
            }
            else if (detectedOrganism == null && isHungryForUnfavoredDiet())
            {
                detectedOrganism = detectUnfavoredDiet();
                if (detectedOrganism != null)
                {
                    Organism capturedOrganism = doCapture(detectedOrganism);
                    if (capturedOrganism != null)
                        EatOrganism(capturedOrganism);
                }
            }
            return hasMoved;
        }

        private bool isHungryForUnfavoredDiet()
        {
            return this.EnergyTotal / this.MaxEnergy < unfavoredHungerThreshold;
        }

        private bool isHungryForFavoredDiet()
        {
            return this.EnergyTotal / this.MaxEnergy < favoredHungerThreshold;
        }

        private void EatOrganism(Organism capturedOrganism)
        {

            int energy = Digestion.Digest(capturedOrganism);
            if ((energy + this.EnergyTotal) >= this.MaxEnergy)
                this.EnergyTotal = this.MaxEnergy;
            else
                this.EnergyTotal += energy;
        }

        private Organism detectUnfavoredDiet()
        {
            Type favoredDiet = Digestion.OrganismHungryFor;
            throw new NotImplementedException();
        }

        private Organism detectFavoredDiet()
        {
            Type favoredDiet = Digestion.OrganismHungryFor;
            throw new NotImplementedException();
        }

        private Organism doCapture(Organism detectedOrganism)
        {
            Boolean isCaptured;
            if (detectedOrganism is Animal)
                isCaptured = captureAnimal();
            else
                isCaptured = capturePlant();
            if (isCaptured)
                return detectedOrganism;
            else
                return null;
        }

        private bool captureAnimal()
        {
            throw new NotImplementedException();
        }

        private bool capturePlant()
        {
            throw new NotImplementedException();
        }

        public override Organism basicMutate(Organism baseOrganism)
        {
            Mutator basicMutator = Mutator.GetBasicInstance();
            Organism newMutatedOrganism = basicMutator.Mutate(baseOrganism);
            return newMutatedOrganism;
        }

        public override Organism complexMutate(Organism baseOrganism)
        {
            Mutator complexMutator = Mutator.GetComplexInstance();
            Organism newMutatedOrganism = complexMutator.Mutate(baseOrganism);
            return newMutatedOrganism;
        }

        internal int getTotalSpeed()
        {
            int totalSpeed = 0;
            foreach (Limb limb in Limbs)
            {
                totalSpeed += limb.BaseSpeed;
            }
            totalSpeed += this.BaseSpeed;
            return totalSpeed;
        }

        public void setObserver(Observer o)
        {
            this.Observer = o;
        }

        public void notifyObservers()
        {
            if (Observer != null)
                Observer.notify();
        }

        #region Unused Methods
        public void notifyObservers(EnvironmentTile tile, Plant plant)
        {
            throw new NotImplementedException();
        }

        public void notifyObservers(Animal animal)
        {
            if (Observer != null)
                Observer.notify(animal);
        }
        #endregion
    }
}
