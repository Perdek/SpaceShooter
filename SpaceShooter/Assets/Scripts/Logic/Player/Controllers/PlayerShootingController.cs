using System;
using System.Collections.Generic;
using Managers.GameManagers;
using Managers.LevelManagers;
using UnityEngine;
using UnityEngine.Serialization;

[Serializable]
public class PlayerShootingController
{
    #region FIELDS

    [FormerlySerializedAs("bulletSpawnPointTransform")] [SerializeField] private Transform _bulletSpawnPointTransform = null;
    [FormerlySerializedAs("weapons")] [SerializeField] private List<Weapon> _weapons = new List<Weapon>();

    private List<Guid> _keysIds = new List<Guid>();
    private int _activeWeaponIndex;

    private IKeyboardManager _keyboardManager;
    private LevelEventsCommunicator _levelEventsCommunicator;
    private IInputManager _inputManager;

    #endregion

    #region PROPERTIES

    public WeaponValue ActiveWeapon { get; private set; }
    public List<Weapon> Weapons => _weapons;

    #endregion

    #region METHODS

    public void InjectDependencies(IKeyboardManager keyboardManager, IUpdateManager updateManager, IPoolManager poolManager, LevelEventsCommunicator levelEventsCommunicator, IInputManager inputManager)
    {
        _keyboardManager = keyboardManager;
        _levelEventsCommunicator = levelEventsCommunicator;
        _inputManager = inputManager;

        for (int i = 0; i < _weapons.Count; i++)
        {
            _weapons[i].InjectDependencies(updateManager, poolManager);
        }
    }

    public void Initialize()
    {
        InitializeWeapons();
        ChooseDefaultWeapon();
    }

    public void AttachEventForUpdateWeapon(Action<int> onUpdate)
    {
        for (int i = 0; i < _weapons.Count; i++)
        {
            _weapons[i].OnUpgradeWeapon += onUpdate;
        }
    }

    public void ClearWeaponsReload()
    {
        for (int i = 0; i < _weapons.Count; i++)
        {
            _weapons[i].ClearReload();
        }
    }

    public void ResetShooting()
    {
        InitializeKeys();
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
        for (int i = 0; i < _weapons.Count; i++)
        {
            _weapons[i].InitializeBulletTransform(_bulletSpawnPointTransform);
            _weapons[i].InitializeWeapon();
        }
    }

    private void AttachKeysForShooting()
    {
        _keysIds.Add(_keyboardManager.AddKey(_inputManager.KeyCodeShoot, Shoot, KeyInput.KeyStateEnum.KEY_PRESSED_DOWN, KeyInput.CheckingModeEnum.DISJUNCTION));
        _keysIds.Add(_keyboardManager.AddKey(_inputManager.KeyCodeNextWeapon, NextWeapon, KeyInput.KeyStateEnum.KEY_RELEASED, KeyInput.CheckingModeEnum.DISJUNCTION));
        _keysIds.Add(_keyboardManager.AddKey(_inputManager.KeyCodePrevWeapon, PrevWeapon, KeyInput.KeyStateEnum.KEY_RELEASED, KeyInput.CheckingModeEnum.DISJUNCTION));
    }

    private void DetachKeysForShooting()
    {
        for (int i = _keysIds.Count - 1; i >= 0; i--)
        {
            _keyboardManager.RemoveKey(_keysIds[i]);
            _keysIds.RemoveAt(i);
        }
    }

    private void NextWeapon()
    {
        bool nextWeaponFounded = false;
        int nextWeaponIndex = 0;

        do
        {
            nextWeaponIndex = _weapons.Count == nextWeaponIndex + 1 ? nextWeaponIndex = 0 : nextWeaponIndex + 1;
            Weapon weaponProposition = _weapons[nextWeaponIndex];

            if (weaponProposition.IsWeaponAvailable() == true || _activeWeaponIndex == nextWeaponIndex)
            {
                nextWeaponFounded = true;
                _activeWeaponIndex = nextWeaponIndex;
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
            prevWeaponIndex = prevWeaponIndex == 0 ? prevWeaponIndex = _weapons.Count - 1 : prevWeaponIndex - 1;
            Weapon weaponProposition = _weapons[prevWeaponIndex];

            if (weaponProposition.IsWeaponAvailable() == true || _activeWeaponIndex == prevWeaponIndex)
            {
                prevWeaponFounded = true;
                _activeWeaponIndex = prevWeaponIndex;
                ActiveWeapon.SetValue(weaponProposition);
            }
        } while (prevWeaponFounded == false);
    }

    private void ChooseDefaultWeapon()
    {
        _activeWeaponIndex = 0;
        ActiveWeapon = new WeaponValue(_weapons[_activeWeaponIndex]);
        ActiveWeapon.Value.WeaponLevel.AddValue(1);
    }

    #endregion

    #region ENUMS

    #endregion
}