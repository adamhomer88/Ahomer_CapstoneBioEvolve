using EvolutionModel.Model.Environment;
using EvolutionModel.Model.Genotypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvolutionModel.ObserverPattern
{
    public interface Observer
    {
        void notify(Animal a);
        void notify();
        void notify(EnvironmentTile tile, Plant plant);
    }
}
