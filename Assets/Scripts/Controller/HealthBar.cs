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

    public float Hp { get => hp; set => hp = value; }

    // Start is called before the first frame update
    public void Start()
    {
        Hp = maxHP = GetComponentInParent<Animal>().MaxHP;
        setInfo();
        bar = transform.Find("Bar");
        bar.localScale = new Vector3(scaleFactor, scaleFactor);
    }

    public bool ReduceHP(float damage)
    {
        bool isTargetDead = false;

        Hp -= damage;
        bar.localScale = new Vector3(Hp / maxHP, scaleFactor);
        setInfo();
        if (Hp <= 0)
        {
            Destroy(gameObject.transform.parent.gameObject);
            isTargetDead = true;
        }

        return isTargetDead;
    }

    void setInfo()
    {
        transform.Find("Info").GetComponent<TextMeshPro>().text = "HP: " + Hp + " atk: " + GetComponentInParent<Animal>().Damage;
    }

}