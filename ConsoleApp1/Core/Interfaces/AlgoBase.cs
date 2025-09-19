using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlgoBenchmark.Core.Interfaces.Enums;

namespace AlgoBenchmark.Core.Interfaces
{
    public abstract class AlgoBase : IAlgorithm
    {
      
        public abstract AlgorithmType Type { get; }

      
        public virtual object Execute(int n)
            => throw new NotImplementedException("Этот алгоритм требует два параметра");

        public virtual object Execute(int n, int m)
            => throw new NotImplementedException("Этот алгоритм требует один параметр");
    }
}
