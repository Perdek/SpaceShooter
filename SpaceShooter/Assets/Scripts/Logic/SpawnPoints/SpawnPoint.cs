using System;
using Unity.UNetWeaver;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
	#region FIELDS

	public event System.Action<BasePoolObject> OnSpawn = delegate { };
	public event System.Action OnSpawnEnd = delegate { };

	[SerializeField]
	private TagManager.TagsEnum poolObjectTag = TagManager.TagsEnum.PLAYER_BULLET_TAG;

	[SerializeField]
	private float firstSpawnDelayInSeconds = 0f;

	[SerializeField]
	private float delayBetweenSpawnsInSeconds = 5f;

	[SerializeField]
	private int spawnObjectsLimit = 1;

	#endregion

	#region PROPERTIES

	public TagManager.TagsEnum PoolObjectTag => poolObjectTag;
	public float FirstSpawnDelayInSeconds => firstSpawnDelayInSeconds;
	public float DelayBetweenSpawnsInSeconds => delayBetweenSpawnsInSeconds;
	public int SpawnObjectsLimit => spawnObjectsLimit;

	private Timer SpawningTimer {
		get;
		set;
	} = null;

	private int CurrentSpawnedObjectNumber {
		get;
		set;
	} = 0;

	#endregion

	#region METHODS

	public void StartSpawn()
	{
		SpawningTimer = new Timer(FirstSpawnDelayInSeconds, DelayBetweenSpawnsInSeconds, Spawn, true);
		SpawningTimer.StartCounting();
	}

	public void Spawn()
	{
		BasePoolObject basePoolObject = PoolManager.Instance.GetPoolObject(PoolObjectTag, this.transform.position, this.transform.rotation);
		CurrentSpawnedObjectNumber++;
		OnSpawn(basePoolObject);
		HandleSpawnEnd();
	}

	public void EndLevel()
	{
		SpawningTimer.EndCounting();
		SpawningTimer = null;
	}

	private void HandleSpawnEnd()
	{
		if (CheckSpawnEnd() == true)
		{
			SpawningTimer.EndCounting();
			OnSpawnEnd();
		}
	}

	private bool CheckSpawnEnd()
	{
		return CurrentSpawnedObjectNumber == SpawnObjectsLimit;
	}

	#endregion

	#region ENUMS

	#endregion
}
