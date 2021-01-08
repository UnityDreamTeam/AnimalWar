using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wolf : Animal
{
    readonly int id = 5;

    public override int GetID()
    {
        return id;
    }
}
