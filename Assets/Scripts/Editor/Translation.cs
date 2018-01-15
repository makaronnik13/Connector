using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public class Translation :  EditorWindow {

	private bool showTasks, showMonologs, showDialogs;

	private TranslationDictionary dictionary;
	private TranslationDictionary Dictionsry
	{
		get
		{
			return dictionary;
		}
	}

	private List<State> states = new List<State>();
	private List<State> States
	{
		get
		{
			if(states.Count == 0)
			{
				states = CombinationsEditor.ProjectStates ();
			}
			return states;
		}
	}
		

	[MenuItem("Window/Translation")]
	static Translation Init()
	{
		Translation window = (Translation)EditorWindow.GetWindow<Translation>("Translation editor", true, new Type[3] { typeof(Animator), typeof(Console), typeof(SceneView) });
		window.minSize = new Vector2(600, 400);
		window.ShowAuxWindow();
		return window;
	}

	void Awake()
	{
	}

	private void OnGUI()
	{
		EditorGUILayout.BeginVertical ();

		EditorGUI.indentLevel = 0;
		showTasks = EditorGUILayout.Foldout(showTasks, "Tasks");

		if (showTasks) {
			EditorGUI.indentLevel = 1;
			EditorGUILayout.BeginHorizontal ();
			EditorGUILayout.LabelField ("original");

			GUI.color = Color.gray;
			GUI.Box (new Rect(GUILayoutUtility.GetLastRect().position, new Vector2(position.width-5,16)) ,"");
			GUI.color = Color.white;
			EditorGUI.LabelField (GUILayoutUtility.GetLastRect(),"original");

			foreach (TranslationDictionary.Languages l in Enum.GetValues(typeof(TranslationDictionary.Languages))) {
				EditorGUILayout.LabelField (l.ToString());
			}
			EditorGUILayout.EndHorizontal ();
			foreach (State s in States) {
				EditorGUILayout.BeginHorizontal ();
				EditorGUILayout.EndHorizontal ();
			}

		}

		EditorGUI.indentLevel = 0;
		showMonologs = EditorGUILayout.Foldout(showMonologs, "Monologs");

		if (showMonologs) {
			EditorGUI.indentLevel = 1;
			EditorGUILayout.BeginHorizontal ();
			EditorGUILayout.LabelField ("person");

			GUI.color = Color.gray;
			GUI.Box (new Rect(GUILayoutUtility.GetLastRect().position, new Vector2(position.width-5,16)) ,"");
			GUI.color = Color.white;
			EditorGUI.LabelField (GUILayoutUtility.GetLastRect(),"person");

			EditorGUILayout.LabelField ("original");
			foreach (TranslationDictionary.Languages l in Enum.GetValues(typeof(TranslationDictionary.Languages))) {
				EditorGUILayout.LabelField (l.ToString());
			}
			EditorGUILayout.EndHorizontal ();
			foreach (State s in States) {
				GUILayout.Box("", new GUILayoutOption[]{GUILayout.ExpandWidth(true), GUILayout.Height(1)});
				foreach (Replica replica in s.monolog.replics) {
					EditorGUILayout.BeginHorizontal ();
					EditorGUILayout.LabelField (replica.person.ToString ());
					EditorGUILayout.LabelField (replica.text);
					foreach (TranslationDictionary.Languages l in Enum.GetValues(typeof(TranslationDictionary.Languages))) {
						EditorGUILayout.TextField (replica.text);
					}
					EditorGUILayout.EndHorizontal ();
				}
			}
		}

		EditorGUI.indentLevel = 0;
		showDialogs = EditorGUILayout.Foldout(showDialogs, "Dialogs");
		if (showDialogs) {
			EditorGUI.indentLevel = 1;
			EditorGUILayout.BeginHorizontal ();
			EditorGUILayout.LabelField ("person");

			GUI.color = Color.gray;
			GUI.Box (new Rect(GUILayoutUtility.GetLastRect().position, new Vector2(position.width-5,16)) ,"");
			GUI.color = Color.white;
			EditorGUI.LabelField (GUILayoutUtility.GetLastRect(),"person");
			EditorGUILayout.LabelField ("original");
			foreach (TranslationDictionary.Languages l in Enum.GetValues(typeof(TranslationDictionary.Languages))) {
				EditorGUILayout.LabelField (l.ToString());
			}
			EditorGUILayout.EndHorizontal ();
			/*
			foreach (State s in States) {
				foreach (CombinationLink link in s.combinationLinks) {
					foreach (Replica replica in link.dialog.replics) {
						EditorGUILayout.BeginHorizontal ();
						EditorGUILayout.LabelField (replica.person.ToString ());
						EditorGUILayout.LabelField (replica.text);
						foreach (TranslationDictionary.Languages l in Enum.GetValues(typeof(TranslationDictionary.Languages))) {
							EditorGUILayout.TextField (replica.text);
						}
						EditorGUILayout.EndHorizontal ();
					}
				}
			}*/
		}

		EditorGUILayout.EndVertical ();
	}
}
