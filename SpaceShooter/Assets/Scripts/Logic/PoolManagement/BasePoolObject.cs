using System;
using UnityEngine;
using Zenject;
using static IBasePoolObject;

public abstract class BasePoolObject : MonoBehaviour, IBasePoolObject
{
	#region FIELDS

	public event Action OnHandleObjectSpawn = delegate { };
	public event Action<PoolObjectStateEnum> OnStateChange = delegate { };
	public event Action<BasePoolObject> OnDeactivation = delegate { };

	#endregion

	#region PROPERTIES

	public PoolObjectStateEnum State {
		get;
		private set;
	}

    public GameObject GetGameObject => gameObject;

    #endregion

    #region METHODS

    public void SetState(PoolObjectStateEnum newState)
	{
		State = newState;
		OnStateChange(State);
	}

	public virtual void HandleObjectSpawn()
	{
		OnHandleObjectSpawn();
	}

	public virtual void Deactivation()
	{
		SetState(PoolObjectStateEnum.WAITING_FOR_USE);
		gameObject.SetActive(false);
		OnDeactivation(this);
	}

	#endregion

	#region ENUMS

    #endregion

    #region INNER_CLASS

	public class Factory : PlaceholderFactory<BasePoolObject>
    {

    }

    #endregion
}
