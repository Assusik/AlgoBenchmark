using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlgoBenchmark.Core.Interfaces;
using AlgoBenchmark.Core.Interfaces.Enums;
using AlgoBenchmark.Core.Services;

namespace AlgoBenchmark.Core.OtherAlgos
{
    //Алгоритм работает поразрядно, начиная с младшего разряда
    //(Least Significant Digit) и заканчивая старшим. На каждом шаге он использует
    //стабильную сортировку (чаще всего — сортировку подсчетом) для упорядочивания элементов по текущему разряду.
    class LSDRadixSort : AlgoBase
    {
        public override AlgorithmType Type => AlgorithmType.Vector;
        public override object Execute(int n)
        {
            
            var array = DataGenerator.GenerateVector(n);
            Stopwatch stopwatch = Stopwatch.StartNew();
            stopwatch.Start();
            if (array == null || array.Length <= 1)
            {
                return 0;
            }

            var max = FindMaxElement(array);
            for (int exp = 1; max/exp > 0; exp*=10)
            {
                CountingSortByDigit(array, exp);
            }
            return 0;
           

        }
        private static int FindMaxElement(int[] array)
        {
            int max = array[0];
            for (int i = 1; i < array.Length; i++)
            {
                if (array[i] > max)
                {
                    max = array[i];
                }
            }
            return max;
        }
        private static void CountingSortByDigit(int[] array,int exp)
        {
            int[] output = new int[array.Length];
            int[] count = new int[10]; // 10 т.к десятичные числа
           
            
            for (int i = 0; i < 10; i++) 
                count[i] = 0;

            for (int i = 0; i < array.Length; i++)
            {
                int digit = array[i] / exp % 10;
                count[digit]++;
            }
            for (int i = 1; i < 10; i++)
                count[i] += count[i - 1];
            for (int i = array.Length - 1; i >= 0; i--)
            {
                int digit = array[i] / exp % 10;
                output[count[digit] - 1] = array[i];
                count[digit]--;
            }
            for (int i = 0; i < array.Length; i++)
                array[i] = output[i];


        }
    }
}
