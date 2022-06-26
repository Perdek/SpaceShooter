using UnityEngine;

[System.Serializable]
public class Pool
{
	#region FIELDS

	[SerializeField]
	private SpawnableObjectsTagsEnum tag = SpawnableObjectsTagsEnum.PLAYER_BULLET_TAG;
	[SerializeField]
	private int size = 0;
	[SerializeField]
	private BasePoolObject poolPrefab = null;

	#endregion

	#region PROPERTIES

	public SpawnableObjectsTagsEnum Tag {
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
