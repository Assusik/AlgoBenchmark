using System;
using AlgoBenchmark.Core.Interfaces;

namespace AlgoBenchmark.Core.OtherAlgos
{
    public class CountingSort : IAlgorithm
    {
        public static void Run(int[] array)
        {
            
            if (array == null || array.Length <= 1)
                return;

            // 1. Находим минимальное и максимальное значение
            int min = array[0];
            int max = array[0];

            for (int i = 1; i < array.Length; i++)
            {
                if (array[i] < min) min = array[i];
                if (array[i] > max) max = array[i];
            }

            // 2. Создаем массив для подсчета (диапазон = max - min + 1)
            int range = max - min + 1;
            int[] count = new int[range];

            // 3. Подсчитываем количество каждого элемента
            for (int i = 0; i < array.Length; i++)
            {
                count[array[i] - min]++; // нормализуем индекс
            }

            // 4. Преобразуем count: теперь count[i] содержит позицию
            // последнего элемента со значением i + min в отсортированном массиве
            for (int i = 1; i < range; i++)
            {
                count[i] += count[i - 1];
            }

            // 5. Создаем выходной массив
            int[] output = new int[array.Length];

            // 6. Заполняем выходной массив (идем с конца для стабильности)
            for (int i = array.Length - 1; i >= 0; i--)
            {
                int normalizedIndex = array[i] - min;
                int position = count[normalizedIndex] - 1; // получаем позицию в output
                output[position] = array[i];
                count[normalizedIndex]--; // уменьшаем счетчик
            }

            // 7. Копируем отсортированный массив обратно в исходный
            Array.Copy(output, array, array.Length);
        }

        
    }
}