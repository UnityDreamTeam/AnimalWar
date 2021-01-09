using System.Collections;
using System.Collections.Generic;
using TMPro;
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
        setInfo();
        bar = transform.Find("Bar");
        bar.localScale = new Vector3(scaleFactor, scaleFactor);
    }

    public bool ReduceHP(float damage)
    {
        bool isTargetDead = false;

        hp -= damage;
        bar.localScale = new Vector3(hp / maxHP, scaleFactor);
        setInfo();
        if (hp <= 0)
        {
            Destroy(gameObject.transform.parent.gameObject);
            isTargetDead = true;
        }

        return isTargetDead;
    }

    void setInfo()
    {
        transform.Find("Info").GetComponent<TextMeshPro>().text = "HP: " + hp + " atk: " + GetComponentInParent<Animal>().Damage;
    }
}