using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EvolutionModel.Model.PhenoTypes.Digestion;
using EvolutionModel.Model.Environment;
using System.Runtime.Serialization;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Drawing;
using System.ComponentModel;
namespace EvolutionModel.Model.Genotypes
{
    public abstract class Organism : ISerializable, INotifyPropertyChanged
    {
        #region BasicPhenotypes
        public int Mass { get; set; }
        public int ChildMass { get; set; }
        public int MaximumMass { get; set; }
        public int EnergyTotal { get; set; }
        public int MaxEnergy { get; set; }
        public int EnergyPerTurn { get; set; }
        #endregion 

        #region DefaultBasePhenotypes
        public const double DEFAULT_COMPLEX_MUTATION_CHANCE = .05;
        public const double DEFAULT_BASE_MUTATION_CHANCE = .25;
        #endregion

        private const int HUNDRED_PERCENT = 100;
        private const int FORCED_BASIC_MUTATION_CHANCE = 75;
        private bool _isForcedMutate;
        public int Generation { get; set; }
        public DigestiveSystem Digestion { get; set; }
        public List<Parasite> Parasites { get; set; }
        public Point Location { get; set; }
        public abstract void doTurn(EnvironmentTile localEnvironment);
        public abstract Organism basicMutate(Organism baseOrganism);
        public abstract Organism complexMutate(Organism baseOrganism);

        public bool IsForcedMutate 
        {
            get { return _isForcedMutate; }
            set 
            {
                _isForcedMutate = value;
                OnPropertyChanged("IsForcedMutate");
            }
        }

        public Organism Reproduce(Organism organism)
        {
            Organism newOrganism = null;
            newOrganism = DeepCopy(organism, newOrganism);
            int randomNumber = OrganismFactory.random.Next(HUNDRED_PERCENT);
            if (IsForcedMutate)
            {
                if (isForcedSimpleMutate(randomNumber))
                    newOrganism = basicMutate(newOrganism);
                else
                    newOrganism = complexMutate(newOrganism);
                IsForcedMutate = false;
            }
            else
            {
                if (isSimpleMutated(randomNumber))
                    newOrganism = basicMutate(newOrganism);
                randomNumber = OrganismFactory.random.Next(HUNDRED_PERCENT);
                if (isComplexMutated(randomNumber))
                    newOrganism = complexMutate(newOrganism);
            }
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

        public DeadOrganism Die()
        {
            DeadOrganism carcass = null;
            if (this.EnergyTotal <= 0)
                carcass = new DeadOrganism(this.Mass);
            else
                carcass = new DeadOrganism(this.Mass, this.EnergyTotal);
            return carcass;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string info)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(info));
            }
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            throw new NotImplementedException();
        }
    }
}
