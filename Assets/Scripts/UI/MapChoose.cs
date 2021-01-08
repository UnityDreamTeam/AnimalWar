using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MapChoose : MonoBehaviour
{
    [SerializeField] static int numOfMaps;
    [SerializeField] Button[] maps = new Button[numOfMaps];
    private int currentMap;
    readonly int lowerBoundary = -1;
    readonly int higherBoundary = 3;

    // Start is called before the first frame update
    private void Start()
    {
        currentMap = 0;
        transform.GetChild(currentMap).gameObject.SetActive(true);
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
}