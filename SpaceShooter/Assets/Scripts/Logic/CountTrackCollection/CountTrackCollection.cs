using System;
using System.Collections.Generic;
using System.Linq;

public class CountTrackCollection<T>
{
    #region MEMBERS

    public event Action<int> OnCollectionCountChange = delegate { };
    public event Action<T> OnAddElement = delegate { };
    public event Action<T> OnRemoveElement = delegate { };
    public event Action<T> OnUpdateElement = delegate { };

    #endregion

    #region PROPERTIES

    public int Count => Collection.Count;

    public List<T> Collection { get; protected set; } = new List<T>();

    public T this[int index]
    {
        get => Collection[index];
        set
        {
            if (index >= 0 && index < Count)
            {
                Collection[index] = value;
                OnUpdateElement?.Invoke(Collection[index]);
            }
        }
    }

    #endregion

    #region METHODS

    public CountTrackCollection()
    {
        Collection = new List<T>();
    }

    public CountTrackCollection(IEnumerable<T> s)
    {
        Collection = s.ToList();
    }

    public virtual void Add(T elementToAdd)
    {
        Collection.Add(elementToAdd);
        OnAddElement(elementToAdd);
        OnCollectionCountChange.Invoke(Collection.Count);
    }

    public void AddRange(List<T> elementsToAdd)
    {
        for (int i = 0; i < elementsToAdd.Count; i++)
        {
            Add(elementsToAdd[i]);
        }
    }

    public virtual void Remove(T elementToRemove)
    {
        Collection.Remove(elementToRemove);
        OnRemoveElement(elementToRemove);
        OnCollectionCountChange.Invoke(Collection.Count);
    }

    public virtual void RemoveAt(int index)
    {
        T elementToRemove = Collection[index];
        Collection.RemoveAt(index);

        OnRemoveElement(elementToRemove);
        OnCollectionCountChange.Invoke(Collection.Count);
    }

    public void Clear()
    {
        for (int i = Count - 1; i >= 0; i--)
        {
            RemoveAt(i);
        }
    }

    public IEnumerator<T> GetEnumerator()
    {
        return Collection.GetEnumerator();
    }

    #endregion
}