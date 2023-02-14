using UnityEngine;
using System.Collections;

public abstract class Enemy : BasePoolObject
{
	#region MEMBERS

	[SerializeField]
	private EnemyInformation enemyInformation = null;

	#endregion

	#region PROPERTIES

	public EnemyInformation EnemyInformation => enemyInformation;

	#endregion

	#region FUNCTIONS

	#endregion

	#region CLASS_ENUMS

	#endregion
}
