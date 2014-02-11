using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvolutionModel.Model.Environment
{
    public class EnvironmentTile : INotifyPropertyChanged
    {
        private int _waterLevel;
        private int _fertilityLevel;

        public int WaterLevel 
        {
            get { return _waterLevel; } 
            set 
            {
                _waterLevel = value;
                OnPropertyChanged("WaterLevel");
            } 
        }

        public int FertilityLevel 
        {
            get { return _fertilityLevel; } 
            set 
            {
                _fertilityLevel = value;
                OnPropertyChanged("FertilityLevel");
            } 
        }

        public List<DeadOrganism> Carcasses = new List<DeadOrganism>();

        public EnvironmentTile(int waterLevel, int fertilityLevel)
        {
            this.WaterLevel = waterLevel;
            this.FertilityLevel = fertilityLevel;
        }

        public void addWater(int water)
        {
            WaterLevel += water;
        }

        public void addFertility(int fertility)
        {
            FertilityLevel += fertility;
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
