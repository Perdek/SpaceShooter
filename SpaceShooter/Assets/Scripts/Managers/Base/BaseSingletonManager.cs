﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseSingletonManager<T> where T : class
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

	private void SingletonInitialization()
	{
		Instance = this as T;
	}

	#endregion

	#region ENUMS

	#endregion
}
