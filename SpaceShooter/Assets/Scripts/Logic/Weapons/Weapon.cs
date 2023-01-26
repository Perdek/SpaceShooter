using System;
using Managers.GameManagers;
using UnityEngine;
using UnityEngine.Serialization;

[Serializable]
public class Weapon
{
    #region FIELDS

    public event Action<int> OnUpgradeWeapon = delegate { };

    [FormerlySerializedAs("bulletTag")] [SerializeField]
    private SpawnableObjectsTagsEnum _bulletTag = SpawnableObjectsTagsEnum.PLAYER_BULLET_TAG;
    [FormerlySerializedAs("weaponInformation")] [SerializeField]
    private WeaponInformation _weaponInformation = null;
    
    private IPoolManager _poolManager;
    private IUpdateManager _updateManager;

    private Transform _bulletSpawnPoint;
    private Timer _reloadingMagazineTimer;
    private Timer _boltReloadCycleTimer;
    private BoolValue _isBoltReloadCycle = new BoolValue(false);
    private PlayerShootingController _cachedPlayerShootingController; //TODO is necessary?

    #endregion

    #region PROPERTIES

    public WeaponInformation WeaponInformation => _weaponInformation;
    public BoolValue IsReloadingMagazine { get; private set; } = new BoolValue(false);
    public IntValue BulletLeftInMagazine { get; private set; } = new IntValue();
    public IntValue WeaponLevel { get; set; } = new IntValue(0);

    #endregion

    #region METHODS

    public void InjectDependencies(IUpdateManager updateManager, IPoolManager poolManager)
    {
        _poolManager = poolManager;
        _updateManager = updateManager;
    }

    public int GetCurrentUpgradingCostCurve() => (int)WeaponInformation.UpgradingCostCurve.Evaluate(WeaponLevel.Value);

    public void InitializeBulletTransform(Transform bulletSpawnPointTransform)
    {
        _bulletSpawnPoint = bulletSpawnPointTransform;
    }

    public void InitializeWeapon()
    {
        BulletLeftInMagazine.SetValue((int)WeaponInformation.MagazineCapacityCurve.Evaluate(WeaponLevel.Value));
        _reloadingMagazineTimer = new Timer(_updateManager, 0, GetCurrentReloadingTimeInSeconds(), FinishReloading);
        _boltReloadCycleTimer = new Timer(_updateManager, 0, GetCurrentTimeInSecondBetweenShoots(), FinishBoltCycle);
    }

    public bool IsLastLevel()
    {
        return WeaponLevel.Value == Constants.MAX_WEAPONS_LEVEL;
    }

    public void CachePlayerShootingController(PlayerShootingController playerShootingController)
    {
        _cachedPlayerShootingController = playerShootingController;
    }

    public void ClearReload()
    {
        BulletLeftInMagazine.SetValue(GetCurrentMagazineCapacity());

        _reloadingMagazineTimer?.EndCounting();
        _boltReloadCycleTimer?.EndCounting();

        IsReloadingMagazine.SetValue(false);
        _isBoltReloadCycle.SetValue(false);
    }

    public void UpgradeWeapon()
    {
        OnUpgradeWeapon(GetCurrentUpgradingCostCurve());
        WeaponLevel.AddValue(1);
    }

    public void ResetWeaponLevel()
    {
        WeaponLevel.SetValue(1);
    }

    public void Shoot()
    {
        if (IsReloadingMagazine.Value == false && _isBoltReloadCycle.Value == false)
        {
            EjectBullet();
            BulletLeftInMagazine.RemoveValue(1);

            if (BulletLeftInMagazine.Value == 0)
            {
                _reloadingMagazineTimer.StartCounting();
                IsReloadingMagazine.SetValue(true);
            }
            else
            {
                _boltReloadCycleTimer.StartCounting();
                _isBoltReloadCycle.SetValue(true);
            }
        }
    }

    public bool IsWeaponAvailable()
    {
        return WeaponLevel.Value > 0;
    }

    private void FinishBoltCycle()
    {
        _isBoltReloadCycle.SetValue(false);
    }

    private void FinishReloading()
    {
        IsReloadingMagazine.SetValue(false);
        BulletLeftInMagazine.SetValue(GetCurrentMagazineCapacity());
    }

    private void EjectBullet()
    {
        IBasePoolObject poolObject = _poolManager.GetPoolObject(_bulletTag, _bulletSpawnPoint.position, _bulletSpawnPoint.rotation);

        Bullet bullet = poolObject as Bullet;

        bullet.OnKillTarget += _cachedPlayerShootingController.NotifyKillEnemy;
    }

    private int GetCurrentReloadingTimeInSeconds() => (int)WeaponInformation.ReloadingTimeInSecondsCurve.Evaluate(WeaponLevel.Value);
    private int GetCurrentTimeInSecondBetweenShoots() => (int)WeaponInformation.TimeInSecondBetweenShootsCurve.Evaluate(WeaponLevel.Value);

    private int GetCurrentMagazineCapacity() => (int)WeaponInformation.MagazineCapacityCurve.Evaluate(WeaponLevel.Value);
    private int GetCurrentDamageCurve() => (int)WeaponInformation.DamageCurve.Evaluate(WeaponLevel.Value);

    #endregion

    #region ENUMS

    #endregion
}
