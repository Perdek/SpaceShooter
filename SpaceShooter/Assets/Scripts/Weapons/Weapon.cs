using System;
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

    public WeaponInformation WeaponInformation => weaponInformation;
    public BoolValue IsReloadingMagazine { get; private set; } = new BoolValue(false);
    public IntValue BulletLeftInMagazine { get; private set; } = new IntValue();
    private Transform BulletSpawnPoint { get; set; }
    private Timer ReloadingMagazineTimer { get; set; }
    private Timer BoltReloadCycleTimer { get; set; }
    private int WeaponLevel { get; set; } = 1;
    private BoolValue IsBoltReloadCycle { get; set; } = new BoolValue(false);

    private PlayerShootingController CachedPlayerShootingController {
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
        BulletLeftInMagazine.SetValue(WeaponInformation.MagazineCapacity);

        ReloadingMagazineTimer = new Timer(0, WeaponInformation.ReloadingTimeInSeconds, FinishReloading);
        BoltReloadCycleTimer = new Timer(0, WeaponInformation.TimeInSecondBetweenShoots, FinishBoltCycle);
    }

    public void CachePlayerShootingController(PlayerShootingController playerShootingController)
    {
        CachedPlayerShootingController = playerShootingController;
    }

    public void ClearReload()
    {
        BulletLeftInMagazine.SetValue(WeaponInformation.MagazineCapacity);

        ReloadingMagazineTimer?.EndCounting();
        BoltReloadCycleTimer?.EndCounting();

        IsReloadingMagazine.SetValue(false);
        IsBoltReloadCycle.SetValue(false);
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
        if (IsReloadingMagazine.Value == false && IsBoltReloadCycle.Value == false)
        {
            EjectBullet();
            BulletLeftInMagazine.RemoveValue(1);

            if (BulletLeftInMagazine.Value == 0)
            {
                ReloadingMagazineTimer.StartCounting();
                IsReloadingMagazine.SetValue(true);
            }
            else
            {
                BoltReloadCycleTimer.StartCounting();
                IsBoltReloadCycle.SetValue(true);
            }
        }
    }

    private void FinishBoltCycle()
    {
        IsBoltReloadCycle.SetValue(false);
    }

    private void FinishReloading()
    {
        IsReloadingMagazine.SetValue(false);
        BulletLeftInMagazine.SetValue(WeaponInformation.MagazineCapacity);
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
