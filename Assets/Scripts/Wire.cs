using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wire : MonoBehaviour {

    public Person start;
    public Person end;

    public void Init(Person start, Person end)
    {
        this.start = start;
        this.end = end;
    }
}
