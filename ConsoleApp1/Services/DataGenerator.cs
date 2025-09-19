using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoBenchmark.Data
{
    public static class DataGenerator
    {
        private static readonly Random Random = new Random();
        

        public static int[] GenerateVector(int size)
        {
            return new int[2];
        }

        public static int[,] GenerateMatrix(int size)
        {
            return new int[2,2];
        }

        public static int[] GenerateCoefficients(int degree)
        {
            return new int[2];
        }
    }
}
