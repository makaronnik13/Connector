using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
//[CreateAssetMenu(menuName = "GameModel/FillerState")]
public class FillerState : State {

	public Person aimPerson;
    public Dialog dialog;

    public override List<Person> secondPersons()
    {
		return new List<Person>{aimPerson};
    }

    public override Dialog StateDialog(int path)
    {
        return dialog;
    }

}
