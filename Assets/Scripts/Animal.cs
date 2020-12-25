using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Animal : IBehavior
{
    protected float currectHP;
    protected float maxHP;
    protected float damage;
    protected float shield;
    protected float walkSpeed = 3;
	protected Animator animator;

	public void attack()
    {
        
    }

    public void move()
    {
		
	}

    public float getWalkSpeed()
    {
        return this.walkSpeed;
    }
}
