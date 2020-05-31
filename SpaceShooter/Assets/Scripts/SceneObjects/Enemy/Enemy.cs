using UnityEngine;
using System.Collections;

public class Enemy : BasePoolObject
{
	#region MEMBERS

	[SerializeField]
	private EnemyInformation enemyInformation = null;

	#endregion

	#region PROPERTIES

	protected EnemyInformation EnemyInformation => enemyInformation;

	#endregion

	#region FUNCTIONS

	#endregion

	#region CLASS_ENUMS

	#endregion
}
