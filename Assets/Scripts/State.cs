using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Sirenix.OdinInspector;

[CreateAssetMenu(menuName = "GameModel/State")]
public class State: ScriptableObject
{
    public string StateName;

	public int day = 1;
	public int minute = 0;

	[MultiLineProperty]
	public string description;

	public Dialog monolog;

	public bool StoryState = false;

	[ShowIf("IsStory")]
    public NarrativeLink[] narrativeLinks = new NarrativeLink[0];

	private bool IsStory()
	{
		return StoryState;
	}

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



    public Person person;

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
