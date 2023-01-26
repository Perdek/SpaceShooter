using UnityEngine;

public interface ISpawnableElement<T> : IAttachable
{
    #region MEMBERS

    #endregion

    #region PROPERTIES

    public T ValueReference { get; set; }

    #endregion

    #region METHODS

    public void RefreshElement();
    public GameObject GetGameObject();
    public void HandleDestroy();

    #endregion
}