using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MapChoose : MonoBehaviour
{
    [SerializeField] Button[] maps;
    [SerializeField] Text infoText = null;

    private int currentMap;
    readonly int lowerBoundary = -1;
    readonly int higherBoundary = 3;
    readonly int minimumArmySize = 3;

    AnimalsChoose script;

    // Start is called before the first frame update
    private void Start()
    {
        currentMap = 0;
        transform.GetChild(currentMap).gameObject.SetActive(true);
        script = GameObject.FindGameObjectWithTag("Animal_Chooser").GetComponent<AnimalsChoose>();
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
        if (script.CountAnimals >= minimumArmySize)
        {
            SceneManager.LoadScene(currentMap + 2);
            infoText.enabled = false;
        }
        else
        {
            infoText.enabled = true;
        }
    }
}