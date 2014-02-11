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
        public UserControl_EnvironmentTile SelectedTile { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            setupEnvironmentGrid();
            setupSliderBindings();
            hideUnusedGroupBoxes();
            OrganismFactory factory = new OrganismFactory();

        }

        private void hideUnusedGroupBoxes()
        {
            this.Selected_Organism_Info_Box.Visibility = Visibility.Hidden;
            this.Selected_Tile_Info_Box.Visibility = Visibility.Hidden;
        }

        private void setupSliderBindings()
        {
            SetupAbiogenesisSliderBinding();
            SetupHumiditySliderBinding();

        }

        private void SetupAbiogenesisSliderBinding()
        {
            Binding abiogenesisBinding = new Binding("AbiogenesisRate");
            abiogenesisBinding.Source = currentEnvironment;
            this.AbiogenesisRate.SetBinding(Slider.ValueProperty, abiogenesisBinding);
            this.Label_Abiogenesis.SetBinding(Label.ContentProperty, abiogenesisBinding);
        }

        private void SetupHumiditySliderBinding()
        {
            Binding humidityBinding = new Binding("Humidity");
            humidityBinding.Source = currentEnvironment;
            this.Humidity.SetBinding(Slider.ValueProperty, humidityBinding);
            this.Label_Humidity.SetBinding(Label.ContentProperty, humidityBinding);
        }

        private void setupEnvironmentGrid()
        {
            currentEnvironment = EnvironmentGenerator.Jungle();
            SetupGridDefinitions();
            SetupTileDefinitions();
        }

        private void SetupTileDefinitions()
        {
            for (int i = 0; i < this.currentEnvironment.Tiles.GetLength(0); i++)
            {
                for (int j = 0; j < this.currentEnvironment.Tiles.GetLength(1); j++)
                {
                    UserControl_EnvironmentTile tile = new UserControl_EnvironmentTile(this.currentEnvironment.Tiles[i, j], this);
                    Grid.SetColumn(tile, j);
                    Grid.SetRow(tile, i);
                    tile.Selection += SelectTile;
                    this.EnvironmentGrid.Children.Add(tile);
                }
            }
        }

        private void SetupGridDefinitions()
        {
            foreach (EnvironmentTile tile in currentEnvironment.Tiles)
            {
                this.EnvironmentGrid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
                this.EnvironmentGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Auto });
            }
        }

        private void SelectTile(UserControl_EnvironmentTile tile)
        {
            if (SelectedTile == null)
            {
                this.SelectedTile = tile;
                tile.IsSelected = true;
                BindTileToSliders();
                this.Selected_Tile_Info_Box.Visibility = Visibility.Visible;
            }
            else if (SelectedTile != tile)
            {
                SelectedTile.IsSelected = false;
                this.SelectedTile = tile;
                tile.IsSelected = true;
                BindTileToSliders();
                this.Selected_Tile_Info_Box.Visibility = Visibility.Visible;
            }
            else
            {
                SelectedTile.IsSelected = false;
                SelectedTile = null;
                this.Selected_Tile_Info_Box.Visibility = Visibility.Hidden;
            }
        }

        private void BindTileToSliders()
        {
            SetupWaterLevelBinding();
            SetupFertilityLevelBinding();
        }

        private void SetupWaterLevelBinding()
        {
            Binding WaterBinding = new Binding("WaterLevel");
            WaterBinding.Source = this.SelectedTile.Model;
            this.Water_Level.SetBinding(Slider.ValueProperty, WaterBinding);
            this.Label_Water_Level.SetBinding(Label.ContentProperty, WaterBinding);
        }

        private void SetupFertilityLevelBinding()
        {
            Binding FertilityBinding = new Binding("FertilityLevel");
            FertilityBinding.Source = this.SelectedTile.Model;
            this.Fertility_Level.SetBinding(Slider.ValueProperty, FertilityBinding);
            this.Label_Fertility_Level.SetBinding(Label.ContentProperty, FertilityBinding);
        }
    }
}
