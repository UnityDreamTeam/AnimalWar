using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : Animal
{
    readonly int id = 2;
    public override int GetID()
    {
        return id;
    }
}
