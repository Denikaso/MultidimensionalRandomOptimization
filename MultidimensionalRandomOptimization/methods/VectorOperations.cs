using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultidimensionalRandomOptimization.methods
{
    internal class VectorOperations
    {
        public static List<double> Add(List<double> a, List<double> b)
        {
            List<double> result = new List<double>();
            for (int i = 0; i < a.Count; i++)
            {
                result.Add(a[i] + b[i]);
            }
            return result;
        }

        public static List<double> Scale(List<double> vector, double scalar)
        {
            List<double> result = new List<double>();
            foreach (double component in vector)
            {
                result.Add(component * scalar);
            }
            return result;
        }
    }
}
