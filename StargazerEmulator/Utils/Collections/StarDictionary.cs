using System.Collections.Concurrent;

namespace Stargazer.Utils.Collections;

public class StarDictionary<TKey, TValue> : ConcurrentDictionary<TKey, TValue> where TKey : notnull
{
    public new TValue? this[TKey key]
    {
        get => TryGetValue(key, out var value) ? value : default;
        set
        {
            if (value != null)
            {
                AddOrUpdate(key, value, (tkey, oldValue) => value);
            }
        }
    }
}