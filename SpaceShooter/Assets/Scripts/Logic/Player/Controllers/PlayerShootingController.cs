using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerShootingController
{
    #region FIELDS

    public event Action<EnemyInformation> OnKillEnemy = delegate { };

    [SerializeField]
    private Transform bulletSpawnPointTransform = null;
    [SerializeField]
    private List<Weapon> weapons = new List<Weapon>();

    #endregion

    #region PROPERTIES

    public Transform BulletSpawnPointTransform => bulletSpawnPointTransform;
    public List<Weapon> Weapons => weapons;

    public WeaponValue ActiveWeapon {
        get;
        private set;
    }

    private List<int> KeysIds {
        get;
        set;
    } = new List<int>();

    private int ActiveWeaponIndex {
        get;
        set;
    } = 0;

    #endregion

    #region METHODS

    public void Initialize()
    {
        ChooseFirstWeapon();
        InitializeWeapons();
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
        LevelManager.Instance.OnLevelStart += AttachKeysForShooting;
        LevelManager.Instance.OnLevelEnd += DetachKeysForShooting;
        LevelManager.Instance.OnLevelEnd += DetachKeysForShooting;
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
        KeysIds.Add(KeyboardManager.Instance.AddKey(KeyCode.Space, Shoot, KeyInput.KeyStateEnum.KEY_PRESSED_DOWN, KeyInput.CheckingModeEnum.DISJUNCTION));
        KeysIds.Add(KeyboardManager.Instance.AddKey(KeyCode.E, NextWeapon, KeyInput.KeyStateEnum.KEY_RELEASED, KeyInput.CheckingModeEnum.DISJUNCTION));
        KeysIds.Add(KeyboardManager.Instance.AddKey(KeyCode.Q, PrevWeapon, KeyInput.KeyStateEnum.KEY_RELEASED, KeyInput.CheckingModeEnum.DISJUNCTION));
    }

    private void DetachKeysForShooting()
    {
        for (int i = KeysIds.Count - 1; i >= 0; i--)
        {
            KeyboardManager.Instance.RemoveKey(KeysIds[i]);
            KeysIds.RemoveAt(i);
        }
    }

    private void NextWeapon()
    {
        ActiveWeaponIndex = Weapons.Count == ActiveWeaponIndex + 1 ? ActiveWeaponIndex = 0 : ActiveWeaponIndex + 1;
        ActiveWeapon.SetValue(Weapons[ActiveWeaponIndex]);
    }

    private void PrevWeapon()
    {
        ActiveWeaponIndex = ActiveWeaponIndex == 0 ? ActiveWeaponIndex = Weapons.Count - 1 : ActiveWeaponIndex - 1;
        ActiveWeapon.SetValue(Weapons[ActiveWeaponIndex]);
    }

    private void ChooseFirstWeapon()
    {
        ActiveWeaponIndex = 0;
        ActiveWeapon = new WeaponValue(Weapons[ActiveWeaponIndex]);
    }

    #endregion

    #region ENUMS

    #endregion
}
