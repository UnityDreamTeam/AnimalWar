using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Computer : MonoBehaviour
{
    [SerializeField] float maxDistanceFromEnemy = 2.1f;
    [SerializeField] LayerMask enemy = default;
    [SerializeField] float speed = 2;
    [SerializeField] float rotationSpeed = 5; //speed of turning

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

    // Update is called once per frame
    void Update()
    {
        if (script.getCurrentPlayerTurn() == BattleState.PLAYER_TWO_TURN)
        {
            InitializeBeforeTurn();

            float step = speed * Time.deltaTime;//How much to go towards the target

            Debug.Log(Vector3.Distance(transform.position, closest.position));
            if (Vector3.Distance(transform.position, closest.position) > maxDistanceFromEnemy)
            {
                gameObject.GetComponentInChildren<Animator>().SetBool("Run", true);

                //rotate to look at the player
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(closest.position - transform.position), rotationSpeed * Time.deltaTime);

                Vector3 positieVoor = closest.position + closest.forward * 2f;
                Debug.Log("transform forward is: " + transform.forward);
                transform.position = Vector3.MoveTowards(transform.position, positieVoor, step);
            }
            else if (transform.position.y - closest.position.y > 0.00001f)
            {
                //Now going up/down to be centered with the enemy
                Vector3 upDownVector = new Vector3(transform.position.x, closest.position.y, transform.position.z);
                transform.position = Vector3.MoveTowards(transform.position, upDownVector, step);

                //rotate to look at the player
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(closest.position - transform.position), rotationSpeed * Time.deltaTime);
            }
            else
            {
                gameObject.GetComponentInChildren<Animator>().SetBool("Run", false);
            }
            

            bool reached_target = Physics.Raycast(transform.position, transform.forward, maxDistanceFromEnemy, enemy);
            //If we reached the target
            if (!delayAttack && reached_target)
            {
                //If the attack killed the enemy
                gameObject.GetComponent<Animal>().attack();
                delayAttack = true;
                StartCoroutine(AttackLock());

                gameObject.GetComponent<Animator>().SetBool("Attack", true);
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
