using System;
using UnityEngine;
using Zenject;

public interface IBasePoolObject
{
    #region MEMBERS

    public event Action OnHandleObjectSpawn;
    public event Action<PoolObjectStateEnum> OnStateChange;
    public event Action<BasePoolObject> OnDeactivation;

    #endregion

    #region PROPERTIES

    public GameObject GetGameObject { get; }

    #endregion

    #region METHODS

    public void SetState(PoolObjectStateEnum newState);

    public void HandleObjectSpawn();

    public void Deactivation();

    #endregion

    #region ENUMS

    public enum PoolObjectStateEnum
    {
        WAITING_FOR_USE,
        IN_USE
    }

    #endregion

    #region FACTORY

    public class Factory : PlaceholderFactory<UnityEngine.Object, UnityEngine.Transform, IBasePoolObject>
    {

    }

    #endregion
}
