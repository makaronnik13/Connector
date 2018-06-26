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


	public CombinationLink[] combinationLinks = new CombinationLink[0];

    public Link wrongConnectionState;
    public Link autoAddState;
	public Link SkipState;

    public NewsVariant wrongConnectionNews;

	public bool EndingCall = false;

    public override List<Person> secondPersons()
    {
		return combinationLinks.Select(cl=>cl.person).ToList();
    }

    public override Dialog StateDialog(int path)
    {
        return combinationLinks[path].dialog;
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
}
