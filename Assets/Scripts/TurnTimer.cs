using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class TurnTimer : MonoBehaviour
{
    [SerializeField] Text countDownDisplay = null;
    [SerializeField] float InitialTimer = 0f;
    BattleState player_turn;

    bool reloadTimer;
    // Start is called before the first frame update
    void Start()
    {
        reloadTimer = true;
        GameObject BattleSystem = GameObject.FindGameObjectWithTag("Battle_System");
        BattleSystem script = BattleSystem.GetComponent<BattleSystem>();
        player_turn = script.getCurrentPlayerTurn();
    }

    // Update is called once per frame
    void Update()
    {
        if (reloadTimer)
        {
            StartCoroutine(CountDownTimer(InitialTimer));
            //Wait after a player's turn ended
            reloadTimer = false;
        }
    }

    IEnumerator CountDownTimer(float initialTime)
    {
        countDownDisplay.text = "Player's #" + player_turn.ToString("D") + " Turn";
        yield return new WaitForSeconds(2f);
        float countDownTimer = initialTime;
        while (countDownTimer > 0)
        {
            countDownDisplay.text = "Left time: " + countDownTimer.ToString();

            yield return new WaitForSeconds(1f);
            countDownTimer--;//Update timer
        }
        //Update player's turn
        player_turn = 3 - player_turn;

        countDownDisplay.text = "Turn is over";
        yield return new WaitForSeconds(2f);
        reloadTimer = true;
    }
}
