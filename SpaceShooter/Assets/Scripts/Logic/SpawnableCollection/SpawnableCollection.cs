using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public abstract class SpawnableCollection<T, U> where T : ISpawnableElement<U> where U : class
{
    #region MEMBERS

    [SerializeField] protected Transform _elementsParent = default;
    [SerializeField] protected T _spawnablePrefab = default;

    private DiContainer _container;

    #endregion

    #region PROPERTIES

    public int Count => SpawnedCollection.Count;

    public CountTrackCollection<T> SpawnedCollection {
        get;
        private set;
    } = new CountTrackCollection<T>();


    #endregion

    #region METHODS

    public void InjectDependencies(DiContainer diContainer)
    {
        _container = diContainer;
    }

    public void RefreshSection(List<U> content)
    {
        ClearSection();
        FillSection(content);

        CountTrackCollection<int> testCountrackCollection = new CountTrackCollection<int>();
    }

    public void FillSection(List<U> content)
    {
        for (int i = 0; i < content.Count; i++)
        {
            AddElement(content[i]);
        }
    }

    public virtual T AddElement(U contentValue)
    {
        GameObject newElementGameObject = _container.InstantiatePrefab(_spawnablePrefab.GetGameObject(), _elementsParent);

        T newElement = newElementGameObject.GetComponent<T>();
        newElement.ValueReference = contentValue;
        newElement.AttachEvents();
        newElement.RefreshElement();
        newElement.GetGameObject().SetActiveOptimized(true);

        SpawnedCollection.Add(newElement);

        return newElement;
    }

    public void ClearSection()
    {
        for (int i = SpawnedCollection.Count - 1; i >= 0; i--)
        {
            RemoveSpawnedElement(SpawnedCollection[i]);
        }
    }

    public void RemoveValue(U valueToRemove)
    {
        for (int i = SpawnedCollection.Count - 1; i >= 0; i--)
        {
            if (SpawnedCollection[i].ValueReference == valueToRemove)
            {
                RemoveSpawnedElement(SpawnedCollection[i]);
            }
        }
    }

    public virtual void RemoveSpawnedElement(T spawnedElement)
    {
        spawnedElement.DetachEvents();
        spawnedElement.HandleDestroy();
        GameObject.Destroy(spawnedElement.GetGameObject());
        SpawnedCollection.Remove(spawnedElement);
    }

    public T GetFirstElement()
    {
        return SpawnedCollection.Collection.First();
    }

    public T GetElement(U valueReference)
    {
        for (int i = 0; i < SpawnedCollection.Count; i++)
        {
            if (SpawnedCollection[i].ValueReference == valueReference)
            {
                return SpawnedCollection[i];
            }
        }

        return default(T);
    }

    #endregion
}