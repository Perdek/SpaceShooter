using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitPanelView : View
{
	#region MEMBERS

	[SerializeField]
	private Transform redImageTransform = null;

	#endregion

	#region PROPERTIES

	private Transform RedImageTransform => redImageTransform;

	#endregion

	#region FUNCTIONS

	public void ShowRedScreen()
	{
		RedImageTransform.gameObject.SetActive(true);
	}

	public void HideRedScreen()
	{
		RedImageTransform.gameObject.SetActive(false);
	}

	#endregion

	#region CLASS_ENUMS

	#endregion
}
