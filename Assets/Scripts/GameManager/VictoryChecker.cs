using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryChecker : MonoBehaviour
{
    [SerializeField] TextMeshPro team1Win = null;
    [SerializeField] TextMeshPro team2Win = null;
    [SerializeField] GameObject timer = null;
    [SerializeField] GameObject battle_system = null;
    [SerializeField] GameObject continueBtn = null;
    GameObject teamOne;
    GameObject teamTwo;
    GameObject animalChoose = null;
    readonly int menuScene = 0;

    void Start()
    {
        teamOne = GameObject.Find("ArmyALocation");
        teamTwo = GameObject.Find("ArmyBLocation");
        animalChoose = GameObject.Find("AnimalChooser");

    }
        // Update is called once per frame
    void Update()
    {
        if (teamOne.transform.childCount == 0)
        {
            team2Win.enabled = true;
            continueBtn.SetActive(true);
            killProcesses();
        }
        else if (teamTwo.transform.childCount == 0)
        {
            team1Win.enabled = true;
            continueBtn.SetActive(true);
            killProcesses();
        } 
    }

    public void continueClick()
    {
        SceneManager.LoadScene(menuScene);
    }



    void killProcesses()
    {
        Destroy(battle_system);
        Destroy(timer);
        Destroy(animalChoose);
    }
}
