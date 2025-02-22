using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(OverlapDamageCaster))]
public class OverlapDamageCasterEditor : Editor
{
    private SerializedProperty overlapTypeProp;
    private SerializedProperty damageBoxSizeProp;
    private SerializedProperty damageRadiusProp;

    private void OnEnable()
    {
        overlapTypeProp = serializedObject.FindProperty("overlapType");
        damageBoxSizeProp = serializedObject.FindProperty("damageBoxSize");
        damageRadiusProp = serializedObject.FindProperty("damageRadius");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        SerializedProperty iterator = serializedObject.GetIterator();
        bool enterChildren = true;
        bool showDamageBox = false;
        bool showDamageRadius = false;
        OverlapType currentOverlapType = OverlapType.Circle;

        while (iterator.NextVisible(enterChildren))
        {
            enterChildren = false;

            if (iterator.name == overlapTypeProp.name)
            {
                EditorGUILayout.PropertyField(iterator, true);
                currentOverlapType = (OverlapType)iterator.enumValueIndex;
                showDamageBox = currentOverlapType == OverlapType.Box;
                showDamageRadius = currentOverlapType == OverlapType.Circle;
            }
            else if (iterator.name == damageBoxSizeProp.name)
            {
                if (showDamageBox)
                    EditorGUILayout.PropertyField(iterator, true);
            }
            else if (iterator.name == damageRadiusProp.name)
            {
                if (showDamageRadius)
                    EditorGUILayout.PropertyField(iterator, true);
            }
            else
            {
                EditorGUILayout.PropertyField(iterator, true);
            }
        }

        serializedObject.ApplyModifiedProperties();
    }
}