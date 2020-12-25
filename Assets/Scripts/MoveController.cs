using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController : MonoBehaviour
{
    [SerializeField]
    Animal animal;

    private float horizontal;
    private float vertical;

    private float rotationDegreePerSecond = 1000;
    // Start is called before the first frame update
    void Start()
    {
        animal = new Elephant();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        Vector3 stickDirection = new Vector3(horizontal, 0, 0);
        Vector3 position = new Vector3(horizontal, vertical, 0);

        /*if (stickDirection.sqrMagnitude > 1)
        {
            stickDirection.Normalize();
            position.Normalize();
        }*/

        if (stickDirection != Vector3.zero)
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(stickDirection, Vector3.up), rotationDegreePerSecond * Time.deltaTime);
        transform.position += position * Time.deltaTime * animal.getWalkSpeed();
    }
}
