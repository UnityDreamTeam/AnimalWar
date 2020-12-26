using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Army
{
    [SerializeField] GameObject [] animals = null; //Prefabs
    [SerializeField] Transform baseLocation = null;
    GameObject[] animals_objects; //Actual game objects

    public Transform BaseLocation { get => baseLocation; }

    public GameObject getAnimal(int index) { return animals[index]; }

    public int armySize() { return animals.Length; }

    public void setAnimalObject(GameObject animal, int index) { animals_objects[index] = animal; }
    public GameObject getAnimalObject(int index) { return animals_objects[index]; }
}
