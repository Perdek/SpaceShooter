using System;
using UnityEngine;

[Serializable]
public class PlayerColliderController : MonoBehaviour
{
	#region FIELDS

	public event Action<int> OnDamageCollision = delegate { };

	#endregion

	#region PROPERTIES

	#endregion

	#region METHODS

	public void DamageCollision(int damageCollisionValue)
	{
		OnDamageCollision(damageCollisionValue);
	}

	#endregion

	#region ENUMS

	#endregion

}
