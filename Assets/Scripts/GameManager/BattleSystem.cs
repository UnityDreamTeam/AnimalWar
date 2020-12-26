using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BattleState { START, PLAYER_ONE_TURN, PLAYER_TWO_TURN, END}

public class BattleSystem : MonoBehaviour
{
    BattleState state; //Current battle state

    public GameObject[] playerOneArmy;
    public GameObject[] playerTwoArmy;

    public Transform[] armyOneInitialLocation;
    public Transform[] armyTwoInitialLocation;

    [SerializeField] int distanceBetweenArmies = 0;

    // Awake is called before all Start() functions in the game
    void Awake()
    {
        state = BattleState.START;
        SetUpBattle();
    }

    void SetUpBattle()
    {
        //TODO: change to player's animals pick
        GameObject ob1 = Instantiate(playerOneArmy[0], armyOneInitialLocation[0]);
        GameObject ob2 = Instantiate(playerTwoArmy[0], armyTwoInitialLocation[0]);

        ob2.transform.position = new Vector3(ob2.transform.position.x + distanceBetweenArmies,
            ob2.transform.position.y, ob2.transform.position.z);
        ob2.transform.eulerAngles = new Vector3(ob2.transform.eulerAngles.x, ob2.transform.eulerAngles.y + 180,
            ob2.transform.eulerAngles.z);

        state = BattleState.PLAYER_ONE_TURN;
    }

    public BattleState getCurrentPlayerTurn()
    {
        return state;
    }

    public void updatePlayerTurn()
    {
        state = 3 - state;
    }
}
