using EvolutionModel.Model.Genotypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvolutionModel.ObserverPattern
{
    public interface DeathObservable
    {
        void notifyDeathObservers(Organism organism);
    }
}
