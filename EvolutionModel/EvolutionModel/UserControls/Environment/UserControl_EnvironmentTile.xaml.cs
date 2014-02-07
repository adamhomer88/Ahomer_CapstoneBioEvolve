using EvolutionModel.Model.Environment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EvolutionModel.UserControls.Environment
{
    /// <summary>
    /// Interaction logic for UserControl_EnvironmentTile.xaml
    /// </summary>
    public partial class UserControl_EnvironmentTile : UserControl
    {
        public EnvironmentTile model { get; set; }
        public UserControl_EnvironmentTile(EnvironmentTile tile)
        {
            InitializeComponent();
            model = tile;
        }

        private void Tile_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.BorderBrush = new SolidColorBrush(Color.FromArgb(255, 0, 0, 0));
            this.BorderThickness = new Thickness(2.0);
            MessageBox.Show(model.fertilityLevel + " " + model.waterLevel);
            this.BorderThickness = new Thickness(0);
        }
    }
}
