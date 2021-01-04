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

    public void ReduceHP(float damage)
    {
        hp -= damage;
        bar.localScale = new Vector3(hp / maxHP, scaleFactor);
        setInfo();
        if (hp <= 0)
        {
            Destroy(gameObject.transform.parent.gameObject);
        }
    }

    void setInfo()
    {
        transform.Find("Info").GetComponent<TextMeshPro>().text = "HP: " + hp + " atk: " + GetComponentInParent<Animal>().Damage;
    }

    private void Update()
    {
        //TODO fix rotation of the text
        //transform.Find("Info").transform.Rotate(new Vector3(0, gameObject.transform.parent.rotation.y - 90, 0));
    }
}