using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBehavior
{
    bool attack();
    void move(Transform transform);
    int GetID();
}
