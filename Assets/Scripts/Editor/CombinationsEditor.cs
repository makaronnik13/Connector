using UnityEngine;
using UnityEditor;
using System;
using System.Collections.Generic;
using System.Linq;

public class CombinationsEditor : EditorWindow
{
    private enum EditorMode
    {
        Narrative,
        Combinations
    }
    private EditorMode editorMode = EditorMode.Combinations;
    private Vector2 screenDelta = Vector2.zero;
    private Link selectedPath = null;
    private Vector2 lastMousePosition;
	private List<KeyValuePair<State, GUIDraggableObject>> statesPositions = new List<KeyValuePair<State, GUIDraggableObject>> ();
    private List<KeyValuePair<State, GUIDraggableObject>> StatesPositions
	{
		get
		{
			if(statesPositions.Count == 0)
			{
				int i = 0;

				foreach (State state in ProjectStates())
					{
					KeyValuePair<State, GUIDraggableObject> kvp = new KeyValuePair<State, GUIDraggableObject> (state, new GUIDraggableObject (new Vector2(state.X, state.Y)));
					statesPositions.Add (kvp);
					kvp.Value.onDrag += kvp.Key.Drag;
					i++;
				}
            }
			return statesPositions;
		}
	}
	private static Texture2D backgroundTexture;    
    private static Texture2D BackgroundTexture
        {
            get
            {
                if (backgroundTexture == null)
                {
                    backgroundTexture = (Texture2D)Resources.Load("Icons/background") as Texture2D;
                    backgroundTexture.wrapMode = TextureWrapMode.Repeat;
                }
                return backgroundTexture;
            }
        }

    public static List<State> ProjectStates()
    {
        List<State> st = new List<State>();
        string[] guids = AssetDatabase.FindAssets("t:State");


        foreach (string s in guids)
        {
            string assetPath = AssetDatabase.GUIDToAssetPath(s);
            State asset = AssetDatabase.LoadAssetAtPath(assetPath, typeof(State)) as State;
            st.Add(asset);
        }

        if (st.Count == 0)
        {
            EditorWindow.GetWindow<CombinationsEditor>().Close();
        }
        return st;
    }

    [MenuItem("Window/CombinationsEditor")]
	static CombinationsEditor Init()
        {
			CombinationsEditor window = (CombinationsEditor)EditorWindow.GetWindow<CombinationsEditor>("Combinations editor", true, new Type[3] { typeof(Animator), typeof(Console), typeof(SceneView) });
            window.minSize = new Vector2(600, 400);
            window.ShowAuxWindow();
            return window;
        }
    private void OnDisable()
    {
        foreach (KeyValuePair<State, GUIDraggableObject> kvp in StatesPositions)
        {
            kvp.Key.Drag(kvp.Value.Position);
            EditorUtility.SetDirty(kvp.Key);
        }
    }
    private void OnGUI()
        {


	
		Event currentEvent = Event.current;
        if ( currentEvent.button == 2)
        {
            if (currentEvent.type == EventType.MouseDown)
            {

                selectedPath = null;
                    lastMousePosition = currentEvent.mousePosition;          
            }
            else if (currentEvent.type == EventType.MouseDrag)
            {

                Vector2 mouseMovementDifference = (currentEvent.mousePosition - lastMousePosition);

               screenDelta += new Vector2(mouseMovementDifference.x, mouseMovementDifference.y);

                lastMousePosition = currentEvent.mousePosition;
                currentEvent.Use();
            }
            
        }

        if (currentEvent.button == 1)
        {
            if (currentEvent.type == EventType.MouseDown)
            {

                Vector3 p = Event.current.mousePosition;

                GenericMenu menu = new GenericMenu();
                menu.AddItem(new GUIContent("AddState"), false, () => 
                {
                    State state =  (State)ScriptableObjectUtility.CreateAsset<State>();
                    KeyValuePair<State, GUIDraggableObject> kvp = new KeyValuePair<State, GUIDraggableObject>(state, new GUIDraggableObject(new Vector2(p.x - screenDelta.x, p.y-screenDelta.y)));

                   // kvp.Value.Position = new Vector2(kvp.Key.X, kvp.Key.Y);
                    statesPositions.Add(kvp);
                    kvp.Value.onDrag += kvp.Key.Drag;
                    Repaint();
                });
                menu.ShowAsContext();
            }
        }

        DrowChainsWindow();
        DrawCreatingLine();


        if (editorMode != EditorMode.Combinations)
        {
            GUI.color = Color.white * 0.6f;
        }
        else
        {
            GUI.color = Color.white;
        }

        if (GUI.Button(new Rect(0,0,30,30), ""))
        {
            editorMode = EditorMode.Combinations;
        }

        if (editorMode != EditorMode.Narrative)
        {
            GUI.color = Color.white * 0.6f;
        }
        else
        {
            GUI.color = Color.white;
        }

        if (GUI.Button(new Rect(30,0,30,30), ""))
        {
            editorMode = EditorMode.Narrative;
        }

        GUI.color = Color.black;
        GUI.Label(new Rect(5, 5, 20, 20), (Texture2D)Resources.Load("Icons/combinations") as Texture2D);
        GUI.Label(new Rect(35, 5, 20, 20), (Texture2D)Resources.Load("Icons/narrative") as Texture2D);
        GUI.color = Color.white;
    }

    private void DrawCreatingLine()
    {
        if (selectedPath!=null && Event.current.type == EventType.keyDown && Event.current.keyCode == KeyCode.Delete)
        {

            foreach (KeyValuePair<State, GUIDraggableObject> p in StatesPositions)
            {
                if (editorMode == EditorMode.Narrative && p.Key.narrativeLinks.Contains(selectedPath))
                {
                    p.Key.RemoveNarrativeLink(p.Key.narrativeLinks.ToList().IndexOf((NarrativeLink)selectedPath));
                }

                if (editorMode == EditorMode.Combinations && p.Key.combinationLinks.Contains(selectedPath))
                {
                    p.Key.RemoveCombinationLink(p.Key.combinationLinks.ToList().IndexOf((CombinationLink)selectedPath));
                }
            }
            selectedPath= null;
            Repaint();
        }

        if (selectedPath != null)
        {
           
            Rect start = new Rect();
            foreach (KeyValuePair<State, GUIDraggableObject> p in StatesPositions)
            {
                float s = 70;
                int i = 0;

                Link[] links = new Link[0];

                if (editorMode == EditorMode.Narrative)
                {
                    links = p.Key.narrativeLinks;
                }
                if (editorMode == EditorMode.Combinations)
                {
                    links = p.Key.combinationLinks;
                }


                foreach (Link c in links)
                {
                    float offset = s * i / (p.Key.narrativeLinks.Count() - 1);
					if (links.Count() == 1)
                    {
                        offset = 100f / 2 - 5;
                    }
                    if (c== selectedPath)
                    {         
                        start = new Rect(p.Value.Position.x + screenDelta.x - 100f / 2 + 5 + offset, p.Value.Position.y + screenDelta.y + 115 / 2f, 100.0f, 115.0f);
                    }
                   
                    i++;
                }
            }
            Handles.BeginGUI();
            DrawNodeCurve(start, new Rect(Event.current.mousePosition, Vector2.one), Color.white, 2);
            Handles.EndGUI();
        }    
    }

    private void DrowChainsWindow()
        {
            Rect fieldRect = new Rect(0, 0, position.width, position.height);
            GUI.DrawTextureWithTexCoords(fieldRect, BackgroundTexture, new Rect(0, 0, fieldRect.width / BackgroundTexture.width, fieldRect.height / BackgroundTexture.height));   
			DrawPathes();
            BeginWindows();
        State manipulatingState = null;

		for (int i = 0; i<=StatesPositions.Count-1; i++) 
		{
			Rect drawRect = new Rect (StatesPositions[i].Value.Position.x + screenDelta.x, StatesPositions[i].Value.Position.y + screenDelta.y, 100.0f,  115.0f);
			if(StatesPositions[i].Value.Click(drawRect))
			{
				manipulatingState = StatesPositions [i].Key;
				Selection.activeObject = StatesPositions[i].Key;
                KeyValuePair<State, GUIDraggableObject> kvp = StatesPositions.Find(k=>k.Key== manipulatingState);
                StatesPositions.Remove (kvp);
                StatesPositions.Add(kvp);
                Repaint ();
            }
            if (StatesPositions[i].Value.Click(drawRect, 1))
            {
                GenericMenu menu = new GenericMenu();
                Selection.activeObject = StatesPositions[i].Key;

                State st = StatesPositions[i].Key;
                Repaint();
                menu.AddItem(new GUIContent("AddLink"), false, ()=> {
                    if (editorMode == EditorMode.Combinations)
                    {
                        ((State)Selection.activeObject).AddCombinationLink();
                    }
                    else {
                        ((State)Selection.activeObject).AddNarrativeLink();
                    }
                });
                menu.AddItem(new GUIContent("RemoveState"), false, () => {

                    StatesPositions.Remove(StatesPositions.Find(s=>s.Key == st));
                    AssetDatabase.DeleteAsset(AssetDatabase.GetAssetPath(st));
                    AssetDatabase.Refresh();
                    Repaint();
                });

                menu.ShowAsContext();
            }
        }

		for (int i = 0; i<=StatesPositions.Count-1; i++)
			{
			
				DrawStateBox (StatesPositions [i]);
			}
            EndWindows();
    }

    private bool DrawButton(KeyValuePair<State, GUIDraggableObject>  state)
    {
        bool containCursor = false;
        Link[] combinations = new Link[0];
        if (editorMode == EditorMode.Combinations)
        {
            combinations = state.Key.combinationLinks;
        }
        if (editorMode == EditorMode.Narrative)
        {
            combinations = state.Key.narrativeLinks;
        }
           

            float s = 90f;
            int i = 0;

        Rect aim = new Rect(state.Value.Position.x + screenDelta.x + 100f / 2-7f, state.Value.Position.y + screenDelta.y- 5, 15, 15);
	
        GUI.Label(aim, new GUIContent(Resources.Load("Icons/button") as Texture2D));
		
		GUI.color = Color.white;

            if (Event.current.type == EventType.mouseUp && aim.Contains(Event.current.mousePosition) && selectedPath != null)
            {
        

            foreach (KeyValuePair<State, GUIDraggableObject> kvp in statesPositions)
                {
                    if (editorMode == EditorMode.Narrative && kvp.Key.narrativeLinks.Contains(selectedPath))
                    {
                        selectedPath.endPoint = state.Key;
                        selectedPath.endPoint.InNarrativeLinks.Add((NarrativeLink)selectedPath);
                    }

                if (editorMode == EditorMode.Combinations && kvp.Key.combinationLinks.Contains(selectedPath))
                {
                    selectedPath.endPoint = state.Key;
                }
            }

            selectedPath = null;
            }

            if (aim.Contains(Event.current.mousePosition))
            {
            containCursor = true;
            }
			
		if(editorMode == EditorMode.Narrative && !state.Key.StoryState)	
		{
			return false;
		}

            foreach (Link c in combinations)
            {
                float offset = s * i / (combinations.Count() - 1);
                if (combinations.Count() == 1)
                {
                    offset = 100f / 2;
                }

                float size = 15;
                if (selectedPath == c)
                {
                    size = 20;
                }
                Rect start = new Rect(state.Value.Position.x + screenDelta.x + offset-size/4, state.Value.Position.y + screenDelta.y + 110, size, size);

 
            GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, 1);

            GUI.Label(start, new GUIContent(Resources.Load("Icons/button") as Texture2D));

                if (start.Contains(Event.current.mousePosition))
                {

                
               /* if (Event.current.type == EventType.MouseDrag)
                {
                    selectedPath = c;
                }*/
                 if (Event.current.type == EventType.MouseDown)
                {
                    if (selectedPath!=c)
                    {
                        selectedPath = c;
                        Repaint();
                    }
                    else
                    {
                        selectedPath = null;
                        Repaint();
                    }
                    
                }

            }


               

                GUI.color = Color.white;
                i++;

                if (start.Contains(Event.current.mousePosition))
                {
                    containCursor = true;
                }
        }

        return containCursor;
    }

    private void DrawStateBox(KeyValuePair<State, GUIDraggableObject> state)
	{

			
		GUI.backgroundColor = Color.gray;


		if (Selection.activeObject == state.Key)
		{
			GUI.backgroundColor = GUI.backgroundColor * 1.3f;
		}


		Rect drawRect = new Rect (state.Value.Position.x + screenDelta.x, state.Value.Position.y + screenDelta.y, 100.0f, 115.0f);//, dragRect;

        

        GUILayout.BeginArea (drawRect, GUI.skin.GetStyle ("Box"));
		GUILayout.BeginVertical ();
		GUILayout.Label (state.Key.StateName, GUILayout.ExpandWidth(true));
	
		Texture2D texture = new Texture2D(1,1);
		if(state.Key.Sprite)
		{
		    texture = state.Key.Sprite.texture;
		}
		GUILayout.Label (texture, GUILayout.Height(100));
		
		GUILayout.EndVertical ();

      

       // dragRect = GUILayoutUtility.GetLastRect ();
	   // dragRect = new Rect (dragRect.x + state.Value.Position.x, dragRect.y + state.Value.Position.y, dragRect.width, dragRect.height);
		GUILayout.EndArea ();

		if (!DrawButton(state) && Selection.activeObject == state.Key) {
			state.Value.Drag (drawRect);
            //selectedPath = null;
			Repaint ();
		}


		GUI.backgroundColor = Color.white;
        
	}

	private void DrawPathes()
	{
		foreach (KeyValuePair<State, GUIDraggableObject> state in StatesPositions)
		{
            Link[] combinations = new Link[0];
            if (editorMode == EditorMode.Narrative)
            {
				if(!state.Key.StoryState)	
				{
					continue;
				}
                combinations = state.Key.narrativeLinks;
            }
            if (editorMode == EditorMode.Combinations)
            {
                combinations = state.Key.combinationLinks;
            } 

			float s =  95;
			int i = 0;


			foreach(Link c in combinations)
			{
				float offset =  s* i/(combinations.Count()-1);
				if(combinations.Count() == 1)
				{
					offset =  48;
				}

				Rect start = new Rect( state.Value.Position.x + screenDelta.x - 100f/2+5 +offset, state.Value.Position.y + screenDelta.y + 115/2f,  100.0f, 115.0f);

				

				try 
				{
					State aim = StatesPositions.First (k => k.Key == c.endPoint).Key; 
					Vector3 aimPosition = StatesPositions.Find (cell => cell.Key == aim).Value.Position + screenDelta;
                    Rect end = new Rect(aimPosition.x, aimPosition.y - 115 / 2f, 100.0f, 115.0f); ;
                    Handles.BeginGUI();
                    Color color = Color.white;
                    if (c == selectedPath)
                    {
                        color = color / 2;
                    }
                    DrawNodeCurve(start, end, color, 2);
                    Handles.EndGUI();
                }
				catch
				{
				}
		
				i++;
			}
		}
	}

	private	void DrawNodeCurve(Rect start, Rect end, Color c, float width)
        {
            float force = 1f;
            Vector3 startPos = new Vector3(start.x + start.width / 2, start.y + start.height / 2, 0);
            Vector3 endPos = new Vector3(end.x + end.width / 2, end.y + end.height / 2, 0);
            float distanceY = Mathf.Abs(startPos.y - endPos.y);
            float distanceX = Mathf.Abs(startPos.x - endPos.x);
            Vector3 middlePoint = (startPos + endPos) / 2;

            Vector3 startTan1 = startPos;
            Vector3 endTan2 = endPos;
            Vector3 startTan2 = middlePoint;
            Vector3 endTan1 = middlePoint;

            if (startPos.y > endPos.y)
            {
                startTan1 -= Vector3.down * 150;
                endTan2 -= Vector3.up * 150;
			if (startPos.y > endPos.y)
                {
                    endTan1 += Vector3.up * Mathf.Max(distanceY, 50);
                    startTan2 -= Vector3.up * Mathf.Max(distanceY, 50);
                }
                else
                {
                    endTan1 += Vector3.down * Mathf.Max(distanceY, 50);
                    startTan2 -= Vector3.down * Mathf.Max(distanceY, 50);
                }
            }
            else
            {
                startTan1 -= distanceY * Vector3.down / force / 2;
                endTan2 -= distanceY * Vector3.up / force / 2;
                if (startPos.x > endPos.x)
                {
                    endTan1 += distanceX * Vector3.right / force / 2;
                    startTan2 -= distanceX * Vector3.right / force / 2;
                }
                else
                {
                    endTan1 += distanceX * Vector3.left / force / 2;
                    startTan2 -= distanceX * Vector3.left / force / 2;
                }
            }

            Color shadowCol = new Color(0, 0, 0, 0.06f);

            // Draw a shadow
            for (int i = 0; i < 2; i++)
            {
			Handles.DrawBezier(startPos, middlePoint, startTan1, endTan1, shadowCol, null, (i + 1) * 7 *width);
            }
		Handles.DrawBezier(startPos, middlePoint, startTan1, endTan1, c, null, 3*width);

            for (int i = 0; i < 2; i++)
            {
			Handles.DrawBezier(middlePoint, endPos, startTan2, endTan2, shadowCol, null, (i + 1) * 7 *width);
            }
		Handles.DrawBezier(middlePoint, endPos, startTan2, endTan2, c, null, 3*width);
        }
  }