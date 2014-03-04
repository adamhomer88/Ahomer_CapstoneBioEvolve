using EvolutionModel.Model.Environment;
using EvolutionModel.Model.Genotypes;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvolutionModel.Model.AnimalStates
{
    [Serializable]
    class PursuePreyState : IAnimalState
    {
        private Animal model;
        private Organism food;
        private int PREY_WITHIN_EATING_RANGE = 10;
        BioEvolveEnvironment environment;

        public PursuePreyState(Organism food, Animal model, BioEvolveEnvironment environment)
        {
            this.food = food;
            this.model = model;
            this.environment = environment;
        }

        public void ExecuteBehavior()
        {
            MoveTowardsFood();
            if (PreyIsCaught())
            {
                model.Digestion.Digest(food);
                model.State = new IdleState(model, environment);
                model.State.ExecuteBehavior();
            }
        }

        private bool PreyIsCaught()
        {
            int x = (int)(food.Location.X - model.Location.X);
            int y = (int)(food.Location.Y - model.Location.Y);
            System.Windows.Vector distanceFromFood = new System.Windows.Vector(Math.Abs(x), Math.Abs(y));
            distanceFromFood.X = x;
            distanceFromFood.Y = y;
            return distanceFromFood.Length < this.PREY_WITHIN_EATING_RANGE;
        }

        private void MoveTowardsFood()
        {
            int x = (int)(food.Location.X - model.Location.X);
            int y = (int)(food.Location.Y - model.Location.Y);
            int newX;
            int newY;
            if(x < 0)
                newX = foodIsDueWest(x);
            else if(x>0)
                newX = foodIsDueEast(x);
            else
                newX = 0;
            if (y < 0)
                newY = foodIsDueNorth(y);
            else if (y > 0)
                newY = foodIsDueSouth(y);
            else
                newY = 0;
            model.Location = new Point((int)(model.Location.X + newX), (int)(model.Location.Y + newY));
        }

        private int foodIsDueSouth(int y)
        {
            int newY = 0;
            if (Math.Abs(y) < model.getTotalSpeed())
                newY = y;
            else
                newY += model.getTotalSpeed();
            return newY;
        }

        private int foodIsDueNorth(int y)
        {
            int newY = 0;
            if (Math.Abs(y) < model.getTotalSpeed())
                newY = y;
            else
                newY -= model.getTotalSpeed();
            return newY;
        }

        private int foodIsDueEast(int x)
        {
            int newX = 0;
            if (Math.Abs(x) < model.getTotalSpeed())
                newX = x;
            else
                newX -= model.getTotalSpeed();
            return newX;
        }

        private int foodIsDueWest(int x)
        {
            int newX = 0;
            if (Math.Abs(x) < model.getTotalSpeed())
                newX = x;
            else
                newX -= model.getTotalSpeed();
            return newX;
        }
    }
}
