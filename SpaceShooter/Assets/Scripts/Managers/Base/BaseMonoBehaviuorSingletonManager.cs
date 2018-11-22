using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseMonoBehaviourSingletonManager<T> : MonoBehaviour where T : MonoBehaviour
{
	#region FIELDS

	private static T instance;

	#endregion

	#region PROPERTIES

	public static T Instance {
		get => instance;
		private set => instance = value;
	}

	#endregion

	#region METHODS

	protected virtual void Awake()
	{
		SingletonInitialization();
	}

	private void SingletonInitialization()
	{
		Instance = this as T;
	}

	#endregion

	#region ENUMS

	#endregion
}
