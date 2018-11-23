using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMainManager : BaseMonoBehaviourSingletonManager<GameMainManager>
{

	#region FIELDS

	[SerializeField]
	private PlayerManager playerManager = null;

	#endregion

	#region PROPERTIES

	public PlayerManager PlayerManager => playerManager;

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
	}

	private void ManagersInitialize()
	{
		KeyboardManager.Initialize();
		PlayerManager.Initialize();
	}

	#endregion

	#region ENUMS

	#endregion
}
