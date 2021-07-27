using UnityEngine;

public interface ISpawnableCollection<U, T> where U : ISpawnableElement<T>
{
    #region MEMBERS

    #endregion

    #region PROPERTIES

    #endregion

    #region METHODS

    public void SetValueReference(T newValue);
    public void RefreshElement();
    public GameObject GetGameObject();
    public void AttachEvents();
    public void DetachEvents();
    public void HandleDestroy();

    #endregion
}
