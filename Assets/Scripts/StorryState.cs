using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(menuName = "GameModel/StoryState")]
public class StorryState : State {

    [HideInInspector]
    [SerializeField]
    public float X, Y;

    public void Drag(Vector2 p)
    {
        X = p.x;
        Y = p.y;
    }

    public NarrativeLink[] narrativeLinks = new NarrativeLink[0];
	public CombinationLink[] combinationLinks = new CombinationLink[0];

	[HideInInspector]
	public List<NarrativeLink> InNarrativeLinks = new List<NarrativeLink>();

    public override Person secondPerson()
    {
        return combinationLinks[0].endPoint.person;
    }

    public override Dialog StateDialog(int path)
    {
        return combinationLinks[path].dialog;
    }

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
			//comb[i].endPoint.InNarrativeLinks.Remove(comb[i]);
		}
		comb.RemoveAt(i);
		narrativeLinks = comb.ToArray();
	}
}
