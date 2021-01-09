using UnityEngine;

[System.Serializable]
public class Army
{
    [SerializeField] GameObject[] animals = null; //Prefabs
    [SerializeField] Transform baseLocation = null;

    static int armySize;
    GameObject[] animals_objects; //Actual game objects

    public Army(Army other)
    {
        this.animals = other.animals;
        this.baseLocation = other.baseLocation;
        this.animals_objects = new GameObject[armySize];
    }

    public Army(GameObject[] animals, Transform location)
    {
        this.animals = animals;
        this.baseLocation = location;
        this.animals_objects = new GameObject[armySize];
    }
    public Transform BaseLocation { get => baseLocation; }
    public static int ArmySize { get => armySize; set => armySize = value; }
    public GameObject[] Animals { get => animals; }

    public GameObject getAnimal(int index) { return Animals[index]; }

    public void setAnimalObject(GameObject animal, int index) { animals_objects[index] = animal; }
    public GameObject getAnimalObject(int index) { return animals_objects[index]; }
}
