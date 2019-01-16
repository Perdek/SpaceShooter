using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Pool
{
	#region FIELDS

	[SerializeField]
	private string tag;
	[SerializeField]
	private int size;
	[SerializeField]
	private BasePoolObject poolPrefab;

	#endregion

	#region PROPERTIES

	public string Tag {
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
