using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Animal : IBehavior
{
    protected float currectHP;
    protected float maxHP;
    protected float damage;
    protected float shield;
    protected float walkSpeed = 3; //TODO fix
	protected Animator animator;
    private float rotationDegreePerSecond = 1000;

    public void attack()
    {
        
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

    public float getWalkSpeed()
    {
        return this.walkSpeed;
    }

    public float getDamage()
    {
        return this.damage;
    }
}
