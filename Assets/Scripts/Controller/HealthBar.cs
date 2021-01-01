using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    Transform bar;
    float scaleFactor = 1.0f; //Since scaling is maximum 1
    float maxHP;
    float hp; //Current animal's HP

    // Start is called before the first frame update
    public void Start()
    {
        hp = maxHP = GetComponentInParent<Animal>().MaxHP;
        bar = transform.Find("Bar");
        bar.localScale = new Vector3(scaleFactor, scaleFactor);
    }

    public void ReduceHP(float damage)
    {
        hp -= damage;
        bar.localScale = new Vector3(hp / maxHP, scaleFactor);
        if (hp <= 0)
        {
            Destroy(gameObject.transform.parent.gameObject);
        }
    }
}
