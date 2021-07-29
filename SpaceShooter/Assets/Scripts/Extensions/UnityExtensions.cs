using System;
using System.Collections.Generic;
using UnityEngine;

public static class UnityExtensions
{
    #region MEMBERS

    #endregion

    #region PROPERTIES

    #endregion

    #region METHODS

    public static void SetActiveOptimized(this GameObject target, bool state)
    {
        if (target != null && target.activeSelf != state)
        {
            target.SetActive(state);
        }
    }

    public static void ToggleActivity(this GameObject target)
    {
        if (target != null)
        {
            if (target.activeSelf == true)
            {
                target.SetActive(false);
            }
            else
            {
                target.SetActive(true);
            }
        }
    }

    public static T GetFirstElement<T>(this List<T> list)
    {
        try
        {
            return list[0];
        }
        catch (NullReferenceException exception)
        {
            throw new NullReferenceException("collection is null ", exception);
        }
        catch (ArgumentOutOfRangeException exception)
        {
            throw new ArgumentOutOfRangeException("index parameter is out of range.", exception);
        }
        catch (Exception exception)
        {
            throw new Exception(exception.Message);
        }
    }

    public static T GetLastElement<T>(this List<T> list) where T : class
    {
        if (list.Count == 0)
        {
            return null;
        }

        return list[list.Count - 1];
    }

    #endregion
}
