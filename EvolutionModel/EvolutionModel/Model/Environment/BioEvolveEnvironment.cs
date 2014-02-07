using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace EvolutionModel.Model.Environment
{
    public class BioEvolveEnvironment : INotifyPropertyChanged
    {
        private int _abiogenesisRate;
        private int _humidity;
        private int DEFAULT_X = 50;
        private int DEFAULT_Y = 50;
        public EnvironmentTile[,] Tiles { get; set; }

        public int Humidity
        {
            get { return _humidity; }
            set
            {
                _humidity = value;
                OnPropertyChanged("Humidity");
            }
        }
        public int AbiogenesisRate
        {
            get { return _abiogenesisRate; }
            set 
            {
                _abiogenesisRate = value;
                OnPropertyChanged("AbiogenesisRate");
            }
        }

        public BioEvolveEnvironment(int x, int y)
        {
            Tiles = new EnvironmentTile[x, y];
        }
        public BioEvolveEnvironment()
        {
            Tiles = new EnvironmentTile[DEFAULT_X, DEFAULT_Y];
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
    }
}
