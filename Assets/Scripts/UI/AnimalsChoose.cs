using System.Collections;
using System.Collections.Generic;
using UnityEditor.Events;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AnimalsChoose : MonoBehaviour
{
    [SerializeField] GameObject PlayerOneArmyPosition;
    List<GameObject> playerOneArmy;
    readonly int maxNumberOfAnimals = 5;
    readonly int minNumberOfAnimals = 1;

    // Start is called before the first frame update
    void Start()
    {
        playerOneArmy = new List<GameObject>();
    }

    public void addAnimalToArmy()
    {
        if (playerOneArmy.Count < maxNumberOfAnimals)
        {
            GameObject animalSelected = EventSystem.current.currentSelectedGameObject.transform.GetChild(0).gameObject;
            playerOneArmy.Add(animalSelected);
            GameObject selectedAnimal = Instantiate(EventSystem.current.currentSelectedGameObject, Vector3.zero,
                Quaternion.identity);

            selectedAnimal.GetComponent<Button>().onClick.AddListener(deleteAnimalFromArmy);
            //selectedAnimal.GetComponent<Button>().onClick.SetPersistentListenerState
            UnityEventTools.RemovePersistentListener(selectedAnimal.GetComponent<Button>().onClick, 0);
            
            foreach (Transform animalPos in PlayerOneArmyPosition.transform)
            {
                if (animalPos.childCount == 0) // dont have child
                {
                    Debug.Log(animalPos.transform);
                    selectedAnimal.transform.parent = animalPos;
                    selectedAnimal.transform.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                    selectedAnimal.transform.transform.localPosition = Vector3.zero;
                    break;
                }
            }
        }
        
    }

    public void deleteAnimalFromArmy()
    {
        playerOneArmy.Remove(EventSystem.current.currentSelectedGameObject.transform.GetChild(0).gameObject);
        Destroy(EventSystem.current.currentSelectedGameObject);
    }
}