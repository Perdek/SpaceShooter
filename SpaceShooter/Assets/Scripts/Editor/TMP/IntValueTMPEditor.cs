#if UNITY_EDITOR

using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(IntValueTMP)), CanEditMultipleObjects]
public class IntValueTMPEditor : TMPro.EditorUtilities.TMP_EditorPanelUI
{
    #region MEMBERS

    private IntValueTMP intValueTMP = default;
    private SerializedProperty usePrefix = default;
    private SerializedProperty prefixValue = default;

    #endregion

    #region PROPERTIES

    #endregion

    #region METHODS

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (intValueTMP == null)
        {
            intValueTMP = (IntValueTMP)target;
        }

        usePrefix = serializedObject.FindProperty("usePrefix");
        prefixValue = serializedObject.FindProperty("prefixValue");

        ShowBasicOptions();
    }

    private void ShowBasicOptions()
    {
        serializedObject.UpdateIfRequiredOrScript();

        EditorGUILayout.PropertyField(usePrefix, new GUIContent("Use Prefix"));
        EditorGUILayout.PropertyField(prefixValue, new GUIContent("Prefix Value"));

        serializedObject.ApplyModifiedProperties();
    }

    #endregion
}

#endif