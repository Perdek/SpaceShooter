using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

[System.Serializable]
public class IntValueTMP : TextMeshProUGUI
{
    #region MEMBERS

    [SerializeField] private bool usePrefix = false;
    [SerializeField] private string prefixValue = string.Empty;

    #endregion

    #region PROPERTIES

    private IntValue IntValueReference {
        get;
        set;
    }

    #endregion

    #region MEMBERS

    public void RegisterValue(IntValue newIntValue)
    {
        DetachEvents();

        IntValueReference = newIntValue;

        UpdateValue(newIntValue.Value);

        AttachEvents();
    }

    public void UpdateValue(int value)
    {
        text = usePrefix ? prefixValue + value : value.ToString();
    }

    public void AddValue(int value)
    {
        text = usePrefix ? prefixValue + IntValueReference.Value : IntValueReference.Value.ToString();
    }

    public void RemoveValue(int value)
    {
        text = usePrefix ? prefixValue + IntValueReference.Value : IntValueReference.Value.ToString();
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();

        DetachEvents();
    }

    private void AttachEvents()
    {
        if (IntValueReference != null)
        {
            IntValueReference.OnValueSet += UpdateValue;
            IntValueReference.OnAddValue += AddValue;
            IntValueReference.OnRemoveValue += RemoveValue;
        }
    }

    private void DetachEvents()
    {
        if (IntValueReference != null)
        {
            IntValueReference.OnValueSet -= UpdateValue;
            IntValueReference.OnAddValue -= AddValue;
            IntValueReference.OnRemoveValue -= RemoveValue;
        }
    }

    #endregion
}
