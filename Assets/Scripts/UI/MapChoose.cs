using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MapChoose : MonoBehaviour
{
    [SerializeField] Button[] maps = new Button[3];
    
    private int currentMap;
    readonly int lowerBoundary = -1;
    readonly int higherBoundary = 3;

    [SerializeField] GameObject PlayerOneArmyPosition;
    List<GameObject> playerOneArmy;
    

    // Start is called before the first frame update
    private void Start()
	{
        currentMap = 0;
        transform.GetChild(currentMap).gameObject.SetActive(true);
        playerOneArmy = new List<GameObject>();

    }

	void selectMap(int index)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(i == index);
        }
    }

    public void changeMap(int change)
    {
        if (change + currentMap != lowerBoundary && change + currentMap != higherBoundary)
        {
            currentMap += change;
            selectMap(currentMap);
        }
    }

    public void startGame()
    {
        SceneManager.LoadScene(currentMap + 2);
    }

    public void addAnimalToArmy()
    {
        GameObject animalSelected = EventSystem.current.currentSelectedGameObject.transform.GetChild(0).gameObject;
        playerOneArmy.Add(animalSelected);
        //Debug.Log(EventSystem.current.currentSelectedGameObject.transform.GetChild(0).gameObject.name + "");
        GameObject selectedAnimal = Instantiate(EventSystem.current.currentSelectedGameObject, Vector3.zero,
            Quaternion.identity);
        selectedAnimal.transform.GetChild(0).transform.localScale = Vector3.one;

        foreach (Transform animalPos in PlayerOneArmyPosition.transform)
        {
            if (animalPos.childCount == 0) // dont have child
            {
                Debug.Log(animalPos.transform);
                selectedAnimal.transform.parent = animalPos;
                //selectedAnimal.transform.localPosition = animalPos.localPosition;
                break;
            }
        }
        
    }

    public void deleteAnimalFromArmy()
    {
        playerOneArmy.Remove(EventSystem.current.currentSelectedGameObject.transform.GetChild(0).gameObject);
    }
}
