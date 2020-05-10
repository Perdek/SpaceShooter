using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AsteroidViewComponent
{
	#region MEMBERS

	#endregion

	#region PROPERTIES

	private Transform AsteroidTransform {
		get;
		set;
	} = null;

	#endregion

	#region METHODS

	public void SetAsteroidTranform(Transform asteroidTransform)
	{
		AsteroidTransform = asteroidTransform;
	}

	public void Explosion()
	{
		if (PoolManager.Instance != null)
		{
			PoolManager.Instance.GetPoolObject(TagManager.TagsEnum.ASTEROID_EXPLOSION_TAG, AsteroidTransform.position, AsteroidTransform.rotation);
		}
	}

	#endregion

	#region CLASS_ENUMS

	#endregion
}
