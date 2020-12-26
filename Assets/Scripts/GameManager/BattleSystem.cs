using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BattleState { START, PLAYER_ONE_TURN, PLAYER_TWO_TURN, END}

public class BattleSystem : MonoBehaviour
{
    BattleState state; //Current battle state

    [SerializeField] Army playerOneArmy = null;
    [SerializeField] Army playerTwoArmy = null;

    [SerializeField] int distanceBetweenArmies = 0;
    [SerializeField] int distanceBetweenAnimals = 0;

    // Awake is called before all Start() functions in the game
    void Awake()
    {
        state = BattleState.START;
        SetUpBattle();
    }

    bool to_focus;
    void Update()
    {
        
    }

    void CameraFocus(Transform target)
    {
        Vector3 pointOnside = target.position + new Vector3(target.localScale.x * 0.5f, 0.0f, target.localScale.z * 0.5f);
        float aspect = (float)Screen.width / (float)Screen.height;
        float maxDistance = (target.localScale.y * 0.5f) / Mathf.Tan(Mathf.Deg2Rad * (Camera.main.fieldOfView / aspect));
        Camera.main.transform.position = Vector3.MoveTowards(pointOnside, target.position, -maxDistance);
        Camera.main.transform.LookAt(target.position);
    }

    void SetUpBattle()
    {
        //TODO: change to player's animals pick
        for (int i = 0; i < playerOneArmy.armySize(); i++)
        {
            GameObject playerOneAnimal = Instantiate(playerOneArmy.getAnimal(i), playerOneArmy.BaseLocation);
            GameObject PlayerTwoAnimal = Instantiate(playerTwoArmy.getAnimal(i), playerTwoArmy.BaseLocation);

            playerOneArmy.setAnimalObject(playerOneAnimal, i);
            playerTwoArmy.setAnimalObject(PlayerTwoAnimal, i);

            //Set position to animal of army #1
            placeAnimalOnMap(playerOneAnimal, playerOneAnimal.transform.position.x, playerOneAnimal.transform.position.y - i * distanceBetweenAnimals);

            //Set position to animal of army #2
            placeAnimalOnMap(PlayerTwoAnimal, PlayerTwoAnimal.transform.position.x + distanceBetweenArmies,
                PlayerTwoAnimal.transform.position.y - i * distanceBetweenAnimals);

            //Set rotation to animal of army #2
            rotateAnimal(PlayerTwoAnimal);
        }

        state = BattleState.PLAYER_ONE_TURN;
    }

    void placeAnimalOnMap(GameObject animal, float x, float y)
    {
        animal.transform.position = new Vector3(x, y, animal.transform.position.z);
    }

    void rotateAnimal(GameObject animal)
    {
        animal.transform.eulerAngles = new Vector3(animal.transform.eulerAngles.x, animal.transform.eulerAngles.y + 180,
            animal.transform.eulerAngles.z);
    }

    public BattleState getCurrentPlayerTurn()
    {
        return state;
    }

    public void updatePlayerTurn()
    {
        //State can be either 1 or 2
        state = 3 - state;
    }

    public void focusOnNextAnimal()
    {
        CameraFocus(playerOneArmy.getAnimalObject(0).transform);
    }
}
