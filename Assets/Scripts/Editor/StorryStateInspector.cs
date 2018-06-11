using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

[CustomEditor(typeof(StorryState))]
public class StorryStateInspector : Editor {

    private StorryState state;
    private bool showLables;
    private ReorderableList monologReplics;
    private ReorderableList combinations;

    private void OnEnable()
    {
        state = (StorryState)target;
        monologReplics = new ReorderableList(serializedObject,
               serializedObject.FindProperty("monolog").FindPropertyRelative("replics"),
               true, true, true, true);
        monologReplics.drawElementCallback =
        (Rect rect, int index, bool isActive, bool isFocused) =>
        {
            var element = monologReplics.serializedProperty.GetArrayElementAtIndex(index);
            rect.y += 2;
            EditorGUI.PropertyField(
                new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight),
                element, GUIContent.none);

        };

        monologReplics.drawHeaderCallback = (Rect rect) => {
            EditorGUI.LabelField(rect, "Replics");
        };

        combinations = new ReorderableList(serializedObject,
               serializedObject.FindProperty("combinationLinks"),
               true, true, true, true);

        combinations.drawElementCallback =
        (Rect rect, int index, bool isActive, bool isFocused) =>
        {
            var element = combinations.serializedProperty.GetArrayElementAtIndex(index);
            rect.y += 2;
            EditorGUI.PropertyField(
                new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight),
                element, GUIContent.none);

        };

        combinations.drawHeaderCallback = (Rect rect) => {
            EditorGUI.LabelField(rect, "Combinations");
        };

        combinations.elementHeightCallback = (int i) =>
        {
            return (6+state.combinationLinks[i].dialog.replics.Count) * EditorGUIUtility.singleLineHeight;
        };
    }

    public override void OnInspectorGUI()
    {
        state.person = (Person)EditorGUILayout.ObjectField("Person",state.person, typeof(Person), false);
        state.TalkingTime = EditorGUILayout.FloatField("DialogTime", state.TalkingTime);

        showLables = EditorGUILayout.Foldout(showLables, "Warning chances");
        if (showLables)
        {
            state.DisconnectWarningChance = EditorGUILayout.Slider("Disconnect", state.DisconnectWarningChance, 0 , 1);
            state.DropWarningChance = EditorGUILayout.Slider("Drop", state.DropWarningChance, 0, 1);
            state.WrongConnectionWarningChance = EditorGUILayout.Slider("Wrong connection", state.WrongConnectionWarningChance, 0, 1);
        }

        EditorGUILayout.LabelField("");
        EditorGUILayout.LabelField("Monolog", EditorStyles.boldLabel);
        state.monolog.clip = (AudioClip)EditorGUILayout.ObjectField("AudioClip", state.monolog.clip, typeof(AudioClip), false);

        serializedObject.Update();
        monologReplics.DoLayoutList();


        EditorGUILayout.LabelField("");
        EditorGUILayout.LabelField("Combinations", EditorStyles.boldLabel);
  
        combinations.DoLayoutList();
        serializedObject.ApplyModifiedProperties();

        EditorGUILayout.LabelField("");
        EditorGUILayout.LabelField("Special combinations", EditorStyles.boldLabel);
        state.autoAddState.endPoint = (StorryState)EditorGUILayout.ObjectField("Auto", state.autoAddState.endPoint, typeof(StorryState), false);
        state.wrongConnectionState.endPoint = (StorryState)EditorGUILayout.ObjectField("Wrong", state.wrongConnectionState.endPoint, typeof(StorryState), false);
        state.SkipState.endPoint = (StorryState)EditorGUILayout.ObjectField("Skip", state.SkipState.endPoint, typeof(StorryState), false);

		state.EndingCall = EditorGUILayout.Toggle ("EndingCall", state.EndingCall);

        EditorUtility.SetDirty(state);
    }
}
