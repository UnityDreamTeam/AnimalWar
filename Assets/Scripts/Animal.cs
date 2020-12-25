using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Animal : IBehavier
{
    protected float health;
    protected float damage;
    protected float shield;

    public void attack()
    {
        throw new System.NotImplementedException();
    }

    public void move()
    {
        throw new System.NotImplementedException();
    }
}
