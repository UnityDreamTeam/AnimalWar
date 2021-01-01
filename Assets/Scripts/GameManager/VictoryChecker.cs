using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryChecker : MonoBehaviour
{
    GameObject[] teamOne;
    GameObject[] teamTwo;

    // Update is called once per frame
    void Update()
    {
        teamOne = GameObject.FindGameObjectsWithTag("PlayerOne");
        teamTwo = GameObject.FindGameObjectsWithTag("PlayerTwo");

        if (teamOne.Length == 0)
        {
            Debug.Log("Team 2 wins");
        }
        else if (teamTwo.Length == 0)
        {
            Debug.Log("Team 1 wins");
        }
 
    }
}
