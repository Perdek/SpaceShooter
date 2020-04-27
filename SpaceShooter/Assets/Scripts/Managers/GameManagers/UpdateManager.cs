using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateManager : BaseMonoBehaviourSingletonManager<UpdateManager>
{
	#region FIELDS

	public event System.Action OnUpdateInputInformation = delegate { };
	public event System.Action OnDataChange = delegate { };
	public event System.Action OnUpdatePhysic = delegate { };
	public event System.Action OnUpdateView = delegate { };

	#endregion

	#region PROPERTIES

	#endregion

	#region METHODS

	protected virtual void Update()
	{
		HandleOnUpdateInputInformation();
	}

	protected virtual void FixedUpdate()
	{
		HandleOnDataChange();
		HandleOnUpdatePhysic();
		HandleOnUpdateView();
	}

	private void HandleOnUpdateInputInformation()
	{
		OnUpdateInputInformation();
	}

	private void HandleOnDataChange()
	{
		OnDataChange();
	}

	private void HandleOnUpdatePhysic()
	{
		OnUpdatePhysic();
	}

	private void HandleOnUpdateView()
	{
		OnUpdateView();
	}

	#endregion

	#region ENUMS

	#endregion
}