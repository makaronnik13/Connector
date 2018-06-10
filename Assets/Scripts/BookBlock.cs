using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookBlock : MonoBehaviour {

	public void HooverOff()
    {
        bool close = false;
        foreach (PaperWithName addres in FindObjectsOfType<PaperWithName>())
        {
            if (addres.transform.parent == null)
            {
                close = true;
            }
        }

        if (close)
        {
            FindObjectOfType<AddresBook>().CloseBook();
        }
    }
}
