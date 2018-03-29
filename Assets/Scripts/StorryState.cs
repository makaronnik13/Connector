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

    public override Person secondPerson()
    {
        return combinationLinks[0].endPoint.person;
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
