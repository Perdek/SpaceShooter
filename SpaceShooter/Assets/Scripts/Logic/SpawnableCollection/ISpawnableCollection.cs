using System.Collections.Generic;
using UnityEngine;

public interface ISpawnableCollection<U, T> where U : ISpawnableElement<T>
{
    #region MEMBERS

    #endregion

    #region PROPERTIES

    #endregion

    #region METHODS

    public CountTrackCollection<T> GetSpawnedCollection();

    public void RefreshSection(List<T> collection);

    public void AddElement(T newElement);

    public void RemoveElement(T elementToRemove);

    #endregion
}
