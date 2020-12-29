using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public Transform bar;
    readonly float maxHealth = 1f;
    float hp;

    // Start is called before the first frame update
    public void Start()
    {
       bar = transform.Find("Bar");
       bar.localScale = new Vector3(maxHealth, maxHealth);
       hp = maxHealth;
    }

    public void ReduceHP(float damage)
    {
        hp -= damage;
        bar.localScale = new Vector3(hp, maxHealth);
        if (hp <= 0)
        {
            Destroy(gameObject.transform.parent.gameObject);
        }
    }
}
