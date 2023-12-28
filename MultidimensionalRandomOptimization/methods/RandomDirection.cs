using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultidimensionalRandomOptimization.methods
{
    internal class RandomDirection
    {
        private readonly ObjectiveFunctionDelegate objectiveFunction;

        public RandomDirection(ObjectiveFunctionDelegate objectiveFunction)
        {
            this.objectiveFunction = objectiveFunction;
        }

        public List<double> FindOptimalPoint(List<double> initialPoint, int dimensions, int maxSteps, double stepSize, int maxFailures, bool findMinimum)
        {
            Random random = new Random();
            List<double> currentPoint = new List<double>(initialPoint);
            List<double> bestPoint = new List<double>(initialPoint);
            double currentBestValue = objectiveFunction(initialPoint);
            int failures = 0;

            Console.WriteLine($"Шаг 0: Текущая точка: {string.Join(", ", initialPoint)}");

            for (int step = 1; step <= maxSteps; step++)
            {
                List<double> randomDirection = GenerateRandomDirection(dimensions, random);
                List<double> newPoint = VectorOperations.Add(currentPoint, VectorOperations.Scale(randomDirection, stepSize));
                double newValue = objectiveFunction(newPoint);

                Console.WriteLine($"Шаг {step}: Текущая точка: {string.Join(", ", newPoint)}\tФункция: {newValue}");

                if ((findMinimum && newValue < currentBestValue) || (!findMinimum && newValue > currentBestValue))
                {
                    currentPoint = new List<double>(newPoint);
                    currentBestValue = newValue;
                    bestPoint = new List<double>(currentPoint);
                    failures = 0;
                }
                else
                {
                    failures++;
                    if (failures >= maxFailures)
                    {
                        // Если достигнуто максимальное количество отказов, сменяем направление
                        randomDirection = GenerateRandomDirection(dimensions, random);
                        newPoint = VectorOperations.Add(currentPoint, VectorOperations.Scale(randomDirection, stepSize));
                        failures = 0;
                    }
                }
            }

            return bestPoint;
        }

        private List<double> GenerateRandomDirection(int dimensions, Random random)
        {
            List<double> direction = new List<double>();
            double lengthSquared = 0.0;

            for (int i = 0; i < dimensions; i++)
            {
                double component = random.NextDouble() * 2 - 1;
                direction.Add(component);
                lengthSquared += component * component;
            }

            double length = Math.Sqrt(lengthSquared);
            double scale = 1.0 / length;

            for (int i = 0; i < dimensions; i++)
            {
                direction[i] *= scale;
            }

            return direction;
        }
    }
}
