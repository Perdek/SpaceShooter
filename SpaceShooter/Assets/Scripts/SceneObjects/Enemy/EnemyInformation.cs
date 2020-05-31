using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy Information", menuName = "Enemy Information")]
public class EnemyInformation : ScriptableObject
{
	#region FIELDS

	[SerializeField]
	private int scorePointsOnDestroy = 1;
	[SerializeField]
	private int moneyBonusOnDestroy = 1;

	#endregion

	#region PROPERTIES

	public int ScorePointsOnDestroy => scorePointsOnDestroy;
	public int MoneyBonusOnDestroy => moneyBonusOnDestroy;

	#endregion

	#region METHODS

	#endregion

	#region ENUMS

	#endregion
}
