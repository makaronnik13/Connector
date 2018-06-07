//using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName = "GameModel/Person")]
public class Person : ScriptableObject {

    //[AssetsOnly, InlineEditor(InlineEditorModes.LargePreview)]
    public Sprite PersonSprite;
    public string PersonName;
    public bool Service = false;
	public StorryState wrongConnectionState;
	public bool hideInBook = false;

    public string Surname
    {
        get
            {
            try
            {
                return PersonName.Split(new char[] { ' ' })[1];
            }
            catch
            {
                return "special";
            }
        }
    }

    public string FirstName
    {
        get
        {
            try
            {
                return PersonName.Split(new char[] { ' ' })[0];
            }
            catch
            {
                return "special";
            }
        }
    }
}
