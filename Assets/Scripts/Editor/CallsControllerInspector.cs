using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using UnityEditorInternal;

[CustomEditor(typeof(DemoCallsController))]
public class CallsControllerInspector : Editor
{
    private DemoCallsController controller;
    private ReorderableList list;
    private bool showStates;

    private void OnEnable()
    {
        controller = (DemoCallsController)target;
        list = new ReorderableList(serializedObject,
               serializedObject.FindProperty("demoStates"),
               true, true, true, true);
        list.drawElementCallback =
        (Rect rect, int index, bool isActive, bool isFocused) => 
        {
            var element = list.serializedProperty.GetArrayElementAtIndex(index);
            rect.y += 2;
            EditorGUI.PropertyField(
                new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight),
                element, GUIContent.none);
           
        };

        list.drawHeaderCallback = (Rect rect) => {
            EditorGUI.LabelField(rect, "States");
        };
    }

    
    public override void OnInspectorGUI()
    {
        controller.DropButton = (Button)EditorGUILayout.ObjectField(controller.DropButton, typeof(Button), true);
        controller.TakeButton = (Button)EditorGUILayout.ObjectField(controller.TakeButton, typeof(Button), true);

        showStates = EditorGUILayout.Foldout(showStates, "States");

        if (showStates)
        {
            serializedObject.Update();
            list.DoLayoutList();
            serializedObject.ApplyModifiedProperties();
        } 
    }
    
}
