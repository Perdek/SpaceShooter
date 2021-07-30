using System;
using UnityEngine;

[System.Serializable]
public class Weapon
{
    #region FIELDS

    public event Action<int> OnUgradeWeapon = delegate { };

    [SerializeField]
    private TagManager.TagsEnum bulletTag = TagManager.TagsEnum.PLAYER_BULLET_TAG;
    [SerializeField]
    private WeaponInformation weaponInformation = null;

    #endregion

    #region PROPERTIES

    public WeaponInformation WeaponInformation => weaponInformation;
    public BoolValue IsReloadingMagazine { get; private set; } = new BoolValue(false);
    public IntValue BulletLeftInMagazine { get; private set; } = new IntValue();
    public IntValue WeaponLevel { get; set; } = new IntValue(1);
    private Transform BulletSpawnPoint { get; set; }
    private Timer ReloadingMagazineTimer { get; set; }
    private Timer BoltReloadCycleTimer { get; set; }
    private BoolValue IsBoltReloadCycle { get; set; } = new BoolValue(false);


    private PlayerShootingController CachedPlayerShootingController {
        get;
        set;
    }

    #endregion

    #region METHODS

    public int GetCurrentUpgradingCostCurve() => (int)WeaponInformation.UpgradingCostCurve.Evaluate(WeaponLevel.Value);

    public void InitializeBulletTransform(Transform bulletSpawnPointTransform)
    {
        BulletSpawnPoint = bulletSpawnPointTransform;
    }

    public void InitializeWeapon()
    {
        BulletLeftInMagazine.SetValue((int)WeaponInformation.MagazineCapacityCurve.Evaluate(WeaponLevel.Value));

        ReloadingMagazineTimer = new Timer(0, GetCurrentReloadingTimeInSeconds(), FinishReloading);
        BoltReloadCycleTimer = new Timer(0, GetCurrentTimeInSecondBetweenShoots(), FinishBoltCycle);
    }

    public void CachePlayerShootingController(PlayerShootingController playerShootingController)
    {
        CachedPlayerShootingController = playerShootingController;
    }

    public void ClearReload()
    {
        BulletLeftInMagazine.SetValue(GetCurrentMagazineCapacity());

        ReloadingMagazineTimer?.EndCounting();
        BoltReloadCycleTimer?.EndCounting();

        IsReloadingMagazine.SetValue(false);
        IsBoltReloadCycle.SetValue(false);
    }

    public void UpgradeWeapon()
    {
        OnUgradeWeapon(GetCurrentUpgradingCostCurve());
        WeaponLevel.AddValue(1);
    }

    public void ResetWeaponLevel()
    {
        WeaponLevel.SetValue(1);
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
        BulletLeftInMagazine.SetValue(GetCurrentMagazineCapacity());
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

    private int GetCurrentReloadingTimeInSeconds() => (int)WeaponInformation.ReloadingTimeInSecondsCurve.Evaluate(WeaponLevel.Value);
    private int GetCurrentTimeInSecondBetweenShoots() => (int)WeaponInformation.TimeInSecondBetweenShootsCurve.Evaluate(WeaponLevel.Value);
    private int GetCurrentMagazineCapacity() => (int)WeaponInformation.MagazineCapacityCurve.Evaluate(WeaponLevel.Value);
    private int GetCurrentDamageCurve() => (int)WeaponInformation.DamageCurve.Evaluate(WeaponLevel.Value);

    #endregion

    #region ENUMS

    #endregion
}
