using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : BaseMonoBehaviourSingletonManager<LevelManager>
{
	#region FIELDS

	[SerializeField]
	private List<SpawnPoint> spawnPoints = new List<SpawnPoint>();

	#endregion

	#region PROPERTIES

	public List<SpawnPoint> SpawnPoints => spawnPoints;

	private int FinishedSpawnPoints {
		get;
		set;
	} = 0;

	private int EnemyCount {
		get;
		set;
	} = 0;

	#endregion

	#region METHODS

	public override void Initialize()
	{
		for (int i = 0; i < SpawnPoints.Count; i++)
		{
			SpawnPoints[i].OnSpawn += UpdateEnemyCount;
			SpawnPoints[i].OnSpawnEnd += UpdateFinishedSpawnPoints;
		}
	}

	public void StartLevel()
	{
		for (int i = 0; i < SpawnPoints.Count; i++)
		{
			SpawnPoints[i].StartSpawn();
		}
	}

	public void EndLevel()
	{
		for (int i = 0; i < SpawnPoints.Count; i++)
		{
			SpawnPoints[i].OnSpawn -= UpdateEnemyCount;
			SpawnPoints[i].OnSpawnEnd -= UpdateFinishedSpawnPoints;
		}
	}

	private void UpdateFinishedSpawnPoints()
	{
		FinishedSpawnPoints++;
		HandleFinishedSpawnPoints();
	}

	private void HandleFinishedSpawnPoints()
	{
		if (FinishedSpawnPoints == SpawnPoints.Count)
		{

		}
	}

	private void UpdateEnemyCount()
	{
		EnemyCount++;
	}

	#endregion

	#region ENUMS

	#endregion
}
