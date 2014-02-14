using EvolutionModel.Model.Genotypes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvolutionModel.Model.Environment
{
    public class EnvironmentTile : INotifyPropertyChanged
    {
        private int _waterLevel;
        private int _fertilityLevel;
        public const int TILE_SIZE_IN_PIXELS = 32;
        public int X { get; set; }
        public int Y { get; set; }
        public List<DeadOrganism> Carcasses = new List<DeadOrganism>();
        public Plant Plantlife { get; set; }
        public Point Location { get; set; }

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

        public EnvironmentTile(int waterLevel, int fertilityLevel, int x, int y)
        {
            this.WaterLevel = waterLevel;
            this.FertilityLevel = fertilityLevel;
            this.X = x;
            this.Y = y;
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
