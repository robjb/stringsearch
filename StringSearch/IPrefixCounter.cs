using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StringSearch
{
    // T is the type of the input argument required by the prefix counter
    public interface IPrefixCounter<T>
    {
        int[] CountPrefixes(T input);
    }
}
