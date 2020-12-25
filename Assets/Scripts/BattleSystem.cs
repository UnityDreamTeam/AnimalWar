using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BattleState { START, PLAYER_ONE_TURN, PLAYER_TWO_TURN, END}

public class BattleSystem : MonoBehaviour
{
    BattleState state;

    public GameObject[] playerOneArmy;
    public GameObject[] playerTwoArmy;

    public Transform[] armyOneInitialLocation;
    public Transform[] armyTwoInitialLocation;

    // Start is called before the first frame update
    void Start()
    {
        state = BattleState.START;
        SetUpBattle();
    }

    void SetUpBattle()
    {
        //TODO: change to player's animals pick
        Instantiate(playerOneArmy[0], armyOneInitialLocation[0]);
        Instantiate(playerTwoArmy[0], armyTwoInitialLocation[0]);

        state = BattleState.PLAYER_ONE_TURN;
    }
}
