using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAIBehavior
{
    Transform findTarget(Transform source, GameObject[] animals);
}
