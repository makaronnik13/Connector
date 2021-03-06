﻿using UnityEditor;
using UnityEngine;

// IngredientDrawer
[CustomPropertyDrawer(typeof(CombinationLink))]
public class CombinationLinkDrawer : PropertyDrawer
{
    // Draw the property inside the given rect
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        // Using BeginProperty / EndProperty on the parent property means that
        // prefab override logic works on the entire property.
        EditorGUI.BeginProperty(position, label, property);

        // Draw label
        position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

        // Don't make child fields be indented
        var indent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;

        // Calculate rects
        var amountRect = new Rect(position.x, position.y, position.width/2-1, position.height);
        var aimRect = new Rect(position.x+ position.width/2+3, position.y, position.width/2-1, position.height);
        var newsRect = new Rect(position.x , position.y+EditorGUIUtility.singleLineHeight+2, position.width, position.height);


        // Draw fields - passs GUIContent.none to each so they are drawn without labels
        EditorGUI.PropertyField(amountRect, property.FindPropertyRelative("person"), GUIContent.none);
        EditorGUI.PropertyField(aimRect, property.FindPropertyRelative("endPoint"), GUIContent.none);
        EditorGUI.PropertyField(newsRect, property.FindPropertyRelative("connectionNews"), GUIContent.none);

        // Set indent back to what it was
        EditorGUI.indentLevel = indent;

        EditorGUI.EndProperty();
    }
}