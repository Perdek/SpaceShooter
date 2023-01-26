using UnityEngine;
using Zenject;

public class PoolObjectsParent : MonoBehaviour
{
	#region FIELDS

	#endregion

	#region PROPERTIES

	public string Tag {
		get;
		private set;
	}

	#endregion

	#region METHODS

	public void SetTag(string newTag)
	{
		Tag = newTag;
	}

	#endregion

	#region FACTORY

	public class Factory : PlaceholderFactory<Object, Transform, PoolObjectsParent>
	{

	}

	#endregion
}
