using EvolutionModel.Model.Environment;
using EvolutionModel.Model.Genotypes;
using EvolutionModel.Model.PhenoTypes.Digestion;
using EvolutionModel.UserControls.Environment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

namespace EvolutionModel
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public BioEvolveEnvironment currentEnvironment { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            setupEnvironmentGrid();
            setupSliderBindings();
            OrganismFactory factory = new OrganismFactory();

            Organism a = factory.randomOrganism();
            MessageBox.Show(a.GetType().ToString());
        }

        private void setupSliderBindings()
        {
            Binding abiogenesisBinding = new Binding("AbiogenesisRate");
            Binding humidityBinding = new Binding("Humidity");
            abiogenesisBinding.Source = currentEnvironment;
            humidityBinding.Source = currentEnvironment;
            this.AbiogenesisRate.SetBinding(Slider.ValueProperty, abiogenesisBinding);
            this.Humidity.SetBinding(Slider.ValueProperty, humidityBinding);
            this.Label_Abiogenesis.SetBinding(Label.ContentProperty, abiogenesisBinding);
            this.Label_Humidity.SetBinding(Label.ContentProperty, humidityBinding);

        }

        private void setupEnvironmentGrid()
        {
            currentEnvironment = EnvironmentGenerator.Jungle();
            foreach (EnvironmentTile tile in currentEnvironment.Tiles)
            {
                this.EnvironmentGrid.RowDefinitions.Add(new RowDefinition() { Height=GridLength.Auto });
                this.EnvironmentGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width=GridLength.Auto });
            }

            for(int i = 0; i < this.currentEnvironment.Tiles.GetLength(0); i++)
            {
                for (int j = 0; j < this.currentEnvironment.Tiles.GetLength(1); j++)
                {
                    UserControl_EnvironmentTile tile = new UserControl_EnvironmentTile(this.currentEnvironment.Tiles[i,j]);
                    Grid.SetColumn(tile, j);
                    Grid.SetRow(tile, i);
                    this.EnvironmentGrid.Children.Add(tile);
                }
            }
        }
    }
}
