﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EvolutionModel.Model.PhenoTypes.Digestion;
using EvolutionModel.Model.Environment;
using System.Runtime.Serialization;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows;
using System.ComponentModel;
using EvolutionModel.ObserverPattern;
namespace EvolutionModel.Model.Genotypes
{
    [Serializable]
    public abstract class Organism : INotifyPropertyChanged, Observable, DeathObservable
    {
        #region BasicPhenotypes
        private int _mass;
        private double _energyTotal;
        public int Mass 
        { 
            get{ return _mass;}
            set
            {
                this._mass = value;
                OnPropertyChanged("Mass");
            } 
        }
        public int ChildMass { get; set; }
        public int MaximumMass 
        {
            get { return ChildMass * 5; }
        }
        public double EnergyTotal 
        {
            get { return _energyTotal; }
            set
            {
                this._energyTotal = value;
                OnPropertyChanged("EnergyTotal");
            }
        }
        public int MaxEnergy 
        { 
            get
            {
                return Mass * MAX_ENERGY_MULTIPLIER;
            }
        }
        public int EnergyPerTurn 
        {
            get { return (int)(this.Mass * DEFAULT_ENERGY_PER_TURN / 4); }
        }
        #endregion 

        #region DefaultBasePhenotypes
        public const double DEFAULT_COMPLEX_MUTATION_CHANCE = 15;
        public const double DEFAULT_BASE_MUTATION_CHANCE = 50;
        public const int MAX_MASS_MULTIPLIER = 7;
        public const double DEFAULT_ENERGY_PER_TURN = 10;
        const int MAX_ENERGY_MULTIPLIER = 50;
        #endregion

        [field: NonSerialized]
        private Observer Observer;
        [field: NonSerialized]
        public DeathObserver DeathObserver;
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
        private const int HUNDRED_PERCENT = 100;
        private const int FORCED_BASIC_MUTATION_CHANCE = 75;
        public string Id { get; set; }
        private bool _isForcedMutate;
        public int Generation { get; set; }
        public DigestiveSystem Digestion { get; set; }
        public List<Parasite> Parasites { get; set; }
        public Boolean IsDead { get; set; }
        public abstract Organism doTurn();
        public abstract Organism basicMutate(Organism baseOrganism);
        public abstract Organism complexMutate(Organism baseOrganism);
        public abstract void Grow();

        public bool IsForcedMutate 
        {
            get { return _isForcedMutate; }
            set 
            {
                _isForcedMutate = value;
                OnPropertyChanged("IsForcedMutate");
            }
        }

        public Organism Reproduce()
        {
            Organism newOrganism = null;
            newOrganism = DeepCopy(this, newOrganism);
            newOrganism = resetChildOrganism(newOrganism);

            int randomNumber = OrganismFactory.random.Next(HUNDRED_PERCENT);
            if (IsForcedMutate)
            {
                newOrganism = ForceMutate(newOrganism, randomNumber);
            }
            else
            {
                DetermineMutation(ref newOrganism, ref randomNumber);
            }
            return newOrganism;
        }

        private Organism resetChildOrganism(Organism newOrganism)
        {
            newOrganism.Mass = newOrganism.ChildMass;
            newOrganism.EnergyTotal = newOrganism.MaxEnergy*Animal.DEFAULT_REPRODUCTION_THRESHOLD-1;
            return newOrganism;
        }

        private void DetermineMutation(ref Organism newOrganism, ref int randomNumber)
        {
            if (isSimpleMutated(randomNumber))
                newOrganism = basicMutate(newOrganism);
            randomNumber = OrganismFactory.random.Next(HUNDRED_PERCENT);
            if (isComplexMutated(randomNumber)&&newOrganism is Plant)
                newOrganism = complexMutate(newOrganism);
        }

        private Organism ForceMutate(Organism newOrganism, int randomNumber)
        {
            newOrganism = complexMutate(newOrganism);
            IsForcedMutate = false;
            return newOrganism;
        }

        private bool isForcedSimpleMutate(int randomNumber)
        {
            return randomNumber < FORCED_BASIC_MUTATION_CHANCE;
        }

        private bool isComplexMutated(int randomNumber)
        {
            return randomNumber < DEFAULT_COMPLEX_MUTATION_CHANCE;
        }

        private bool isSimpleMutated(int number)
        {
            return number<DEFAULT_BASE_MUTATION_CHANCE;
        }

        private static Organism DeepCopy(Organism organism, Organism newOrganism)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(ms, organism);
                ms.Position = 0;
                newOrganism = (Organism)formatter.Deserialize(ms);
            }
            return newOrganism;
        }

        public void Die()
        {
            this.IsDead = true;
            notifyDeathObservers(this);
        }

        public Organism BurnEnergy()
        {
            Organism deadOrganism = null;
            this.EnergyTotal -= this.EnergyPerTurn;
            if (EnergyTotal < 0)
                deadOrganism = this;
            if (Mass >= MaximumMass || Mass>10)
                deadOrganism = this;
            return deadOrganism;
        }

        [field:NonSerialized()]
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string info)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(info));
            }
        }
        
        public void notifyObservers()
        {
            if (Observer != null)
                Observer.notify();
        }


        public void notifyObservers(Animal animal)
        {
            if (Observer != null)
                Observer.notify(animal);
        }

        public void notifyObservers(EnvironmentTile tile, Plant plant)
        {
            if (Observer != null)
                Observer.notify(tile, plant);
        }

        public void setObserver(Observer o)
        {
            this.Observer = o;
        }

        public void notifyDeathObservers(Organism organism)
        {
            if(DeathObserver != null)
                this.DeathObserver.notifyOfDeath(organism);
        }

        public void SetDeathObserver(DeathObserver observer)
        {
            this.DeathObserver = observer;
        }
    }
}
