using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laba1
{
    internal class Algorithms
    {
        public static long[] PermanentFunction(int[] vector)
        {
            var timeVector = new long[vector.Length];

            for (int i = 0; i < vector.Length; i++)
            {
                Stopwatch stopwatch = Stopwatch.StartNew();

                double sum = 0;
                for (int j = 0; j <= i; j++)
                {
                    sum += Math.Pow(vector[j], 2);
                }

                stopwatch.Stop();
                timeVector[i] = stopwatch.ElapsedTicks;
            }

            return timeVector;
        }

        public static long[] ProductOfElements(int[] vector)
        {
            var timeVector = new long[vector.Length];

            for (int i = 0; i < vector.Length; i++)
            {
                Stopwatch stopwatch = Stopwatch.StartNew();

                long product = 1;
                for (int j = 0; j <= i; j++)
                {
                    product *= vector[j];
                }

                stopwatch.Stop();
                timeVector[i] = stopwatch.ElapsedTicks;
            }

            return timeVector;
        }

        public static long[] BubbleSort(int[] vector)
        {
            var timeVector = new long[vector.Length];

            for (int count = 1; count <= vector.Length; count++)
            {
                int[] newVector = new int[count];
                Array.Copy(vector, newVector, count);

                int temporary;

                Stopwatch stopwatch = Stopwatch.StartNew();

                for (int j = 0; j <= newVector.Length - 2; j++)
                {
                    for (int i = 0; i <= newVector.Length - 2; i++)
                    {
                        if (newVector[i] > newVector[i + 1])
                        {
                            temporary = newVector[i + 1];
                            newVector[i + 1] = newVector[i];
                            newVector[i] = temporary;
                        }
                    }
                }

                stopwatch.Stop();
                timeVector[count - 1] = stopwatch.ElapsedTicks;
            }

            return timeVector;
        }

        public static long[] TimSort(int[] vector)
        {
            var timeVector = new long[vector.Length];

            for (int count = 1; count <= vector.Length; count++)
            {
                int[] newVector = new int[count];
                Array.Copy(vector, newVector, count);

                Stopwatch stopwatch = Stopwatch.StartNew();

                int n = newVector.Length;

                int minRun = 32, t = n, r = 0;
                while (t >= 32) { r |= t & 1; t >>= 1; }
                minRun = t + r;

                for (int i = 0; i < n; i += minRun)
                {
                    int end = Math.Min(i + minRun - 1, n - 1);
                    for (int j = i + 1; j <= end; j++)
                    {
                        int key = newVector[j], k = j - 1;
                        while (k >= i && newVector[k] > key) { newVector[k + 1] = newVector[k]; k--; }
                        newVector[k + 1] = key;
                    }
                }

                for (int s = minRun; s < n; s *= 2)
                {
                    for (int l = 0; l < n; l += 2 * s)
                    {
                        int m = Math.Min(l + s - 1, n - 1);
                        int rEnd = Math.Min(l + 2 * s - 1, n - 1);

                        if (m < rEnd)
                        {
                            int lSize = m - l + 1, rSize = rEnd - m;
                            int[] left = new int[lSize], right = new int[rSize];

                            Array.Copy(newVector, l, left, 0, lSize);
                            Array.Copy(newVector, m + 1, right, 0, rSize);

                            int i = 0, j = 0, k = l;
                            while (i < lSize && j < rSize)
                                newVector[k++] = left[i] <= right[j] ? left[i++] : right[j++];

                            while (i < lSize) newVector[k++] = left[i++];
                            while (j < rSize) newVector[k++] = right[j++];
                        }
                    }
                }

                stopwatch.Stop();
                timeVector[count - 1] = stopwatch.ElapsedTicks;
            }

            return timeVector;
        }
    }
}
