using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

[CustomEditor(typeof(News))]
public class NewsInspector : Editor 
{
	private News  news;
	private ReorderableList list;

	private void OnEnable()
	{
		news = (News)target;
		list = new ReorderableList(serializedObject,
			serializedObject.FindProperty("Variants"),
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
			EditorGUI.LabelField(rect, "Variants");
		};
	}


	public override void OnInspectorGUI()
	{
		news.defaultVariant = (NewsVariant)EditorGUILayout.ObjectField ("default", news.defaultVariant, typeof(NewsVariant), false);
			serializedObject.Update();
			list.DoLayoutList();
			serializedObject.ApplyModifiedProperties();

		EditorUtility.SetDirty (news);
	}
}
