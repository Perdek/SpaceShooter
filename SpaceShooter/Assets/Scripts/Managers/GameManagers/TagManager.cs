using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TagManager : BaseSingletonManager<TagManager>
{
	#region FIELDS

	#endregion

	#region PROPERTIES

	#endregion

	#region METHODS

	#endregion

	#region ENUMS

	public enum TagsEnum
	{
		PLAYER_BULLET_TAG,
		ASTEROID_TAG,
		ASTEROID_EXPLOSION_TAG,
		PLAYER_GUN_BULLET_TAG,
		PLAYER_MISSILE_TAG,
		PLAYER_HOMING_MISSILE_TAG,
		LITTLE_ASTEROID_TAG,
		HP_BONUS,
		SHIELD_BONUS,
		WEAPON_BONUS
	}

	#endregion
}
