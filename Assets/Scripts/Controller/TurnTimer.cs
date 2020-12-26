﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class TurnTimer : MonoBehaviour
{
    [SerializeField] Text countDownDisplay = null;
    [SerializeField] Text go = null;
    [SerializeField] Text information = null;
    [SerializeField] float InitialTimer = 0f;

    BattleState player_turn;
    BattleSystem script;

    bool reloadTimer;
    // Start is called before the first frame update
    void Start()
    {
        reloadTimer = true;
        GameObject BattleSystem = GameObject.FindGameObjectWithTag("Battle_System");
        script = BattleSystem.GetComponent<BattleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (reloadTimer)
        {
            player_turn = script.getCurrentPlayerTurn();
            StartCoroutine(CountDownTimer(InitialTimer));
            //Wait after a player's turn ended
            reloadTimer = false;
        }
    }

    IEnumerator CountDownTimer(float initialTime)
    {
        //Convert enum string to number
        information.text = "Player's #" + player_turn.ToString("D") + " Turn";
        information.enabled = true;

        yield return new WaitForSeconds(1f);
        information.enabled = false;

        //Enable movement only after pronauncing on player's turn
        go.enabled = true;
        yield return new WaitForSeconds(1f);
        script.enableCurrentAnimalMovement();
        go.enabled = false;

        float countDownTimer = initialTime;
        while (countDownTimer > 0)
        {
            countDownDisplay.text = "Time left : " + countDownTimer.ToString();

            yield return new WaitForSeconds(1f);
            countDownTimer--;//Update timer
        }

        //Update relevant components
        script.disableCurrentAnimalMovement();

        information.text = "Turn is over";
        information.enabled = true;
       
        yield return new WaitForSeconds(1f);

        information.enabled = false;
        script.updatePlayerTurn();
        script.focusOnActiveAnimal();
        yield return new WaitForSeconds(3f);
        script.returnFocusToNormal();
        yield return new WaitForSeconds(2f);
        reloadTimer = true;
    }
}
