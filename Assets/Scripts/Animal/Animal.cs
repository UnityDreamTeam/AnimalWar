using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Animal : MonoBehaviour, IBehavior
{
    protected float currectHP;
    [SerializeField] private float maxHP;
    [SerializeField] protected float damage = 0.1f;
    readonly int id = -1;
    protected float shield;
    [SerializeField] protected float walkSpeed = 3;
	protected Animator animator;
    private float rotationDegreePerSecond = 1000;
    bool delayAttack = false;
    [SerializeField] float delayAttackTime = 1.8f;
    [SerializeField] Transform attackPos = null;
    [SerializeField] float attackRadius = 0.1f;
    [SerializeField] LayerMask whatIsEnemies = default;
    [SerializeField] Vector3 halfExtents = default;


    public bool attack()
    {
        bool isDead = false;

        Collider[] enemiesToDamage = Physics.OverlapBox(attackPos.position, halfExtents, Quaternion.identity, whatIsEnemies, QueryTriggerInteraction.UseGlobal);
        for (int i = 0; i < enemiesToDamage.Length; i++)
        {
            if (gameObject.tag.Equals(enemiesToDamage[i].tag)) // check if teammate
            {
                continue;
            }

            if(enemiesToDamage[i].GetComponentInChildren<HealthBar>().ReduceHP(Damage))
            {
                isDead = true;
            }
        }

        return isDead;
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

        transform.position += position * Time.deltaTime * WalkSpeed;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(attackPos.position, halfExtents);
    }

    private void Update()
    {
        if (gameObject.GetComponent<AnimalController>().enabled)
        {
            move(transform);
        }
        
        if (!delayAttack && Input.GetKeyDown(KeyCode.Space) && gameObject.GetComponent<AnimalController>().enabled)
        {
            //TODO creat animator Controll script
            gameObject.GetComponent<Animator>().SetBool("Attack", true);

            attack();
            delayAttack = true;
            StartCoroutine(attackLock());
        }

        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
    }

    IEnumerator attackLock()
    {
        yield return new WaitForSeconds(delayAttackTime);
        delayAttack = false;
    }

        public void disableMovement()
    {
        gameObject.GetComponent<Animator>().SetBool("Run", false);
    }

    public virtual int GetID()
    {
        return Id;
    }

    protected int Id { get => id; }
    public float WalkSpeed { get => walkSpeed; set => walkSpeed = value; }
    public float MaxHP { get => maxHP; set => maxHP = value; }
    public float Damage { get => damage; set => damage = value; }
}