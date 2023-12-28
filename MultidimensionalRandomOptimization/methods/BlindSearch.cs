using System;
using System.Collections.Generic;

namespace MultidimensionalRandomOptimization.methods
{
    internal class BlindSearch
    {
        private readonly ObjectiveFunctionDelegate objectiveFunction;

        public BlindSearch(ObjectiveFunctionDelegate objectiveFunction)
        {
            this.objectiveFunction = objectiveFunction;
        }

        public List<double> FindOptimalPoint(List<double> lowerBounds, List<double> upperBounds, int dimensions, int numPoints, bool findMinimum)
        {
            Random random = new Random();
            List<double> bestPoint = GenerateRandomPoint(lowerBounds, upperBounds, random);
            double bestValue = objectiveFunction(bestPoint);
            
            for (int i = 0; i < numPoints; i++)
            {
                List<double> randomPoint = GenerateRandomPoint(lowerBounds, upperBounds, random);
                double randomValue = objectiveFunction(randomPoint);

                if ((findMinimum && randomValue < bestValue) || (!findMinimum && randomValue > bestValue))
                {
                    bestPoint = new List<double>(randomPoint);
                    bestValue = randomValue;
                }
            }

            return bestPoint;
        }

        private List<double> GenerateRandomPoint(List<double> lowerBounds, List<double> upperBounds, Random random)
        {
            List<double> point = new List<double>();
            for (int i = 0; i < lowerBounds.Count; i++)
            {
                point.Add(lowerBounds[i] + random.NextDouble() * (upperBounds[i] - lowerBounds[i]));
            }
            return point;
        }
    }
}
