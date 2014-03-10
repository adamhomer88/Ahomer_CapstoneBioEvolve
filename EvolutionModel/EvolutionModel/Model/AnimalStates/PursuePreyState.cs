using EvolutionModel.Model.Environment;
using EvolutionModel.Model.Genotypes;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EvolutionModel.Model.PhenoTypes.Limbs;

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

        public Organism ExecuteBehavior()
        {
            MoveTowardsFood();
            if (food.IsDead)
                model.State = new IdleState(model, environment);
            Organism deadFood = null;
            if (PreyIsCaught())
            {
                int energyGained = model.Digestion.Digest(food);
                finishDigestion(energyGained);
                deadFood = food;
                model.State = new IdleState(model, environment);
                model.State.ExecuteBehavior();
            }
            return deadFood;
        }

        private void finishDigestion(int energyGained)
        {
            if (this.model.EnergyTotal + energyGained < this.model.MaxEnergy)
                this.model.EnergyTotal += energyGained;
            else
                this.model.EnergyTotal = this.model.MaxEnergy;
        }

        private bool PreyIsCaught()
        {
            bool isCaught = false;
            int x = (int)(food.Location.X - model.Location.X);
            int y = (int)(food.Location.Y - model.Location.Y);
            System.Windows.Vector distanceFromFood = new System.Windows.Vector(Math.Abs(x), Math.Abs(y));
            distanceFromFood.X = x;
            distanceFromFood.Y = y;
            if (food is Animal)
                isCaught = (isAnimalCaught() && FoodIsInRange(distanceFromFood));
            else
                isCaught = FoodIsInRange(distanceFromFood);
            return isCaught;
        }

        private bool isAnimalCaught()
        {
            bool isCaught = true;
            Animal AnimalFood = food as Animal;
            double AnimalDefense = 0;
            double PredatorOffense = 0;
            AnimalDefense = CalculateAnimalDefense(AnimalFood, AnimalDefense);
            if(AnimalDefense !=0)
            {
                PredatorOffense = CalculatePredatorOffense(PredatorOffense);
                isCaught = PredatorOffense < AnimalDefense;
            }
            return isCaught;
        }

        private static double CalculateAnimalDefense(Animal AnimalFood, double AnimalDefense)
        {
            foreach (Limb limb in AnimalFood.Limbs)
            {
                if (limb is PredatoryLimb)
                    AnimalDefense += 0.5;
                else
                    AnimalDefense += 1;
            }
            return AnimalDefense;
        }

        private double CalculatePredatorOffense(double PredatorOffense)
        {
            foreach (Limb limb in model.Limbs)
            {
                if (limb is PredatoryLimb)
                    PredatorOffense += 1;
            }
            return PredatorOffense;
        }

        private bool FoodIsInRange(Vector distanceFromFood)
        {
            return (distanceFromFood.Length < this.PREY_WITHIN_EATING_RANGE);
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
                newX += model.getTotalSpeed();
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

        public override string ToString()
        {
            return "Pursuing prey to eat.";
        }
    }
}
