using System;
using System.Collections.Generic;
using Managers.GameManagers;
using Managers.LevelManagers;
using UnityEngine;

[Serializable]
public class PlayerShootingController
{
    #region FIELDS

    public event Action<EnemyInformation> OnKillEnemy = delegate { };

    [SerializeField] private Transform bulletSpawnPointTransform = null;
    [SerializeField] private List<Weapon> weapons = new List<Weapon>();

    private IKeyboardManager _keyboardManager;
    private LevelEventsCommunicator _levelEventsCommunicator;

    #endregion

    #region PROPERTIES

    public Transform BulletSpawnPointTransform => bulletSpawnPointTransform;
    public List<Weapon> Weapons => weapons;

    public WeaponValue ActiveWeapon { get; private set; }

    private List<Guid> KeysIds { get; set; } = new List<Guid>();

    private int ActiveWeaponIndex { get; set; } = 0;

    #endregion

    #region METHODS

    public void InjectDependencies(IKeyboardManager keyboardManager, IUpdateManager updateManager,
        IPoolManager poolManager, LevelEventsCommunicator levelEventsCommunicator)
    {
        _keyboardManager = keyboardManager;
        _levelEventsCommunicator = levelEventsCommunicator;

        for (int i = 0; i < Weapons.Count; i++)
        {
            Weapons[i].InjectDependencies(updateManager, poolManager);
        }
    }

    public void Initialize()
    {
        InitializeWeapons();
        ChooseDefaultWeapon();
    }

    public void AttachEventForUpdateWeapon(Action<int> onUpdate)
    {
        for (int i = 0; i < Weapons.Count; i++)
        {
            Weapons[i].OnUgradeWeapon += onUpdate;
        }
    }

    public void ClearWeaponsReload()
    {
        for (int i = 0; i < Weapons.Count; i++)
        {
            Weapons[i].ClearReload();
        }
    }

    public void ResetShooting()
    {
        InitializeKeys();
    }

    public void NotifyKillEnemy(EnemyInformation killedEnemyInformation)
    {
        OnKillEnemy(killedEnemyInformation);
    }

    public void Shoot()
    {
        ActiveWeapon.Value.Shoot();
    }

    private void InitializeKeys()
    {
        _levelEventsCommunicator.OnLevelStart += AttachKeysForShooting;
        _levelEventsCommunicator.OnLevelEnd += DetachKeysForShooting;
        _levelEventsCommunicator.OnLevelEnd += DetachKeysForShooting;
    }

    private void InitializeWeapons()
    {
        for (int i = 0; i < Weapons.Count; i++)
        {
            Weapons[i].InitializeBulletTransform(BulletSpawnPointTransform);
            Weapons[i].CachePlayerShootingController(this);
            Weapons[i].InitializeWeapon();
        }
    }

    private void AttachKeysForShooting()
    {
        KeysIds.Add(_keyboardManager.AddKey(KeyCode.Space, Shoot, KeyInput.KeyStateEnum.KEY_PRESSED_DOWN,
            KeyInput.CheckingModeEnum.DISJUNCTION));
        KeysIds.Add(_keyboardManager.AddKey(KeyCode.E, NextWeapon, KeyInput.KeyStateEnum.KEY_RELEASED,
            KeyInput.CheckingModeEnum.DISJUNCTION));
        KeysIds.Add(_keyboardManager.AddKey(KeyCode.Q, PrevWeapon, KeyInput.KeyStateEnum.KEY_RELEASED,
            KeyInput.CheckingModeEnum.DISJUNCTION));
    }

    private void DetachKeysForShooting()
    {
        for (int i = KeysIds.Count - 1; i >= 0; i--)
        {
            _keyboardManager.RemoveKey(KeysIds[i]);
            KeysIds.RemoveAt(i);
        }
    }

    private void NextWeapon()
    {
        bool nextWeaponFounded = false;
        int nextWeaponIndex = 0;

        do
        {
            nextWeaponIndex = Weapons.Count == nextWeaponIndex + 1 ? nextWeaponIndex = 0 : nextWeaponIndex + 1;
            Weapon weaponProposition = Weapons[nextWeaponIndex];

            if (weaponProposition.IsWeaponAvailable() == true || ActiveWeaponIndex == nextWeaponIndex)
            {
                nextWeaponFounded = true;
                ActiveWeaponIndex = nextWeaponIndex;
                ActiveWeapon.SetValue(weaponProposition);
            }
        } while (nextWeaponFounded == false);
    }

    private void PrevWeapon()
    {
        bool prevWeaponFounded = false;
        int prevWeaponIndex = 0;

        do
        {
            prevWeaponIndex = prevWeaponIndex == 0 ? prevWeaponIndex = Weapons.Count - 1 : prevWeaponIndex - 1;
            Weapon weaponProposition = Weapons[prevWeaponIndex];

            if (weaponProposition.IsWeaponAvailable() == true || ActiveWeaponIndex == prevWeaponIndex)
            {
                prevWeaponFounded = true;
                ActiveWeaponIndex = prevWeaponIndex;
                ActiveWeapon.SetValue(weaponProposition);
            }
        } while (prevWeaponFounded == false);
    }

    private void ChooseDefaultWeapon()
    {
        ActiveWeaponIndex = 0;
        ActiveWeapon = new WeaponValue(Weapons[ActiveWeaponIndex]);
        ActiveWeapon.Value.WeaponLevel.AddValue(1);
    }

    #endregion

    #region ENUMS

    #endregion
}