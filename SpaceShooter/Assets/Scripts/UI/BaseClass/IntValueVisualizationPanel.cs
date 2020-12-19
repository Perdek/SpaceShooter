using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class IntValueVisualizationPanel : NotifiableValueVisualization<IntValue, int>
{
    #region MEMBERS

    #endregion

    #region PROPERTIES


    #endregion

    #region METHODS

    public override void AttachEvents()
    {
        ValueReference.OnValueSet += RefreshPanel;
        ValueReference.OnAddValue += AddElement;
        ValueReference.OnRemoveValue += RemoveLastElement;
    }

    public override void DetachEvents()
    {
        if (ValueReference != null)
        {
            ValueReference.OnValueSet -= RefreshPanel;
            ValueReference.OnAddValue -= AddElement;
            ValueReference.OnRemoveValue -= RemoveLastElement;
        }
    }

    public override void UpdateVisualization(int value)
    {
        RefreshPanel(ValueReference.Value);
    }

    public void RefreshPanel(int playerHP)
    {
        ClearPanel();
        FillPanel(playerHP);
    }

    private void FillPanel(int value)
    {
        for (int i = 0; i < value; i++)
        {
            AddElement();
        }
    }

    #endregion
}