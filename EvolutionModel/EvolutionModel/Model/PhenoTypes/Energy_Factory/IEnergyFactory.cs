using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EvolutionModel.Model.PhenoTypes.Energy_Factory
{
    public interface IEnergyFactory : ISerializable
    {
        int createEnergy(out int waterCount, int mass);
    }
}
