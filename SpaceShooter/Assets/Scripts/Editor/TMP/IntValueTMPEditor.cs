#if UNITY_EDITOR

using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(IntValueTMP)), CanEditMultipleObjects]
public class IntValueTMPEditor : TMPro.EditorUtilities.TMP_EditorPanelUI
{
    #region MEMBERS

    private IntValueTMP _intValueTMP = default;
    private SerializedProperty _usePrefix = default;
    private SerializedProperty _prefixValue = default;

    #endregion

    #region PROPERTIES

    #endregion

    #region METHODS

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (_intValueTMP == null)
        {
            _intValueTMP = (IntValueTMP)target;
        }

        _usePrefix = serializedObject.FindProperty("usePrefix");
        _prefixValue = serializedObject.FindProperty("prefixValue");

        ShowBasicOptions();
    }

    private void ShowBasicOptions()
    {
        serializedObject.UpdateIfRequiredOrScript();

        EditorGUILayout.PropertyField(_usePrefix, new GUIContent("Use Prefix"));
        EditorGUILayout.PropertyField(_prefixValue, new GUIContent("Prefix Value"));

        serializedObject.ApplyModifiedProperties();
    }

    #endregion
}

#endif