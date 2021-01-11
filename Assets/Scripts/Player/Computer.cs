using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Computer : MonoBehaviour
{
    [SerializeField] float maxDistanceFromEnemy = 2.1f;
    [SerializeField] LayerMask enemy = default;
    [SerializeField] float speed = 2;

    BattleSystem script;
    Transform [] locations;
    Transform closest;
    Vector3 deadEnemy;

    [SerializeField] float delayAttackTime = 1.8f;
    bool delayAttack = false;

    void Start()
    {
        GameObject BattleSystem = GameObject.FindGameObjectWithTag("Battle_System");
        script = BattleSystem.GetComponent<BattleSystem>();
        locations = new Transform[script.PlayerOneArmy.Animals.Length];
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
        if (script.getCurrentPlayerTurn() == BattleState.PLAYER_TWO_TURN)
        {
            InitializeBeforeTurn();
            if (closest != null)
            {
                float step = speed * Time.deltaTime;//How much to go towards the target

                Debug.Log(Vector3.Distance(transform.position, closest.position));
                if (Vector3.Distance(transform.position, closest.position) > maxDistanceFromEnemy)
                {
                    gameObject.GetComponentInChildren<Animator>().SetBool("Run", true);

                    //Vector3 positieVoor = closest.position + closest.forward * 2f;
                    transform.position = Vector3.MoveTowards(transform.position, closest.position, step);
                }
                else if (transform.position.y - closest.position.y > 0.00001f)
                {
                    //Now going up/down to be centered with the enemy
                    Vector3 upDownVector = new Vector3(transform.position.x, closest.position.y, transform.position.z);
                    transform.position = Vector3.MoveTowards(transform.position, upDownVector, step);
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
            if (script.PlayerOneArmy.getAnimalObject(i) != null)
            {
                locations[i] = script.PlayerOneArmy.getAnimalObject(i).transform;
            }
        }

        closest = findClosestAnimal(locations);
    }

    Transform findClosestAnimal(Transform[] locations)
    {
        Vector3 computerAnimalLocation = transform.position;

        float distance = int.MaxValue;
        Transform closest = locations[0];

        for (int i = 0; i < locations.Length; i++)
        {
            if (locations[i] != null)
            {
                float currentDistance = Vector3.Distance(computerAnimalLocation, locations[i].position);
                if (currentDistance < distance)
                {
                    distance = currentDistance;
                    closest = locations[i];
                }
            }
        }

        return closest;
    }
}
