using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMainManager : BaseMonoBehaviourSingletonManager<GameMainManager>
{
	#region FIELDS

	#endregion

	#region PROPERTIES

	public KeyboardManager KeyboardManager {
		get;
		private set;
	} = new KeyboardManager();

	public PlayerManager PlayerManager {
		get;
		private set;
	} = new PlayerManager();

	#endregion

	#region METHODS

	protected override void Awake()
	{
		base.Awake();
		KeyboardManager.Initialize();
		PlayerManager.Initialize();
	}

	protected virtual void Update()
	{
        KeyboardManager.CheckKeys();
	}

	#endregion

	#region ENUMS

	#endregion
}
