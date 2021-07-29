using UnityEngine;

public abstract class SpawnableElement<T> : MonoBehaviour, ISpawnableElement<T>
{
    #region MEMBERS

    #endregion

    #region PROPERTIES

    public abstract T ValueReference { get; set; }

    #endregion

    #region METHODS

    public abstract void AttachEvents();

    public abstract void DetachEvents();

    public abstract GameObject GetGameObject();

    public abstract void HandleDestroy();

    public abstract void RefreshElement();

    #endregion
}
