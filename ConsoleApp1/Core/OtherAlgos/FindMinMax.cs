using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoBenchmark.Core.OtherAlgos
{
    using System;
    using AlgoBenchmark.Core.Interfaces;

    public  class FindMinMax: IAlgorithm
    {
        // Метод для поиска МАКСИМАЛЬНОГО элемента в массиве
        public static int Run(int[] array)
        {
            if (array == null || array.Length == 0)
                throw new ArgumentException("Массив не может быть пустым");

            // Предполагаем, что первый элемент - максимальный
            int max = array[0];

            // Последовательно сравниваем с остальными элементами
            for (int i = 1; i < array.Length; i++)
            {
                if (array[i] > max)
                {
                    max = array[i]; // Нашли новый максимум
                }
            }

            return max;
        }

        // Метод для поиска МИНИМАЛЬНОГО элемента в массиве
        public static int FindMinimum(int[] array)
        {
            if (array == null || array.Length == 0)
                throw new ArgumentException("Массив не может быть пустым");

            int min = array[0];

            for (int i = 1; i < array.Length; i++)
            {
                if (array[i] < min)
                {
                    min = array[i]; // Нашли новый минимум
                }
            }

            return min;
        }

        // Метод для одновременного поиска МАКСИМУМА и МИНИМУМА
        // (Более эффективная версия - делает примерно 3n/2 сравнений вместо 2n)
        public static (int min, int max) FindBoth(int[] array)
        {
            if (array == null || array.Length == 0)
                throw new ArgumentException("Массив не может быть пустым");

            int min, max;
            int startIndex;

            // Определяем начальные значения в зависимости от четности длины массива
            if (array.Length % 2 == 0)
            {
                // Для четной длины: сравниваем первые два элемента
                if (array[0] > array[1])
                {
                    max = array[0];
                    min = array[1];
                }
                else
                {
                    max = array[1];
                    min = array[0];
                }
                startIndex = 2;
            }
            else
            {
                // Для нечетной длины: первый элемент и min и max
                min = max = array[0];
                startIndex = 1;
            }

            // Обрабатываем элементы парами
            for (int i = startIndex; i < array.Length; i += 2)
            {
                if (array[i] > array[i + 1])
                {
                    // array[i] больше array[i+1]
                    if (array[i] > max) max = array[i];
                    if (array[i + 1] < min) min = array[i + 1];
                }
                else
                {
                    // array[i+1] больше или равен array[i]
                    if (array[i + 1] > max) max = array[i + 1];
                    if (array[i] < min) min = array[i];
                }
            }

            return (min, max);
        }
    }
}
