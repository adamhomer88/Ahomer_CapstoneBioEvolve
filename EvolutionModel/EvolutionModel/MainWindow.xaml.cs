using EvolutionModel.Model.Environment;
using EvolutionModel.Model.Genotypes;
using EvolutionModel.Model.PhenoTypes.Digestion;
using EvolutionModel.ObserverPattern;
using EvolutionModel.UserControls.Creature;
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
    public partial class MainWindow : Window, Observer
    {
        public BioEvolveEnvironment currentEnvironment { get; set; }
        public UserControl_EnvironmentTile SelectedTile { get; set; }
        public UserControl_Animal SelectedAnimal { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            setupEnvironmentGrid();
            setupSliderBindings();
            hideUnusedGroupBoxes();
            currentEnvironment.Simulate();
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
            currentEnvironment.AddObserver(this);
            this.Environment_Grid.Height = 32 * currentEnvironment.Y_Size;
            this.Environment_Grid.Width = 32 * currentEnvironment.X_Size;
            SetupTileDefinitions();
        }

        private void SetupTileDefinitions()
        {
            for (int i = 0; i < this.currentEnvironment.Y_Size; i++)
            {
                for (int j = 0; j < this.currentEnvironment.X_Size; j++)
                {
                    EnvironmentTile model = (from item in this.currentEnvironment.EnvironmentPlantLife.Keys
                                            where item.X == j && item.Y == i
                                            select item).SingleOrDefault();
                    UserControl_EnvironmentTile tile = new UserControl_EnvironmentTile(model, this);
                    AddTileToGrid(i, j, tile);
                }
            }
            //AddTestOrganism();
        }

        private void AddTileToGrid(int i, int j, UserControl_EnvironmentTile tile)
        {
            tile.Selection += SelectTile;
            Canvas.SetTop(tile,32*i);
            Canvas.SetLeft(tile,32*j);
            this.Environment_Grid.Children.Add(tile);
        } 

        private void AddTestOrganism()
        {
            OrganismFactory factory = new OrganismFactory(currentEnvironment);
            Organism organism = factory.randomOrganism();
            if (organism is Animal)
            {
                currentEnvironment.Animals.Add(organism as Animal);
                AddTestOrganismToGrid(new UserControl_Animal(organism as Animal));
            }
            else
            {
                AddTestOrganismToGrid(new UserControl_Plant(organism as Plant));
            }
        }

        private void AddTestOrganismToGrid(UIElement userControl)
        {
            if (userControl is UserControl_Animal)
                (userControl as UserControl_Animal).Selection += SelectAnimal;
            else
                (userControl as UserControl_Plant).Selection += SelectPlant;
            Canvas.SetTop(userControl, 200);
            Canvas.SetLeft(userControl, 200);
            this.Environment_Grid.Children.Add(userControl);
        }

        private void SelectAnimal(UserControl_Animal animal)
        {
            if(SelectedAnimal == null)
            {
                SelectNewAnimal(animal);
            }
            else if (SelectedAnimal != animal)
            {
                ChangeSelectedAnimal(animal);
            }
            else
            {
                DeselectAnimal(animal);
            }
        }

        private void DeselectAnimal(UserControl_Animal animal)
        {
            this.SelectedAnimal = null;
            this.Selected_Organism_Info_Box.Visibility = Visibility.Hidden;
        }

        private void ChangeSelectedAnimal(UserControl_Animal animal)
        {
            this.SelectedAnimal = animal;
            this.Selected_Organism_Info_Box.Visibility = Visibility.Visible;
        }

        private void SelectNewAnimal(UserControl_Animal animal)
        {
            this.SelectedAnimal = animal;
            this.Selected_Organism_Info_Box.Visibility = Visibility.Visible;
        }

        private void SelectPlant(UserControl_Plant plant)
        {
            throw new NotImplementedException();
        }

        private void SelectTile(UserControl_EnvironmentTile tile)
        {
            if (SelectedTile == null)
            {
                SelectTileNew(tile);
            }
            else if (SelectedTile != tile)
            {
                ChangeSelectedTile(tile);
            }
            else
            {
                DeselectTile();
            }
        }

        private void SelectTileNew(UserControl_EnvironmentTile tile)
        {
            this.SelectedTile = tile;
            tile.IsSelected = true;
            BindTileToSliders();
            this.Selected_Tile_Info_Box.Visibility = Visibility.Visible;
        }

        private void ChangeSelectedTile(UserControl_EnvironmentTile tile)
        {
            SelectedTile.IsSelected = false;
            this.SelectedTile = tile;
            tile.IsSelected = true;
            BindTileToSliders();
            this.Selected_Tile_Info_Box.Visibility = Visibility.Visible;
        }

        private void DeselectTile()
        {
            SelectedTile.IsSelected = false;
            SelectedTile = null;
            this.Selected_Tile_Info_Box.Visibility = Visibility.Hidden;
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

        private void Set_Force_Mutate_Organism(object sender, RoutedEventArgs e)
        {
            if (SelectedAnimal != null)
                SelectedAnimal.Model.IsForcedMutate = true;
        }

        public void notify(Animal animal)
        {
            DisplayAnimal(animal);
        }

        private void DisplayPlant(EnvironmentTile tile, Plant plant)
        {
            Dispatcher.BeginInvoke((Action)(() =>
            {
                UserControl_Plant PlantDisplay = new UserControl_Plant(plant);
                PlantDisplay.Selection += SelectPlant;
                Canvas.SetTop(PlantDisplay, tile.Location.Y*32);
                Canvas.SetTop(PlantDisplay, tile.Location.X*32);
                this.Environment_Grid.Children.Add(PlantDisplay);
            }));
            //todo
        }

        private void DisplayAnimal(Animal animal)
        {
            Dispatcher.BeginInvoke((Action)(() =>
            {
                UserControl_Animal AnimalDisplay = new UserControl_Animal(animal);
                AnimalDisplay.Selection += SelectAnimal;
                Canvas.SetTop(AnimalDisplay, animal.Location.Y);
                Canvas.SetTop(AnimalDisplay, animal.Location.X);
                this.Environment_Grid.Children.Add(AnimalDisplay);
            }));
        }
        
        public void notify(EnvironmentTile tile, Plant plant)
        {
            DisplayPlant(tile, plant);
        }

        #region Unused Methods
        public void notify()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
