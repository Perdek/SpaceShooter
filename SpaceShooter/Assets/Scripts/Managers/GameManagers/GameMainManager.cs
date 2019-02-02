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
	[SerializeField]
	private UpdateManager updateManager = null;

	#endregion

	#region PROPERTIES

	public PlayerManager PlayerManager => playerManager;
	public PoolManager PoolManager => poolManager;
	public UpdateManager UpdateManager => updateManager;

	public KeyboardManager KeyboardManager {
		get;
		private set;
	} = new KeyboardManager();

	public TagManager TagManager {
		get;
		private set;
	} = new TagManager();



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
		UpdateManager.SingletonInitialization();
		KeyboardManager.SingletonInitialization();
		PlayerManager.SingletonInitialization();
		PoolManager.SingletonInitialization();
		TagManager.SingletonInitialization();
	}

	private void ManagersInitialize()
	{
		UpdateManager.Initialize();
		KeyboardManager.Initialize();
		PlayerManager.Initialize();
		PoolManager.Initialize();
		TagManager.Initialize();
	}

	#endregion

	#region ENUMS

	#endregion
}
