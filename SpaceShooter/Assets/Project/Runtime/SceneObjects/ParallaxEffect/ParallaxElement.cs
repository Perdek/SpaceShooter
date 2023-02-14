using System;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxElement : MonoBehaviour
{
    #region MEMBERS

    [SerializeField]
    private SpriteRenderer spriteReference;

    #endregion

    #region PROPERTIES

    public float SpriteHeight => spriteReference.bounds.size.y;

    #endregion

    #region METHDOS

    #endregion
}
