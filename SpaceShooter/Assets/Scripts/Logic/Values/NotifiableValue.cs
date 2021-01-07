using System;

public abstract class NotifiableValue<T>
{
    #region MEMBERS

    public event Action<T> OnBeforeValueSet = delegate { };
    public event Action<T> OnValueSet = delegate { };

    private T value;

    #endregion

    #region PROPERTIES

    public T Value {
        get => value;
        private set => this.value = value;
    }

    #endregion

    #region METHODS

    public void SetValue(T newValue)
    {
        OnBeforeValueSet(Value);
        Value = newValue;
        OnValueSet(Value);
    }

    public void SetValueSilent(T newValue)
    {
        Value = newValue;
    }

    #endregion
}
