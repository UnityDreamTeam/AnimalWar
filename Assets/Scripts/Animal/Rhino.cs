using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rhino : Animal
{
    readonly int id = 3;

    public override int GetID()
    {
        return id;
    }
}
