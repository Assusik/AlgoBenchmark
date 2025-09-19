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
    public  class QuickSelect : AlgoBase
    {
        public override AlgorithmType Type => AlgorithmType.Vector;

        // Основной публичный метод для нахождения k-го наименьшего элемента
        public object Execute(int n, int k)
        {
            var array = DataGenerator.GenerateVector(n);
            Stopwatch stopwatch = Stopwatch.StartNew();
            stopwatch.Start();
            if (array == null || array.Length == 0)
                throw new ArgumentException("Массив не может быть пустым");
            if (k < 1 || k > array.Length)
                throw new ArgumentException("k должно быть в диапазоне от 1 до длины массива");

            // k-й наименьший элемент имеет индекс k-1 в отсортированном массиве
            stopwatch.Start();
            return QuickSelectRecursive(array, 0, array.Length - 1, k - 1);
        }

        // Рекурсивная реализация алгоритма
        private static int QuickSelectRecursive(int[] array, int left, int right, int k)
        {
            if (left == right)
                return array[left];

            // Выбираем опорный элемент и разбиваем массив
            int pivotIndex = Partition(array, left, right);

            // Определяем, в какой части массива продолжать поиск
            if (k == pivotIndex)
                return array[k];
            else if (k < pivotIndex)
                return QuickSelectRecursive(array, left, pivotIndex - 1, k);
            else
                return QuickSelectRecursive(array, pivotIndex + 1, right, k);
        }

        // Метод разбиения Lomuto
        private static int Partition(int[] array, int left, int right)
        {
            // Выбираем опорный элемент (можно оптимизировать выбор)
            int pivot = array[right];
            int i = left;

            for (int j = left; j < right; j++)
            {
                if (array[j] <= pivot)
                {
                    Swap(array, i, j);
                    i++;
                }
            }

            Swap(array, i, right);
            return i;
        }

        // Вспомогательный метод для обмена элементов
        private static void Swap(int[] array, int i, int j)
        {
            int temp = array[i];
            array[i] = array[j];
            array[j] = temp;
        }

        
    }
}
