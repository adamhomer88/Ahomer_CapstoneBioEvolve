using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvolutionModel.Model.Genotypes
{
    public interface IParasite
    {
        int Absorb(Animal animal);
        int Absorb(Plant plant);
    }
}
