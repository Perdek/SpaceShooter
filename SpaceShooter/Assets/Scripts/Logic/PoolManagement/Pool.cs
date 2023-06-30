using UnityEngine;
using UnityEngine.Serialization;

[System.Serializable]
public class Pool
{
	#region FIELDS

	[FormerlySerializedAs("tag")] [SerializeField]
	private SpawnableObjectsTagsEnum _tag = SpawnableObjectsTagsEnum.PLAYER_BULLET_TAG;
	[FormerlySerializedAs("size")] [SerializeField]
	private int _size = 0;
	[FormerlySerializedAs("poolPrefab")] [SerializeField]
	private BasePoolObject _poolPrefab = null;

	#endregion

	#region PROPERTIES

	public SpawnableObjectsTagsEnum Tag => _tag;
	public int Size => _size;
	public BasePoolObject PoolPrefab => _poolPrefab;

	#endregion

	#region METHODS

	#endregion

	#region ENUMS

	#endregion
}
