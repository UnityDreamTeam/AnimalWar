using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class VictoryChecker : MonoBehaviour
{
    [SerializeField] TextMeshPro team1Win = null;
    [SerializeField] TextMeshPro team2Win = null;
    [SerializeField] GameObject timer;
    [SerializeField] GameObject battle_system;
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
            team2Win.enabled = true;
            killProcesses();
        }
        else if (teamTwo.Length == 0)
        {
            Debug.Log("Team 1 wins");
            team1Win.enabled = true;
            killProcesses();
        } 
    }

    void killProcesses()
    {
        Destroy(battle_system);
        Destroy(timer);
    }
}
