using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClosestAnimal : IAIBehavior
{
    public Transform findTarget(Transform source, GameObject[] animals)
    {
        Vector3 computerAnimalLocation = source.position;

        float distance = int.MaxValue;
        Transform closestLocation = null;

        for (int i = 0; i < animals.Length; i++)
        {
            if (animals[i] != null)
            {
                float currentDistance = Vector3.Distance(computerAnimalLocation, animals[i].transform.position);
                if (currentDistance < distance)
                {
                    distance = currentDistance;
                    closestLocation = animals[i].transform;
                }
            }
        }

        return closestLocation;
    }
}
