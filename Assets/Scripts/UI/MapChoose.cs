using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MapChoose : MonoBehaviour
{

    public enum MAPS { BRIDGE, VOLACNO, DESERT}

    [SerializeField] Button[] maps = null;
    [SerializeField] Text infoText = null;

    MAPS currentMap;
    AnimalsChoose script;

    readonly int minimumArmySize = 3;
    readonly int mapsOffset = 2;

    // Start is called before the first frame update
    private void Start()
    {
        currentMap = MAPS.BRIDGE;

        transform.GetChild((int)currentMap).gameObject.SetActive(true);
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
        if (change + (int)currentMap >= (int)MAPS.BRIDGE && change + (int)currentMap < maps.Length)
        {
            currentMap += change;
            selectMap((int)currentMap);
        }
    }

    public void startGame()
    {
        if (script.CountAnimals >= minimumArmySize)
        {
            SceneManager.LoadScene((int)(currentMap + mapsOffset));
            infoText.enabled = false;
        }
        else
        {
            //Notify the user
            infoText.enabled = true;
        }
    }
}