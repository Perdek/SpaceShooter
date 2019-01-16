using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMainManager : BaseMonoBehaviourSingletonManager<GameMainManager>
{

	#region FIELDS

	[SerializeField]
	private PlayerManager playerManager = null;
	[SerializeField]
	private PoolManager poolManager = null;

	#endregion

	#region PROPERTIES

	public PlayerManager PlayerManager => playerManager;
	public PoolManager PoolManager  => poolManager;

	public KeyboardManager KeyboardManager {
		get;
		private set;
	} = new KeyboardManager();
	

	#endregion

	#region METHODS

	protected virtual void Awake()
	{
		SingletonsInitializes();
		ManagersInitialize();
	}

	protected virtual void Update()
	{
		KeyboardManager.CheckKeys();
	}

	private void SingletonsInitializes()
	{
		SingletonInitialization();
		KeyboardManager.SingletonInitialization();
		PlayerManager.SingletonInitialization();
		PoolManager.SingletonInitialization();
	}

	private void ManagersInitialize()
	{
		KeyboardManager.Initialize();
		PlayerManager.Initialize();
		PoolManager.Initialize();
	}

	#endregion

	#region ENUMS

	#endregion
}
