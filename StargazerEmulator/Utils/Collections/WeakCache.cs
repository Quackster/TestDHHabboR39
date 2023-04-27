using System.Collections.Concurrent;

namespace Stargazer.Utils.Collections;

public class WeakCache<TKey, TValue> where TKey : class where TValue : class
{
    public delegate TValue ValueGenerator();
    
    private readonly ConcurrentDictionary<TKey, WeakReference<TValue>> _cache;
    private readonly ValueGenerator? _valueGenerator;

    public WeakCache(ValueGenerator valueGenerator)
    {
        _cache = new ConcurrentDictionary<TKey, WeakReference<TValue>>();
        _valueGenerator = valueGenerator;
    }

    // Number of items in the cache.
    public int Count => _cache.Count;

    // Retrieve a data object from the cache.
    public TValue? this[TKey key]
    {
        get
        {
            var hasKey = _cache.TryGetValue(key, out var reference);

            switch (hasKey)
            {
                case false when _valueGenerator == null:
                    return default;
                case false when _valueGenerator != null:
                    reference = new WeakReference<TValue>(_valueGenerator());
                    _cache.TryAdd(key, reference);
                    break;
            }

            var hasTarget = _cache[key].TryGetTarget(out var value);

            switch (hasTarget)
            {
                case false when _valueGenerator == null:
                    return default;
                case false when _valueGenerator != null:
                    value = _valueGenerator();
                    _cache[key].SetTarget(value);
                    return value;
            }

            return value;
        }
    }
}
