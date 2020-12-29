using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Animal : MonoBehaviour, IBehavior
{
    protected float currectHP;
    protected float maxHP;
    protected float damage = 0.1f;
    protected float shield;
    protected float walkSpeed = 3; //TODO fix
	protected Animator animator;
    private float rotationDegreePerSecond = 1000;

    [SerializeField]
    Transform attackPos = null;

    [SerializeField]
    float attackRangeX = 0;

    [SerializeField]
    float attackRangeY = 0;

    [SerializeField]
    LayerMask whatIsEnemies = default;

    public void attack()
    {
        Collider2D[] enemiesToDamage = Physics2D.OverlapBoxAll(attackPos.position, new Vector2(attackRangeX, attackRangeY), 0, whatIsEnemies);
        for (int i = 0; i < enemiesToDamage.Length; i++)
        {
            enemiesToDamage[i].GetComponentInChildren<HealthBar>().ReduceHP(getDamage()); // TODO make a script to deal damage of the enemy


        }
    }

    public void move(Transform transform)
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 stickDirection = new Vector3(horizontal, 0, 0);
        Vector3 position = new Vector3(horizontal, vertical, 0);

        if (stickDirection != Vector3.zero)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(stickDirection, Vector3.up), rotationDegreePerSecond * Time.deltaTime);
        }

        //Check if movement detected
        if (horizontal != 0 || vertical != 0)
        {
            gameObject.GetComponentInChildren<Animator>().SetBool("Run", true);
        }
        else
        {
            gameObject.GetComponentInChildren<Animator>().SetBool("Run", false);
        }

        transform.position += position * Time.deltaTime * getWalkSpeed();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(attackPos.position, new Vector3(attackRangeX, attackRangeY, 0));
    }

    private void Update()
    {
        if (gameObject.GetComponent<AnimalController>().enabled)
        {
            move(transform);
        }
        
        if (Input.GetKeyDown(KeyCode.Space) && gameObject.GetComponent<AnimalController>().enabled)
        {
            //TODO creat animator Controll script
            gameObject.GetComponent<Animator>().SetBool("Attack", true);

            attack();
        }
    }

    public void disableMovement()
    {
        gameObject.GetComponent<Animator>().SetBool("Run", false);
    }

    public float getWalkSpeed()
    {
        return this.walkSpeed;
    }

    public float getDamage()
    {
        return this.damage;
    }
}
