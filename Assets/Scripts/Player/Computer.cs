using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Computer : MonoBehaviour
{
    enum ComputerLevel {EASY, MEDIUM}

    [SerializeField] float maxDistanceFromEnemy = 2.1f;
    [SerializeField] float speed = 2;
    [SerializeField] float rotationSpeed = 2;
    [SerializeField] float delayAttackTime = 1.8f;

    AnimalsChoose acScript;
    IAIBehavior targetFinder;
    BattleSystem script;

    Transform closest;
    GameObject[] animals;

    Vector3 deadEnemy;
    bool delayAttack = false;

    void Start()
    {
        GameObject BattleSystem = GameObject.FindGameObjectWithTag("Battle_System");

        script = BattleSystem.GetComponent<BattleSystem>();
        acScript = GameObject.FindGameObjectWithTag("Animal_Chooser").GetComponent<AnimalsChoose>();

        animals = new GameObject[script.PlayerOneArmy.Animals.Length];
        deadEnemy = new Vector3(int.MaxValue, int.MaxValue, int.MaxValue);

        if(acScript.Difficult == (int)ComputerLevel.EASY)
        {
            targetFinder = new ClosestAnimal();
        }
        else if(acScript.Difficult == (int)ComputerLevel.MEDIUM)
        {
            targetFinder = new LowestHPAnimal();
        }
    }

    private void FixedUpdate()
    {
        //Update computer's animal roatation
        if (script.isComputerTurn())
        {
            if (closest != null)
            {
                Vector3 relativePos = closest.position - transform.position;
                Quaternion toRotation = Quaternion.LookRotation(relativePos);
                transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (script.isComputerTurn())
        {
            InitializeBeforeTurn();
            if (closest != null)
            {
                float step = speed * Time.deltaTime;//How much to go towards the target

                if (Vector3.Distance(transform.position, closest.position) > maxDistanceFromEnemy)
                {
                    gameObject.GetComponentInChildren<Animator>().SetBool("Run", true);

                    transform.position = Vector3.MoveTowards(transform.position, closest.position, step);
                }
                else
                {
                    gameObject.GetComponentInChildren<Animator>().SetBool("Run", false);

                    //If we reached the target
                    if (!delayAttack)
                    {
                        //If the attack killed the enemy
                        gameObject.GetComponent<Animal>().attack();
                        delayAttack = true;
                        StartCoroutine(AttackLock());

                        gameObject.GetComponent<Animator>().SetBool("Attack", true);
                    }
                }
            }
        }
    }

    IEnumerator AttackLock()
    {
        yield return new WaitForSeconds(delayAttackTime);
        delayAttack = false;
    }

    void InitializeBeforeTurn()
    {
        for (int i = 0; i < script.PlayerOneArmy.Animals.Length; i++)
        {
            animals[i] = script.PlayerOneArmy.getAnimalObject(i);
        }

        closest = targetFinder.findTarget(transform, animals);
    }
}
