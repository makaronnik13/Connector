using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName = "GameModel/FillerState")]
public class FillerState : State {

	public Person aimPerson;
    public Dialog dialog;

    public override Person secondPerson()
    {
        return aimPerson;
    }

    public override Dialog StateDialog(int path)
    {
        return dialog;
    }

}
