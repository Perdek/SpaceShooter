using Managers.GameManagers;
using System;
using UnityEngine;
using Zenject;

[System.Serializable]
public class Weapon
{
    #region FIELDS

    public event Action<int> OnUgradeWeapon = delegate { };

    [SerializeField]
    private SpawnableObjectsTagsEnum bulletTag = SpawnableObjectsTagsEnum.PLAYER_BULLET_TAG;
    [SerializeField]
    private WeaponInformation weaponInformation = null;
    
    private IPoolManager poolManager;
    private IUpdateManager updateManager;

    #endregion

    #region PROPERTIES

    public WeaponInformation WeaponInformation => weaponInformation;
    public BoolValue IsReloadingMagazine { get; private set; } = new BoolValue(false);
    public IntValue BulletLeftInMagazine { get; private set; } = new IntValue();
    public IntValue WeaponLevel { get; set; } = new IntValue(0);
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

    public void InjectDependencies(IUpdateManager updateManager, IPoolManager poolManager)
    {
        this.poolManager = poolManager;
        this.updateManager = updateManager;
    }

    public int GetCurrentUpgradingCostCurve() => (int)WeaponInformation.UpgradingCostCurve.Evaluate(WeaponLevel.Value);

    public void InitializeBulletTransform(Transform bulletSpawnPointTransform)
    {
        BulletSpawnPoint = bulletSpawnPointTransform;
    }

    public void InitializeWeapon()
    {
        BulletLeftInMagazine.SetValue((int)WeaponInformation.MagazineCapacityCurve.Evaluate(WeaponLevel.Value));
        ReloadingMagazineTimer = new Timer(updateManager, 0, GetCurrentReloadingTimeInSeconds(), FinishReloading);
        BoltReloadCycleTimer = new Timer(updateManager, 0, GetCurrentTimeInSecondBetweenShoots(), FinishBoltCycle);
    }

    public bool IsLastLevel()
    {
        return WeaponLevel.Value == Constants.MAX_WEAPONS_LEVEL;
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

    public bool IsWeaponAvailable()
    {
        return WeaponLevel.Value > 0;
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
        IBasePoolObject poolObject = poolManager.GetPoolObject(bulletTag, BulletSpawnPoint.position, BulletSpawnPoint.rotation);

        Bullet bullet = poolObject as Bullet;

        bullet.OnKillTarget += CachedPlayerShootingController.NotifyKillEnemy;
    }

    private int GetCurrentReloadingTimeInSeconds() => (int)WeaponInformation.ReloadingTimeInSecondsCurve.Evaluate(WeaponLevel.Value);
    private int GetCurrentTimeInSecondBetweenShoots() => (int)WeaponInformation.TimeInSecondBetweenShootsCurve.Evaluate(WeaponLevel.Value);

    private int GetCurrentMagazineCapacity() => (int)WeaponInformation.MagazineCapacityCurve.Evaluate(WeaponLevel.Value);
    private int GetCurrentDamageCurve() => (int)WeaponInformation.DamageCurve.Evaluate(WeaponLevel.Value);

    #endregion

    #region ENUMS

    #endregion
}
