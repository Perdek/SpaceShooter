using UnityEngine;

public class Asteroid : BasePoolObject
{
	#region FIELDS

	[SerializeField]
	private uint speedFactory = 10;
	[SerializeField]
	private float speedLotteryRangeFactor = 1;
	[SerializeField]
	private Rigidbody2D rigidbody2DComponent = null;
	[SerializeField]
	private float directionLotteryRange = 0;

	#endregion

	#region PROPERTIES

	public Rigidbody2D Rigidbody2DComponent => rigidbody2DComponent;
	public uint SpeedFactory => speedFactory;
	public float SpeedLotteryRangeFactor => speedLotteryRangeFactor;
	public float DirectionLotteryRange => directionLotteryRange;

	private float CalculatedSpeedFactor {
		get;
		set;
	} = 1;

	private Vector2 CalculatedDirection {
		get;
		set;
	} = new Vector2();

	#endregion

	#region METHODS

	public override void HandleObjectSpawn()
	{
		if (State == PoolObjectStateEnum.WAITING_FOR_USE)
		{
			UpdateManager.Instance.OnUpdatePhysic += Move;
		}

		RandomSpeedFactor();
		RandomDirectionValue();
	}

	public override void Deactivation()
	{
		base.Deactivation();
		Explosion();
		UpdateManager.Instance.OnUpdatePhysic -= Move;
	}

	protected virtual void OnTriggerEnter2D(Collider2D other)
	{
		HandleCollision(other);
	}

	private void RandomSpeedFactor()
	{
		float minSpeed = SpeedFactory - SpeedLotteryRangeFactor;
		float maxSpeed = SpeedFactory + SpeedLotteryRangeFactor;
		CalculatedSpeedFactor = Random.Range(minSpeed > 1 ? minSpeed : 1, maxSpeed);
	}

	private void RandomDirectionValue()
	{
		float minDirectionValue = -DirectionLotteryRange;
		float maxDirectionValue = DirectionLotteryRange;
		CalculatedDirection = new Vector2(Random.Range(minDirectionValue, maxDirectionValue), -1);
	}

	private void Move()
	{
		Rigidbody2DComponent.MovePosition(Rigidbody2DComponent.position + CalculatedDirection * Time.fixedDeltaTime * CalculatedSpeedFactor);
	}

	private void Explosion()
	{
		Debug.Log("Explosion");
	}

	private void HandleCollision(Collider2D other)
	{
		if (other.GetComponent<SceneBottonCollider>() != null || other.GetComponent<Bullet>() != null)
		{
			Deactivation();
		}
	}

	private bool CheckCollisionWithPlayerBullet(Collider2D other)
	{
		Bullet bullet = other.GetComponentInChildren<Bullet>();

		return bullet != null ? false : bullet.Iff == IdentificationFriendOrFoeEnum.FRIEND;
	}

	#endregion

	#region ENUMS

	#endregion
}
