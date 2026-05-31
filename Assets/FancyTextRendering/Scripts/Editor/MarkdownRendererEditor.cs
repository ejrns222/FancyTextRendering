using LogicUI.FancyTextRendering;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(MarkdownRenderer))]
public class MarkdownRendererEditor : Editor
{
    SerializedProperty sourceModeProperty;
    SerializedProperty sourceProperty;
    SerializedProperty sourceAssetProperty;
    SerializedProperty renderSettingsProperty;

    private void OnEnable()
    {
        sourceModeProperty = serializedObject.FindProperty("_SourceMode");
        sourceProperty = serializedObject.FindProperty("_Source");
        sourceAssetProperty = serializedObject.FindProperty("_SourceAsset");
        renderSettingsProperty = serializedObject.FindProperty("RenderSettings");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(sourceModeProperty, new GUIContent("Source Mode"));

        MarkdownRenderer.SourceMode sourceMode = (MarkdownRenderer.SourceMode)sourceModeProperty.enumValueIndex;
        if (sourceMode == MarkdownRenderer.SourceMode.TextAsset)
        {
            EditorGUILayout.PropertyField(sourceAssetProperty, new GUIContent("Source Asset"));
        }
        else
        {
            EditorGUILayout.PropertyField(sourceProperty, new GUIContent("Source"));
        }

        EditorGUILayout.Space();
        EditorGUILayout.PropertyField(renderSettingsProperty, true);

        serializedObject.ApplyModifiedProperties();
    }
}