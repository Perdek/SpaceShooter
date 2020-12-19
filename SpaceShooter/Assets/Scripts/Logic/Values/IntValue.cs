using System;

public class IntValue : NotifiableValue<int>
{
    #region MEMBERS

    public event Action<int> OnRemoveValue = delegate { };
    public event Action<int> OnAddValue = delegate { };

    #endregion

    #region PROPERTIES

    #endregion

    #region METHODS

    public IntValue(int value)
    {
        SetValue(value);
    }

    public void AddValue(int addedValue)
    {
        SetValueSilent(Value + addedValue);
        OnAddValue(addedValue);
    }

    public void RemoveValue(int removedValue)
    {
        SetValueSilent(Value - removedValue);
        OnRemoveValue(removedValue);
    }    

    #endregion
}
