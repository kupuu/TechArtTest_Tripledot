using UnityEditor;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.UI;

[CustomEditor(typeof(AnimatedToggle))]
[CanEditMultipleObjects]
public class AnimatedToggleEditor : ToggleEditor
{
    private SerializedProperty toggleDotProp;
    private SerializedProperty dotCurveProp;
    private AnimatedToggle animtedToggle;


    protected override void OnEnable()
    {
        base.OnEnable();

        toggleDotProp = serializedObject.FindProperty("toggleDot");
        dotCurveProp = serializedObject.FindProperty("dotCurve");
        animtedToggle = (AnimatedToggle)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        serializedObject.Update();

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Animated Dot", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(toggleDotProp, new GUIContent("Dot"));
        EditorGUILayout.PropertyField(dotCurveProp, new GUIContent("Dot Curve"));
        animtedToggle.UpdateDotImmediate();
        serializedObject.ApplyModifiedProperties();
    }
}
