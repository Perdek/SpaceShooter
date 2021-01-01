using UnityEngine;

[System.Serializable]
public class Weapon
{
	#region FIELDS

	[SerializeField]
	private TagManager.TagsEnum bulletTag = TagManager.TagsEnum.PLAYER_BULLET_TAG;
	[SerializeField]
	private WeaponInformation weaponInformation = null;

	#endregion

	#region PROPERTIES

	private Transform BulletSpawnPoint { get; set; }
	private WeaponInformation WeaponInformation => weaponInformation;
	private Timer ReloadingMagazineTimer { get; set; }
	private Timer BoltReloadCycleTimer { get; set; }
	private int WeaponLevel { get; set; } = 1;
	private BoolValue IsReloading { get; set; } = new BoolValue(false);
	private int BulletLeftInMagazine { get; set; }

	private PlayerShootingController CachedPlayerShootingController
	{
		get;
		set;
	}


	#endregion

	#region METHODS

	public void InitializeBulletTransform(Transform bulletSpawnPointTransform)
	{
		BulletSpawnPoint = bulletSpawnPointTransform;
	}

	public void InitializeWeapon()
    {
		BulletLeftInMagazine = WeaponInformation.MagazineCapacity;

		ReloadingMagazineTimer = new Timer(0, WeaponInformation.ReloadingTimeInSeconds, FinishReloading);
		BoltReloadCycleTimer = new Timer(0, WeaponInformation.TimeInSecondBetweenShoots, FinishBoltCycle);
	}

	public void CachePlayerShootingController(PlayerShootingController playerShootingController)
	{
		CachedPlayerShootingController = playerShootingController;
	}

	public void UpgradeWeapon()
	{
		WeaponLevel++;
	}

	public void ResetWeaponLevel()
	{
		WeaponLevel = 1;
	}

	public void Shoot()
	{
		if (IsReloading.Value == false)
		{
			EjectBullet();
			BulletLeftInMagazine--;
			IsReloading.SetValue(true);

			if (BulletLeftInMagazine == 0)
			{	
				ReloadingMagazineTimer.StartCounting();
			}
			else
			{
				BoltReloadCycleTimer.StartCounting();
			}
		}
	}

	private void FinishBoltCycle()
    {
		IsReloading.SetValue(false);
	}

	private void FinishReloading()
	{
		IsReloading.SetValue(false);
		BulletLeftInMagazine = WeaponInformation.MagazineCapacity;
	}

    private void EjectBullet()
	{
		if (PoolManager.Instance != null)
		{
			BasePoolObject poolObject = PoolManager.Instance.GetPoolObject(bulletTag, BulletSpawnPoint.position, BulletSpawnPoint.rotation);

			Bullet bullet = poolObject as Bullet;

			bullet.OnKillTarget += CachedPlayerShootingController.NotifyKillEnemy;
		}
	}

	#endregion

	#region ENUMS

	#endregion
}
