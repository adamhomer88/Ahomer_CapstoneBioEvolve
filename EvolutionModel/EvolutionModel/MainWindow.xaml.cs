using EvolutionModel.Model.Environment;
using EvolutionModel.Model.Genotypes;
using EvolutionModel.Model.PhenoTypes.Digestion;
using EvolutionModel.Model.PhenoTypes.Limbs;
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

    public partial class MainWindow : Window, Observer, DeathObserver
    {
        public BioEvolveEnvironment currentEnvironment { get; set; }
        public UserControl_EnvironmentTile SelectedTile { get; set; }
        public UserControl_Organism SelectedOrganism { get; set; }
        private List<UserControl_Organism> OrganismDisplays = new List<UserControl_Organism>();

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
            SetupSpeedSliderBinding();
        }

        private void SetupSpeedSliderBinding()
        {
            Binding speedBinding = new Binding("Interval");
            speedBinding.Source = currentEnvironment;
            this.Speed.SetBinding(Slider.ValueProperty, speedBinding);
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
            currentEnvironment.AddDeathObserver(this);
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
        }

        private void AddTileToGrid(int i, int j, UserControl_EnvironmentTile tile)
        {
            tile.Selection += SelectTile;
            Canvas.SetTop(tile, 32 * i);
            Canvas.SetLeft(tile, 32 * j);
            this.Environment_Grid.Children.Add(tile);
        }

        private void SelectOrganism(UserControl_Organism organism)
        {
            if (SelectedOrganism == null)
            {
                SelectNewOrganism(organism);
            }
            else if (SelectedOrganism != organism)
            {
                ChangeSelectedOrganism(organism);
            }
            else
            {
                DeselectOrganism();
            }
        }

        private void DeselectOrganism()
        {
            this.SelectedOrganism.VisualRepresentation.Stroke = null;
            this.SelectedOrganism = null;
            this.Selected_Organism_Info_Box.Visibility = Visibility.Hidden;
        }

        private void ChangeSelectedOrganism(UserControl_Organism organism)
        {
            this.SelectedOrganism.VisualRepresentation.Stroke = null;
            this.SelectedOrganism = organism;
            this.SelectedOrganism.VisualRepresentation.Stroke = new SolidColorBrush(Colors.Blue);
            bindOrganismToLabels(organism);
            displayOrganismPhenotypes(organism);
            this.Selected_Organism_Info_Box.Visibility = Visibility.Visible;
        }

        private void displayOrganismPhenotypes(UserControl_Organism organism)
        {
            this.PhenoTypes_StackPanel.Children.Clear();
            if (organism.Model is Animal)
            {
                Animal animal = organism.Model as Animal;
                AddAnimalStateLabel(animal);
                AddDigestiveLabel(animal);
                AddSensoryOrgan(animal);
                AddLimbsLabel(animal);
            }
            else
            {
                Plant plant = organism.Model as Plant;
                AddPlantEnergyFactoryLabel(plant);
                AddPlantAbsorbtionLabel(plant);
            }
        }

        private void AddSensoryOrgan(Animal animal)
        {
            Label sensoryOrganLabel = new Label();
            sensoryOrganLabel.Content = "Senses:" + animal.Sensory.ToString();
            this.PhenoTypes_StackPanel.Children.Add(sensoryOrganLabel);
        }

        private void AddPlantAbsorbtionLabel(Plant plant)
        {
            Label absorbtionLabel = new Label();
            absorbtionLabel.Content = "Nutrient Absorbtion";
            this.PhenoTypes_StackPanel.Children.Add(absorbtionLabel);
            absorbtionLabel = new Label();
            absorbtionLabel.Content = plant.NutrientAbsorbtion.ToString();
            this.PhenoTypes_StackPanel.Children.Add(absorbtionLabel);
        }

        private void AddPlantEnergyFactoryLabel(Plant plant)
        {
            Label energyLabel = new Label();
            energyLabel.Content = "Energy Creation";
            this.PhenoTypes_StackPanel.Children.Add(energyLabel);
            energyLabel = new Label();
            energyLabel.Content = plant.EnergyFactory.ToString();
            this.PhenoTypes_StackPanel.Children.Add(energyLabel);
        }

        private void AddLimbsLabel(Animal animal)
        {
            Label animalLimbsLabel = new Label();
            if (animal.Limbs.Count == 0)
            {
                animalLimbsLabel.Content = "0 limbs.";
                this.PhenoTypes_StackPanel.Children.Add(animalLimbsLabel);
            }
            else
            {
                foreach (Limb L in animal.Limbs)
                {
                    animalLimbsLabel = new Label();
                    animalLimbsLabel.Content = L.ToString();
                    this.PhenoTypes_StackPanel.Children.Add(animalLimbsLabel);
                }
            }
        }

        private void AddAnimalStateLabel(Animal animal)
        {
            Label animalState = new Label();
            animalState.Content = animal.State.ToString();
            this.PhenoTypes_StackPanel.Children.Add(animalState);
        }

        private void AddDeathObserver(Animal animal)
        {
            Label deathobserver = new Label();
            deathobserver.Content = animal.DeathObserver.ToString();
            this.PhenoTypes_StackPanel.Children.Add(deathobserver);
        }

        private void AddDigestiveLabel(Animal animal)
        {
            Label digestiveSystem = new Label();
            digestiveSystem.Content = "Digestive System:" + animal.Digestion.ToString();
            this.PhenoTypes_StackPanel.Children.Add(digestiveSystem);
        }

        private void SelectNewOrganism(UserControl_Organism organism)
        {
            this.SelectedOrganism = organism;
            this.SelectedOrganism.VisualRepresentation.Stroke = new SolidColorBrush(Colors.Blue);
            bindOrganismToLabels(organism);
            displayOrganismPhenotypes(organism);
            this.Selected_Organism_Info_Box.Visibility = Visibility.Visible;
        }

        private void bindOrganismToLabels(UserControl_Organism organism)
        {
            Binding organismMassBinding = new Binding("Mass");
            organismMassBinding.Source = organism.Model;
            this.Mass_Label.SetBinding(Label.ContentProperty, organismMassBinding);
            Binding organismEnergyBinding = new Binding("EnergyTotal");
            organismEnergyBinding.Source = organism.Model;
            this.Energy_Label.SetBinding(Label.ContentProperty, organismEnergyBinding);
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
            if (SelectedOrganism != null)
                SelectedOrganism.Model.IsForcedMutate = true;
        }

        public void notify(Animal animal)
        {
            DisplayAnimal(animal);
        }

        private void DisplayPlant(EnvironmentTile tile, Plant plant)
        {
            Dispatcher.BeginInvoke((Action)(() =>
            {
                Control_Plant PlantDisplay = new Control_Plant(plant);
                PlantDisplay.Selection += SelectOrganism;
                Canvas.SetTop(PlantDisplay, tile.Y * 32);
                Canvas.SetLeft(PlantDisplay, tile.X * 32);
                Canvas.SetZIndex(PlantDisplay, 1);
                this.OrganismDisplays.Add(PlantDisplay);
                this.Environment_Grid.Children.Add(PlantDisplay);
            }));
        }

        private void DisplayAnimal(Animal animal)
        {
            Dispatcher.BeginInvoke((Action)(() =>
            {
                Control_Animal AnimalDisplay = new Control_Animal(animal);
                AnimalDisplay.Selection += SelectOrganism;
                Canvas.SetTop(AnimalDisplay, animal.Location.Y);
                Canvas.SetLeft(AnimalDisplay, animal.Location.X);
                Canvas.SetZIndex(AnimalDisplay, 2);
                this.OrganismDisplays.Add(AnimalDisplay);
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

        public void notifyOfDeath(Organism organism)
        {
            Dispatcher.BeginInvoke((Action)(() =>
            {
                foreach (UserControl_Organism OrganismDisplay in this.OrganismDisplays)
                {
                    bool found = RemoveDeadOrganism(organism, OrganismDisplay);
                    if (found)
                        break;
                }
            }));
        }

        private bool RemoveDeadOrganism(Organism deadAnimal, UserControl_Organism organismDisplay)
        {
            bool found = false;
            UserControl_Organism OrganismDisplay = organismDisplay as UserControl_Organism;
            if (OrganismDisplay.Model.Equals(deadAnimal))
            {
                if (organismDisplay.Equals(this.SelectedOrganism))
                    DeselectOrganism();
                Environment_Grid.Children.Remove(organismDisplay);
                this.OrganismDisplays.Remove(organismDisplay);
                found = true;
            }
            return found;
        }

        private void DisplayDeadOrganism(Organism deadOrganism)
        {
            if (deadOrganism is Animal)
            {
                DeadAnimal deadAnimal = new DeadAnimal(deadOrganism as Animal);
                Control_DeadAnimal DeadAnimalDisplay = new Control_DeadAnimal(deadAnimal);
                Canvas.SetLeft(DeadAnimalDisplay, deadOrganism.Location.X);
                Canvas.SetTop(DeadAnimalDisplay, deadOrganism.Location.Y);
                Canvas.SetZIndex(DeadAnimalDisplay, 0);
                this.Environment_Grid.Children.Add(DeadAnimalDisplay);
            }
        }

        private void StopSimulation(object sender, RoutedEventArgs e)
        {
            Button pauseButton = sender as Button;
            if (currentEnvironment.IsPaused)
            {
                currentEnvironment.UnPause();
                pauseButton.Content = "Pause";
            }
            else
            {
                currentEnvironment.Pause();
                pauseButton.Content = "Play";
            }
        }

        private void RestartSimulation(object sender, RoutedEventArgs e)
        {
            currentEnvironment.Pause();
            while (!currentEnvironment.ReadyForClear){}
                this.Environment_Grid.Children.Clear();
                currentEnvironment.Animals.Clear();
                setupEnvironmentGrid();
                setupSliderBindings();
                hideUnusedGroupBoxes();
                currentEnvironment.Simulate();
        }
    }
}

