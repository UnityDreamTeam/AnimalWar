using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BattleState {START, PLAYER_ONE_TURN, PLAYER_TWO_TURN}

public class BattleSystem : MonoBehaviour
{
    BattleState state; //Current battle state

    [SerializeField] Army playerOneArmy = null;
    [SerializeField] Army playerTwoArmy = null;

    [SerializeField] float distanceBetweenArmies = 0;
    [SerializeField] float distanceBetweenAnimals = 0;

    [SerializeField] Color playerOneBarColor = Color.red;
    [SerializeField] Color playerTwoBarColor = Color.blue;
    
    readonly int zoomIn = 3; //How much to zoom in object
    readonly int modulu_three = 3;

    int playerOneCurrentAnimalTurn;
    int playerTwoCurrentAnimalTurn;
    GameObject currentActiveAnimal;

    readonly int playerOneTagPosition = 7;
    readonly int playerTwoTagPosition = 8;

    //Save camera's transform properties
    Vector3 positionCamera;
    Quaternion rotationCamera;

    // Awake is called before all Start() functions in the game
    void Awake()
    {
        //Initialize Army objects
        Army.ArmySize = playerOneArmy.Animals.Length;
        playerOneArmy = new Army(playerOneArmy);
        playerTwoArmy = new Army(playerTwoArmy);

        positionCamera = Camera.main.transform.position;
        rotationCamera = Camera.main.transform.rotation;

        playerOneCurrentAnimalTurn = playerTwoCurrentAnimalTurn = 0;

        SetUpBattle();
    }

    void SetUpBattle()
    {
        //TODO: change to player's animals pick
        for (int i = 0; i < Army.ArmySize; i++)
        {
            GameObject playerOneAnimal = Instantiate(playerOneArmy.getAnimal(i), playerOneArmy.BaseLocation);
            GameObject PlayerTwoAnimal = Instantiate(playerTwoArmy.getAnimal(i), playerTwoArmy.BaseLocation);

            // each animal has tag to identify if belong to player one or to player two
            playerOneAnimal.tag = UnityEditorInternal.InternalEditorUtility.tags[playerOneTagPosition];
            PlayerTwoAnimal.tag = UnityEditorInternal.InternalEditorUtility.tags[playerTwoTagPosition];

            // change the color of the bar life of the player's animals
            playerOneAnimal.transform.Find("HealthBar/Bar/BarSprite").GetComponent<SpriteRenderer>().color = playerOneBarColor;
            PlayerTwoAnimal.transform.Find("HealthBar/Bar/BarSprite").GetComponent<SpriteRenderer>().color = playerTwoBarColor;

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
        //Set first animal to play (player #1)
        currentActiveAnimal = playerOneArmy.getAnimalObject(playerOneCurrentAnimalTurn++);
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
        state = modulu_three - state;

        if(state == BattleState.PLAYER_TWO_TURN)
        {
            //Set the current animal to be able to move
            do
            {
                currentActiveAnimal = playerTwoArmy.getAnimalObject(playerTwoCurrentAnimalTurn);
                //Shifting to the next animal
                playerTwoCurrentAnimalTurn++;
                playerTwoCurrentAnimalTurn %= Army.ArmySize;
            }while(currentActiveAnimal == null);
        }
        else
        {
            //Set the current animal to be able to move
            do
            {
                currentActiveAnimal = playerOneArmy.getAnimalObject(playerOneCurrentAnimalTurn);
                //Shifting to the next animal
                playerOneCurrentAnimalTurn++;
                playerOneCurrentAnimalTurn %= Army.ArmySize;
            }while(currentActiveAnimal == null);
        }
    }

    public void disableCurrentAnimalMovement()
    {
        currentActiveAnimal.GetComponent<AnimalController>().enabled = false;
        currentActiveAnimal.GetComponent<Animal>().disableMovement();
    }

    public void enableCurrentAnimalMovement()
    {
        currentActiveAnimal.GetComponent<AnimalController>().enabled = true;
    }

    public void focusOnActiveAnimal()
    {
        Camera.main.transform.LookAt(currentActiveAnimal.transform);
        //Zoom in
        Camera.main.orthographicSize -= zoomIn;
    }

    public void returnFocusToNormal()
    {
        Camera.main.transform.position = positionCamera;
        Camera.main.transform.rotation = rotationCamera;
        //Zoom out
        Camera.main.orthographicSize += zoomIn;
    }
}
