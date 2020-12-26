﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Animal : MonoBehaviour, IBehavior
{
    protected float currectHP;
    protected float maxHP;
    protected float damage;
    protected float shield;
    protected float walkSpeed = 3; //TODO fix
	protected Animator animator;
    private float rotationDegreePerSecond = 1000;


    [SerializeField]
    Transform attackPos;

    [SerializeField]
    float attackRangeX;

    [SerializeField]
    float attackRangeY;

    [SerializeField]
    LayerMask whatIsEnemies;

    public void attack()
    {
        Collider2D[] enemiesToDamage = Physics2D.OverlapBoxAll(attackPos.position, new Vector2(attackRangeX, attackRangeY), 0, whatIsEnemies);
        for (int i = 0; i < enemiesToDamage.Length; i++)
        {
            //enemiesToDamage[i].GetComponent<DamageScript>().TakeDamage(animal.getDamage()); // TODO make a script to deal damage of the enemy
        }
    }

    public void move(Transform transform)
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 stickDirection = new Vector3(horizontal, 0, 0);
        Vector3 position = new Vector3(horizontal, vertical, 0);

        /*if (stickDirection.sqrMagnitude > 1)
        {
            stickDirection.Normalize();
            position.Normalize();
        }*/

        if (stickDirection != Vector3.zero)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(stickDirection, Vector3.up), rotationDegreePerSecond * Time.deltaTime);
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
        if (gameObject.GetComponent<MoveController>().enabled)
        {
            move(transform);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            attack();
        }
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
