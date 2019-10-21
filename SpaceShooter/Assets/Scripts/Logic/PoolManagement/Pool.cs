using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Pool
{
	#region FIELDS

	[SerializeField]
	private TagManager.TagsEnum tag = TagManager.TagsEnum.PLAYER_BULLET_TAG;
	[SerializeField]
	private int size = 0;
	[SerializeField]
	private BasePoolObject poolPrefab = null;

	#endregion

	#region PROPERTIES

	public TagManager.TagsEnum Tag {
		get => tag;
		private set => tag = value;
	}

	public int Size {
		get => size;
		private set => size = value;
	}

	public BasePoolObject PoolPrefab {
		get => poolPrefab;
		private set => poolPrefab = value;
	}

	#endregion

	#region METHODS

	#endregion

	#region ENUMS

	#endregion
}
