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
    GameObject teamOne;
    GameObject teamTwo;

    void Start()
    {
        teamOne = GameObject.Find("ArmyALocation");
        teamTwo = GameObject.Find("ArmyBLocation");
    }
        // Update is called once per frame
    void Update()
    {
        if (teamOne.transform.childCount == 0)
        {
            team2Win.enabled = true;
            killProcesses();
        }
        else if (teamTwo.transform.childCount == 0)
        {

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
