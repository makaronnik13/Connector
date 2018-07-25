//using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName = "GameModel/Person")]
public class Person : ScriptableObject {

    //[AssetsOnly, InlineEditor(InlineEditorModes.LargePreview)]
    //public Sprite PersonSprite;
    public string PersonName;
    public string Number;

    public bool Service = false;
    public bool hideInBook = false;

    public StorryState wrongConnectionState;

    [ContextMenu("Randomize number")]
    private void GenerateRandomNumber()
    {
        Number = Random.Range(1, 9)+"-";
        Number += Random.Range(0, 9) +""+ Random.Range(0, 9) + "-" + Random.Range(0, 9) +""+ Random.Range(0, 9);
    }

    public string FirstName
    {
        get
            {
            try
            {
                int lastDotIndex = PersonName.IndexOf(' ');
                return PersonName.Substring(0, lastDotIndex);
            }
            catch
            {
                return "special";
            }
        }
    }

    public string Surname
    {
        get
        {
            try
            {
                int lastDotIndex = PersonName.IndexOf(' ');
                return PersonName.Substring(lastDotIndex + 1);
            }
            catch
            {
                return "special";
            }
        }
    }
}
