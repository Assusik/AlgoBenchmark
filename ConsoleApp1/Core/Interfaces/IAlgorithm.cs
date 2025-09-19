using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlgoBenchmark.Core.Interfaces.Enums;

namespace AlgoBenchmark.Core.Interfaces
{
    interface IAlgorithm
    {
        public object Execute(int n);
        public object Execute(int n, int m);
        public AlgorithmType Type { get; }

        
    }
}
