﻿using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AnimalsChoose : MonoBehaviour
{
    [SerializeField] GameObject PlayerOneArmyPosition = null;
    [SerializeField] GameObject[] playerOneArmy;
    readonly int maxNumberOfAnimals = 5;
    readonly float scaleMiniAnimal = 0.5f;
    int[] animalMap = null;
    int countAnimals = 0;
    readonly int empty = -1;
    int difficult = 0;
    [SerializeField] AudioSource select = null;
    [SerializeField] AudioSource delete = null;

    void Awake()
    {
        DontDestroyOnLoad(this); // save for the next 
    }

    // Start is called before the first frame update
    void Start()
    {
        playerOneArmy = new GameObject[maxNumberOfAnimals];
        animalMap = new int[maxNumberOfAnimals];
        for (int i = 0; i < animalMap.Length; i++)
        {
            animalMap[i] = empty;
        }
    }

    public void addAnimalToArmy()
    {
        if (CountAnimals < maxNumberOfAnimals)
        {
            GameObject animalSelected = EventSystem.current.currentSelectedGameObject;
            GameObject selectedAnimal = Instantiate(animalSelected,Vector3.zero, Quaternion.identity);

            selectedAnimal.GetComponent<AddAnimal>().enabled = false;
            selectedAnimal.GetComponent<Button>().onClick.AddListener(deleteAnimalFromArmy);

            select.Play();

            foreach (Transform animalPos in PlayerOneArmyPosition.transform)
            {
                if (animalPos.childCount == 0) // dont have child
                {
                    selectedAnimal.transform.SetParent(animalPos);
                    selectedAnimal.transform.transform.localScale = new Vector3(scaleMiniAnimal, scaleMiniAnimal, scaleMiniAnimal);
                    selectedAnimal.transform.transform.localPosition = Vector3.zero;
                    playerOneArmy[Int32.Parse(animalPos.name)] = animalSelected.transform.GetChild(0).gameObject;
                    animalMap[Int32.Parse(animalPos.name)] = animalSelected.transform.GetChild(0)
                        .gameObject.GetComponent<Animal>().GetID();
                    CountAnimals++;
                    break;
                }
            }
        }
    }

    public void deleteAnimalFromArmy()
    {
        delete.Play();
        int indexToDelete = Int32.Parse(EventSystem.current.currentSelectedGameObject.transform.parent.name);
        playerOneArmy[indexToDelete] = null;
        animalMap[indexToDelete] = empty;
        CountAnimals--;
        Destroy(EventSystem.current.currentSelectedGameObject);
    }

    public void chooseDifficult(int dif)
    {
        Difficult = dif;
    }

    public int[] getMap()
    {
        return animalMap;
    }

    public int CountAnimals { get => countAnimals; set => countAnimals = value; }
    public int Difficult { get => difficult; set => difficult = value; }
}