using System;
using UnityEditor.Events;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AnimalsChoose : MonoBehaviour
{
    [SerializeField] GameObject PlayerOneArmyPosition = null;
    GameObject[] playerOneArmy;
    readonly int maxNumberOfAnimals = 5;
    int countAnimals = 0;

    // Start is called before the first frame update
    void Start()
    {
        playerOneArmy = new GameObject[maxNumberOfAnimals];
    }

    public void addAnimalToArmy()
    {
        if (countAnimals < maxNumberOfAnimals)
        {
            GameObject animalSelected = EventSystem.current.currentSelectedGameObject;
            GameObject selectedAnimal = Instantiate(EventSystem.current.currentSelectedGameObject,
                Vector3.zero, Quaternion.identity);

            selectedAnimal.GetComponent<Button>().onClick.AddListener(deleteAnimalFromArmy);
            UnityEventTools.RemovePersistentListener(selectedAnimal.GetComponent<Button>().onClick, 0);
            
            foreach (Transform animalPos in PlayerOneArmyPosition.transform)
            {
                if (animalPos.childCount == 0) // dont have child
                {
                    selectedAnimal.transform.SetParent(animalPos);
                    selectedAnimal.transform.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                    selectedAnimal.transform.transform.localPosition = Vector3.zero;
                    playerOneArmy[Int32.Parse(animalPos.name)] = animalSelected.transform.GetChild(0).gameObject;
                    countAnimals++;
                    break;
                }
            }
        }
    }

    public void deleteAnimalFromArmy()
    {
        int indexToDelete = Int32.Parse(EventSystem.current.currentSelectedGameObject.transform.parent.name);
        playerOneArmy[indexToDelete] = null;
        countAnimals--;
        Destroy(EventSystem.current.currentSelectedGameObject);
    }
}