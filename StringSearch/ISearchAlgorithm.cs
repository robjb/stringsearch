using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StringSearch
{
    // T is the type of the object produced by running the algorithm
    public interface ISearchAlgorithm<T>
    {
        string Name { get; }

        T Run(char[] s);

        // Searching not yet implemented in algorithm classes
        // int Search(T result, string key)
    }
}
