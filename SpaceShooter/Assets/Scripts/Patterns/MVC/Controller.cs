﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
	#region FIELDS

	[SerializeField]
	private View viewModule = null;
	[SerializeField]
	private Model modelModule = null;

	#endregion

	#region PROPERTIES

	public View ViewModule => viewModule;
	public Model ModelModule => modelModule;

	#endregion

	#region METHODS

	public virtual void Initialize()
	{
		ViewModule.Initialize();
		ModelModule.Initialize();
	}

	public T GetModel<T>() where T : Model
	{
		return ModelModule as T;
	}

	public T GetView<T>() where T : View
	{
		return ViewModule as T;
	}

	#endregion

	#region ENUMS

	#endregion
}