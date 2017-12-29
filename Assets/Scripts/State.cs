using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Sirenix.OdinInspector;

[CreateAssetMenu(menuName = "GameModel/State")]
public class State: ScriptableObject
{
    public string StateName;
	[Range(0,360)]
	public float Horizontal;
	[Range(-90,90)]
	public float Vertical;

	[MultiLineProperty]
	public string description;

	public Dialog monolog;
    public NarrativeLink[] narrativeLinks = new NarrativeLink[0];
    public CombinationLink[] combinationLinks = new CombinationLink[0];

    [HideInInspector]
    public List<NarrativeLink> InNarrativeLinks = new List<NarrativeLink>();

    [HideInInspector]
	[SerializeField]
	public float X, Y;

	public void Drag(Vector2 p)
	{
		X = p.x;
		Y = p.y;
	}

   

    [AssetsOnly, InlineEditor(InlineEditorModes.LargePreview)]
    public Sprite Sprite;

    public void AddNarrativeLink()
    {
        NarrativeLink c = new NarrativeLink();
        List<NarrativeLink> comb = narrativeLinks.ToList();
        comb.Add(c);
        narrativeLinks = comb.ToArray();
    }

    public void AddCombinationLink()
    {
        CombinationLink c = new CombinationLink();
        List<CombinationLink> comb = combinationLinks.ToList();
        comb.Add(c);
        combinationLinks = comb.ToArray();
    }


    public void RemoveCombinationLink(int i)
    {
        List<CombinationLink> comb = combinationLinks.ToList();
        comb.RemoveAt(i);
        combinationLinks = comb.ToArray();
    }

    public void RemoveNarrativeLink(int i)
    {

        List<NarrativeLink> comb = narrativeLinks.ToList();

        if (comb[i].endPoint)
        {
            comb[i].endPoint.InNarrativeLinks.Remove(comb[i]);
        }
        comb.RemoveAt(i);
        narrativeLinks = comb.ToArray();
    }
}
