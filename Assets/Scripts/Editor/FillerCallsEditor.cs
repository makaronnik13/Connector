
using UnityEngine;
using UnityEditor;
using System;
using System.Collections.Generic;
using System.Linq;

public class FillerCallsEditor : EditorWindow {

	private Dictionary<int, bool> daysDict = new Dictionary<int, bool>();

	private Vector2 scrollPosition = Vector2.zero;

	private List<FillerState> fillerStates;
	private List<FillerState> FillerStates
	{
		get
		{
			if(fillerStates == null)
			{
		
					fillerStates = new List<FillerState>();
					string[] guids = AssetDatabase.FindAssets("t:FillerState");


					foreach (string s in guids)
					{
						string assetPath = AssetDatabase.GUIDToAssetPath(s);
					FillerState asset = AssetDatabase.LoadAssetAtPath(assetPath, typeof(FillerState)) as FillerState;
					fillerStates.Add(asset);
					fillerStates.OrderBy (f=>f.day*60*24+f.minute);
					}

				for(int i = 1; i<=7; i++)
				{
					daysDict.Add (i, false);
				}
			}

			return fillerStates;
		}	
	}

	[MenuItem("Window/FillersCallsEditor")]
	static FillerCallsEditor Init()
	{
		FillerCallsEditor window = (FillerCallsEditor)EditorWindow.GetWindow<FillerCallsEditor>("Fillers calls editor", true, new Type[3] { typeof(Animator), typeof(Console), typeof(SceneView) });
		window.minSize = new Vector2(600, 400);
		window.ShowAuxWindow();
		return window;
	}

	private void OnGUI()
	{
		FillerStates.OrderBy (f=>f.day*60*24+f.minute);

		scrollPosition = EditorGUILayout.BeginScrollView (scrollPosition, GUIStyle.none, GUI.skin.verticalScrollbar);
		EditorGUILayout.BeginVertical ();


		for(int i = 1; i<=FillerStates[FillerStates.Count-1].day;i++)
		{
			EditorGUILayout.LabelField ("Day "+i, EditorStyles.boldLabel);
			GUI.Box (GUILayoutUtility.GetLastRect(), "");


			Event currentEvent = Event.current;

			if (currentEvent.type == EventType.MouseDown) {
				if (GUILayoutUtility.GetLastRect ().Contains (currentEvent.mousePosition)) 
				{
					if (currentEvent.button == 0){
						daysDict [i] = !daysDict [i];
						Repaint ();
					}
				}
			}


			EditorGUI.LabelField (GUILayoutUtility.GetLastRect(), "Day "+i, EditorStyles.boldLabel);

			if (daysDict [i]) {

				foreach (FillerState fs in FillerStates.Where(fs=>fs.day == i).OrderBy(fs=>fs.minute)) {
					EditorGUILayout.BeginHorizontal ();
					EditorGUILayout.BeginVertical (GUILayout.Width(50));
					fs.day = EditorGUILayout.IntSlider ("day", fs.day, 1, 7);
					fs.minute = EditorGUILayout.IntField ("minute", fs.minute);
					fs.minute = Mathf.Clamp (fs.minute, 0, 24 * 60);
					EditorGUILayout.EndVertical ();

					EditorGUILayout.BeginVertical (GUILayout.Width(100));

					if (fs.person) {
						GUILayout.Label (fs.person.PersonSprite.texture, GUILayout.Width (100), GUILayout.Height (100));
					}
					FillerStates.ElementAt (FillerStates.IndexOf (fs)).person = EditorGUILayout.ObjectField (fs.person, typeof(Person), false, GUILayout.Width (100)) as Person;
				
					EditorGUILayout.EndVertical ();

					EditorGUILayout.BeginVertical (GUILayout.Width(100));
					if (fs.aimPerson) {
						GUILayout.Label (fs.aimPerson.PersonSprite.texture, GUILayout.Width (100), GUILayout.Height (100));
					}
					FillerStates.ElementAt (FillerStates.IndexOf (fs)).aimPerson = EditorGUILayout.ObjectField (fs.aimPerson, typeof(Person), false, GUILayout.Width (100)) as Person;
				
					EditorGUILayout.EndVertical ();

					EditorGUILayout.BeginVertical (GUILayout.Width(50));
					fs.waitingTime = EditorGUILayout.FloatField ("waiting", fs.waitingTime);
					fs.canBeDisconnectedAfter = EditorGUILayout.FloatField ("safe disconnect", fs.canBeDisconnectedAfter);
					fs.badChance = EditorGUILayout.Slider ("warning chance", fs.badChance, 0, 1);
					EditorGUILayout.EndVertical ();
					EditorGUILayout.EndHorizontal ();


				}
			}
		}



		EditorGUILayout.EndVertical ();
		EditorGUILayout.EndScrollView();
	}
}
