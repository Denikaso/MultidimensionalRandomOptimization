using System;
using System.Collections.Generic;
using MultidimensionalRandomOptimization.methods;

public delegate double ObjectiveFunctionDelegate(List<double> point);

public class Program
{
    static double Function1(List<double> point)
    {
        double x1 = point[0];
        double x2 = point[1];

        return 9 * x1 * x1 + 9 * x1 - 6 * x1 * x2 + 4 * x2 * x2 - 6 * x2 - 4;
    }
    static double Function2(List<double> point)
    {
        double x1 = point[0];
        double x2 = point[1];
        double x3 = point[2];

        return 8 * Math.Pow(x1 - 1, 2) + Math.Pow(x2 - 4, 2) + Math.Pow(x3 + 7, 2);
    }
    static double Function3(List<double> point)
    {
        double x1 = point[0];
        double x2 = point[1];

        return 100 * (x2 - x1 * x1) + Math.Pow(1 - x1, 2);
    }

    public static void Main()
    {
        Console.WriteLine("Выберите функцию оптимизации:");
        int selectedFunction = int.Parse(Console.ReadLine());
        int dimensions;

        ObjectiveFunctionDelegate objectiveFunction = null;
        switch (selectedFunction)
        {
            case 1:
                objectiveFunction = Function1;
                dimensions = 2;
                break;
            case 2:
                objectiveFunction = Function2;
                dimensions = 3;
                break;
            case 3:
                objectiveFunction = Function3;
                dimensions = 2;
                break;
            default:
                throw new ArgumentException("Неверный выбор функции");
        }                

        // Выбор метода оптимизации
        Console.WriteLine("Выберите метод оптимизации:");
        Console.WriteLine("1. Метод слепого поиска");
        Console.WriteLine("2. Метод случайных направлений");

        int selectedMethod = int.Parse(Console.ReadLine());

        switch (selectedMethod)
        {
            case 1:
                BlindSearch blindSearch = new BlindSearch(objectiveFunction);

                // Задаем границы для слепого поиска
                List<double> lowerBoundsBlindSearch = new List<double>();
                List<double> upperBoundsBlindSearch = new List<double>();

                Console.WriteLine("Введите границы:");
                for (int i = 0; i < dimensions; i++)
                {
                    Console.Write($"Нижняя граница для компоненты {i + 1}: ");
                    lowerBoundsBlindSearch.Add(double.Parse(Console.ReadLine()));

                    Console.Write($"Верхняя граница для компоненты {i + 1}: ");
                    upperBoundsBlindSearch.Add(double.Parse(Console.ReadLine()));
                }

                // Ищем оптимальную точку для минимума
                List<double> optimalPointBlindSearchMin = blindSearch.FindOptimalPoint(lowerBoundsBlindSearch, upperBoundsBlindSearch, dimensions, numPoints: 1_000_000, findMinimum: true);
                Console.WriteLine("Optimal Minimum Point (Blind Search): " + string.Join(", ", optimalPointBlindSearchMin));
                Console.WriteLine("Optimal Minimum Value (Blind Search): " + objectiveFunction(optimalPointBlindSearchMin));

                // Ищем оптимальную точку для максимума
                List<double> optimalPointBlindSearchMax = blindSearch.FindOptimalPoint(lowerBoundsBlindSearch, upperBoundsBlindSearch, dimensions, numPoints: 1_000_000, findMinimum: false);
                Console.WriteLine("Optimal Maximum Point (Blind Search): " + string.Join(", ", optimalPointBlindSearchMax));
                Console.WriteLine("Optimal Maximum Value (Blind Search): " + objectiveFunction(optimalPointBlindSearchMax));
                break;
            case 2:
                RandomDirection randomDirectionSearch = new RandomDirection(objectiveFunction);
                Console.Write("Введите начальную точку:");
                List<double> initialPoint = new List<double>();
                for (int i = 0; i < dimensions; i++)
                {
                    Console.Write($"Компонента {i + 1}: ");
                    initialPoint.Add(double.Parse(Console.ReadLine()));
                }
                Console.Write("Введите размер шага: ");
                double stepSize = double.Parse(Console.ReadLine());
                Console.Write("Введите количество шагов: ");
                int numSteps = int.Parse(Console.ReadLine());
                
                List<double> optimalPointRandomDirectionSearchMin = randomDirectionSearch.FindOptimalPoint(initialPoint, dimensions, numSteps, stepSize, maxFailures: 5, findMinimum: true);
                Console.WriteLine("Optimal Minimum Point (Random Direction Search): " + string.Join(", ", optimalPointRandomDirectionSearchMin));
                Console.WriteLine("Optimal Minimum Value (Random Direction Search): " + objectiveFunction(optimalPointRandomDirectionSearchMin));
                
                List<double> optimalPointRandomDirectionSearchMax = randomDirectionSearch.FindOptimalPoint(initialPoint, dimensions, numSteps, stepSize, maxFailures: 5, findMinimum: false);
                Console.WriteLine("Optimal Maximum Point (Random Direction Search): " + string.Join(", ", optimalPointRandomDirectionSearchMax));
                Console.WriteLine("Optimal Maximum Value (Random Direction Search): " + objectiveFunction(optimalPointRandomDirectionSearchMax));
                break;
            default:
                throw new ArgumentException("Неверный выбор метода оптимизации");
        }
    }
}
