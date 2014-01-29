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
namespace EvolutionModel.Model.Genotypes
{
    public abstract class Organism : ISerializable
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
        public int Generation { get; set; }
        public DigestiveSystem Digestion { get; set; }
        public List<Parasite> Parasites { get; set; }

        public abstract void doTurn(EnvironmentTile localEnvironment);
        public abstract Organism mutate(Organism baseOrganism);
        public abstract Organism complexMutate(Organism baseOrganism);

        public Organism Reproduce(Organism organism)
        {
            Organism newOrganism = null;
            newOrganism = DeepCopy(organism, newOrganism);
            int randomNumber = OrganismFactory.random.Next(HUNDRED_PERCENT);
            if (isSimpleMutated(randomNumber))
                newOrganism = mutate(newOrganism);
            randomNumber = OrganismFactory.random.Next(HUNDRED_PERCENT);
            if (isComplexMutated(randomNumber))
                newOrganism = complexMutate(newOrganism);
            return newOrganism;
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

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            throw new NotImplementedException();
        }
    }
}
