﻿using EvolutionModel.Model.Environment;
using EvolutionModel.Model.Genotypes;
using EvolutionModel.Model.PhenoTypes.Digestion;
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
    public partial class MainWindow : Window
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
            SetupGridDefinitions();
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
            AddTestOrganism();
        }

        private void AddTileToGrid(int i, int j, UserControl_EnvironmentTile tile)
        {
            Grid.SetColumn(tile, j);
            Grid.SetRow(tile, i);
            tile.Selection += SelectTile;
            this.Environment_Grid.Children.Add(tile);
        }

        private void AddTestOrganism()
        {
            OrganismFactory factory = new OrganismFactory();
            Organism organism = factory.randomOrganism();
            if (organism is Animal)
            {
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
            Grid.SetRow(userControl, 5);
            Grid.SetColumn(userControl, 5);
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

        private void SetupGridDefinitions()
        {
            foreach (EnvironmentTile tile in currentEnvironment.EnvironmentPlantLife.Keys)
            {
                this.Environment_Grid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
                this.Environment_Grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Auto });
            }
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
    }
}
