using System;
using System.Collections;
using System.Collections.Generic;

namespace AutoMapper.Bugfix.Service.Entities;

public class AutoMappingSharedCollection : ICollection<AutoMappingSharedClass>, IEnumerable<AutoMappingSharedClass>
{
    private readonly List<AutoMappingSharedClass> _data = [];

    private readonly Dictionary<string, AutoMappingSharedClass> _dataDict
        = new(StringComparer.OrdinalIgnoreCase);

    /// <inheritdoc/>
    public int Count => _data.Count;

    /// <inheritdoc/>
    public bool IsReadOnly => false;

    public void AddRange(IEnumerable<AutoMappingSharedClass> collection)
    {
        _ = collection ?? throw new ArgumentNullException(nameof(collection));

        foreach (var item in collection)
        {
            Add(item);
        }
    }

    /// <inheritdoc/>
    public void Add(AutoMappingSharedClass item)
    {
        _ = item ?? throw new ArgumentNullException(nameof(item));
        _ = item.Name ?? throw new NullReferenceException(nameof(item.Name));

        if (_dataDict.ContainsKey(item.Name))
        {
            throw new Exception();
        }
        _data.Add(item);
        _dataDict.Add(item.Name, item);
    }

    /// <inheritdoc/>
    public void Clear()
    {
        _data.Clear();
        _dataDict.Clear();
    }

    /// <inheritdoc/>
    public bool Contains(AutoMappingSharedClass item)
    {
        _ = item ?? throw new ArgumentNullException(nameof(item));
        _ = item.Name ?? throw new NullReferenceException(nameof(item.Name));

        return _dataDict.ContainsKey(item.Name);
    }

    /// <inheritdoc/>
    public void CopyTo(AutoMappingSharedClass[] array, int arrayIndex)
    {
        _ = array ?? throw new ArgumentNullException(nameof(array));

        foreach (var data in array)
        {
            _ = data ?? throw new NullReferenceException(nameof(data));
            _ = data.Name ?? throw new NullReferenceException(nameof(data.Name));

            if (_dataDict.ContainsKey(data.Name))
            {
                throw new Exception();
            }
        }
        _data.CopyTo(array, arrayIndex);

        foreach (var data in array)
        {
            _dataDict.Add(data.Name, data);
        }
    }

    /// <inheritdoc/>
    public IEnumerator<AutoMappingSharedClass> GetEnumerator() => _data.GetEnumerator();

    /// <inheritdoc/>
    IEnumerator IEnumerable.GetEnumerator() => _data.GetEnumerator();

    /// <inheritdoc/>
    public bool Remove(AutoMappingSharedClass item)
    {
        _ = item ?? throw new ArgumentNullException(nameof(item));
        _ = item.Name ?? throw new NullReferenceException(nameof(item.Name));

        bool removed = _data.Remove(item);

        if (removed)
        {
            _dataDict.Remove(item.Name);
        }
        return removed;
    }

    public AutoMappingSharedClass? TryGetRecordMetadata(string fieldName)
    {
        if (_dataDict.TryGetValue(fieldName, out AutoMappingSharedClass? value))
        {
            return value;
        }
        return null;
    }

    public void Sort(Comparison<AutoMappingSharedClass> commpareRule)
    {
        _data.Sort(commpareRule);
    }


}
