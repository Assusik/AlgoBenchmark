using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoBenchmark.Core.OtherAlgos
{
    using System;
    using System.Diagnostics;
    using AlgoBenchmark.Core.Interfaces;
    using AlgoBenchmark.Core.Interfaces.Enums;
    using AlgoBenchmark.Core.Services;

    public  class FindMinMax: AlgoBase
    {
        // Метод для поиска МАКСИМАЛЬНОГО элемента в массиве
        public override AlgorithmType Type => AlgorithmType.Vector;
        public override object Execute(int n)
        {
            var array = DataGenerator.GenerateVector(n);
            Stopwatch stopwatch = Stopwatch.StartNew();
            stopwatch.Start();
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

            stopwatch.Stop();
            return max;
        }

    }
}
