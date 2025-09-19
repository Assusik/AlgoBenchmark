using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoBenchmark.Core.Interfaces.Enums
{
    public enum AlgorithmType
    {
        Vector,     // Использует Execute(int n)
        Matrix,     // Использует Execute(int n, int m)  
        Polynomial
    }
}
