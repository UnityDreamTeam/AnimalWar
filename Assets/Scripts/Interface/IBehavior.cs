using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBehavior
{
    void attack();
    void move(Transform transform);
    int GetID();
}
