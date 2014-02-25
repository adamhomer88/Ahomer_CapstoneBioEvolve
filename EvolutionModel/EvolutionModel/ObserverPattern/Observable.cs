using EvolutionModel.Model.Environment;
using EvolutionModel.Model.Genotypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvolutionModel.ObserverPattern
{
    public interface Observable
    {
        void notifyObservers(Animal animal);
        void notifyObservers();
        void notifyObservers(EnvironmentTile tile, Plant plant);
    }
}
