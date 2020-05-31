using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon Information", menuName = "Weapon Information")]
public class WeaponInformation : ScriptableObject
{
    #region FIELDS

    [SerializeField]
    private int magazineCapacity = 5;
    [SerializeField]
    private float timeInSecondBetweenShoots = 1f;
    [SerializeField]
    private float reloadingTimeInSeconds = 3f;
    [SerializeField]
    private int damage = 10;
    [SerializeField]
    private Sprite bulletSprite = null;

    #endregion

    #region PROPERTIES

    public float ReloadingTimeInSeconds => reloadingTimeInSeconds;
    public float TimeInSecondBetweenShoots => timeInSecondBetweenShoots;
    public int MagazineCapacity => magazineCapacity;
    public int Damage => damage;
    public Sprite BulletSprite => bulletSprite;

    #endregion

    #region METHODS

    #endregion

    #region ENUMS

    #endregion
}
