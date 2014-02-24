using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvolutionModel.ObserverPattern
{
    public interface Observable
    {
        void notifyObservers();
    }
}
