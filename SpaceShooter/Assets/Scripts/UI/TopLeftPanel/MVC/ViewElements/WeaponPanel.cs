using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class WeaponPanel
{
    #region MEMBERS

    [SerializeField]
    private Image weaponIconImage = null;
    [SerializeField]
    private TMPro.TMP_Text reloadText = null;
    [SerializeField]
    private TMPro.TMP_Text weaponNameText = null;

    #endregion

    #region PROPERTIES

    private TMPro.TMP_Text ReloadText => reloadText;
    private Image WeaponIconImage => weaponIconImage;
    private TMP_Text WeaponNameText => weaponNameText;

    private WeaponValue PlayerCurrentWeapon {
        get;
        set;
    }

    #endregion

    #region FUNCTIONS

    public void RegisterWeapon(WeaponValue weapon)
    {
        UnregisterWeapon();

        PlayerCurrentWeapon = weapon;

        weapon.OnValueSet += RefreshView;

        RefreshView(weapon.Value);
        RefreshReloadingText(weapon.Value.IsReloadingMagazine.Value);

        AttachEvents();
    }

    private void RefreshView(Weapon weapon)
    {
        WeaponIconImage.sprite = weapon.WeaponInformation.BulletSprite;
        WeaponNameText.text = weapon.WeaponInformation.WeaponName;
    }

    private void UnregisterWeapon()
    {
        if (PlayerCurrentWeapon != null)
        {
            PlayerCurrentWeapon.OnValueSet -= RefreshView;
        }

        DetachEvents();
    }

    private void AttachEvents()
    {
        PlayerCurrentWeapon.Value.IsReloadingMagazine.OnValueSet += RefreshReloadingText;
        PlayerCurrentWeapon.Value.BulletLeftInMagazine.OnValueSet += RefreshMagazineText;
        PlayerCurrentWeapon.Value.BulletLeftInMagazine.OnRemoveValue += RefreshRemoveMagazineText;
    }

    private void DetachEvents()
    {
        if (PlayerCurrentWeapon != null)
        {
            PlayerCurrentWeapon.Value.IsReloadingMagazine.OnValueSet -= RefreshReloadingText;
            PlayerCurrentWeapon.Value.BulletLeftInMagazine.OnValueSet -= RefreshMagazineText;
            PlayerCurrentWeapon.Value.BulletLeftInMagazine.OnRemoveValue -= RefreshRemoveMagazineText;
        }
    }

    private void RefreshMagazineText(int bulletLeft)
    {
        ReloadText.text = bulletLeft.ToString();
    }

    private void RefreshRemoveMagazineText(int removedBullets)
    {
        ReloadText.text = PlayerCurrentWeapon.Value.BulletLeftInMagazine.Value.ToString();
    }

    private void RefreshReloadingText(bool isReloading)
    {
        if (isReloading == true)
        {
            ReloadText.text = "Reloading";
        }
        else
        {
            ReloadText.text = PlayerCurrentWeapon.Value.BulletLeftInMagazine.Value.ToString();
        }
    }

    #endregion

    #region CLASS_ENUMS

    #endregion
}
