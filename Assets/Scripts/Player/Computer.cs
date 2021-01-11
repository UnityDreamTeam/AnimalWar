using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Computer : MonoBehaviour
{
    [SerializeField] float maxDistanceFromEnemy = 2.1f;
    [SerializeField] float speed = 2;
    [SerializeField] float rotationSpeed = 2;
    [SerializeField] float delayAttackTime = 1.8f;

    BattleSystem script;
    Transform closest;
    GameObject[] animals;

    Vector3 deadEnemy;
    bool delayAttack = false;

    void Start()
    {
        GameObject BattleSystem = GameObject.FindGameObjectWithTag("Battle_System");
        script = BattleSystem.GetComponent<BattleSystem>();
        animals = new GameObject[script.PlayerOneArmy.Animals.Length];
        deadEnemy = new Vector3(int.MaxValue, int.MaxValue, int.MaxValue);
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

        closest = findClosestAnimal(animals);
    }

    Transform findClosestAnimal(GameObject[] animals)
    {
        Vector3 computerAnimalLocation = transform.position;

        float distance = int.MaxValue;
        Transform closestLocation = null;

        for (int i = 0; i < animals.Length; i++)
        {
            if (animals[i] != null)
            {
                float currentDistance = Vector3.Distance(computerAnimalLocation, animals[i].transform.position);
                if (currentDistance < distance)
                {
                    distance = currentDistance;
                    closestLocation = animals[i].transform;
                }
            }
        }

        return closestLocation;
    }

    Transform findLowestHPAnimal(GameObject[] animals)
    {
        Transform lowestLocation = null;

        float lowestHP = int.MaxValue;

        for (int i = 0; i < animals.Length; i++)
        {
            if (animals[i] != null)
            {
                float currentHP = animals[i].GetComponent<Animal>().CurrectHP;
                if (currentHP < lowestHP)
                {
                    lowestHP = currentHP;
                    lowestLocation = animals[i].transform;
                }
            }
        }

        return lowestLocation;
    }
}
