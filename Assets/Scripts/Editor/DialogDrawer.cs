using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

// IngredientDrawer
[CustomPropertyDrawer(typeof(Dialog))]
public class DialogDrawer : PropertyDrawer
{
    [System.Serializable]
    public class ReorderableList_Vector3 : ReorderableList<Vector3> { }
    [System.Serializable]
    public class ReorderableList_RectOffset : ReorderableList<RectOffset> { }
    public class ReorderableList<T> : SimpleReorderableList
    {
        public List<T> List;
    }
    public class SimpleReorderableList { }


    private ReorderableList replics;

    private ReorderableList getList(SerializedProperty property)
    {
        if (replics == null)
        {
            replics = new ReorderableList(property.serializedObject, property, true, true, true, true);
            replics.drawElementCallback = (UnityEngine.Rect rect, int index, bool isActive, bool isFocused) =>
            {
                rect.width -= 40;
                rect.x += 20;

                var amountRect = new Rect(rect.position.x, rect.position.y, rect.width / 2 - 2, rect.height);
                var unitRect = new Rect(rect.position.x + rect.width / 2 + 2, rect.position.y, rect.width / 2 - 3, rect.height);

                EditorGUI.PropertyField(amountRect, property.GetArrayElementAtIndex(index).FindPropertyRelative("person"), GUIContent.none);
                EditorGUI.PropertyField(unitRect, property.GetArrayElementAtIndex(index).FindPropertyRelative("text"), GUIContent.none);
            };

            replics.drawHeaderCallback = (Rect rect) => {
                EditorGUI.LabelField(rect, "Replics");
            };

        }
        return replics;
    }


    public override float GetPropertyHeight(SerializedProperty property, UnityEngine.GUIContent label)
    {
        return getList(property.FindPropertyRelative("List")).GetHeight();
    }



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
        var amountRect = new Rect(position.x, position.y, position.width , EditorGUIUtility.singleLineHeight);
        var unitRect = new Rect(position.x , position.y + EditorGUIUtility.singleLineHeight+3, position.width, position.height - EditorGUIUtility.singleLineHeight-3);


        // Draw fields - passs GUIContent.none to each so they are drawn without labels
        EditorGUI.PropertyField(amountRect, property.FindPropertyRelative("clip"), GUIContent.none);

        var listProperty = property.FindPropertyRelative("replics");
        var list = getList(listProperty);
        var height = 0f;
        for (var i = 0; i < listProperty.arraySize; i++)
        {
            height = Mathf.Max(height, EditorGUI.GetPropertyHeight(listProperty.GetArrayElementAtIndex(i)));
        }
        list.elementHeight = height;
        list.DoList(unitRect);


        // Set indent back to what it was
        EditorGUI.indentLevel = indent;

        EditorGUI.EndProperty();
    }
}