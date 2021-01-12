using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LowestHPAnimal : IAIBehavior
{
    public Transform findTarget(Transform source, GameObject[] animals)
    {
        Transform lowestLocation = null;

        float lowestHP = int.MaxValue;

        for (int i = 0; i < animals.Length; i++)
        {
            if (animals[i] != null)
            {
                float currentHP = animals[i].GetComponent<Animal>().getCurrentHP();
                if (currentHP < lowestHP)
                {
                    lowestHP = currentHP;
                    lowestLocation = animals[i].transform;
                }
            }
        }

        return lowestLocation;
    }
}
