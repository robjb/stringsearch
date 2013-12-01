namespace StringSearch
{
    // T is the type of the input argument required by the prefix counter
    public interface IPrefixCounter<in T>
    {
        int[] CountPrefixes(T input);
    }
}
