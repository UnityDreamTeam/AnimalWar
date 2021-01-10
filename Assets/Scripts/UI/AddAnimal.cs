using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddAnimal : MonoBehaviour
{
    Button myButton;
    [SerializeField] GameObject animalChooser;

    // Start is called before the first frame update
    void Start()
    {
        myButton = GetComponent<Button>();
        myButton.onClick.AddListener(animalChooser.GetComponent<AnimalsChoose>().addAnimalToArmy);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
