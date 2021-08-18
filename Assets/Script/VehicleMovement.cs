using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleMovement : MonoBehaviour
{
    private Rigidbody rigidBody;
    private bool crashed = false;
    public int speed = 0;
    public CapsuleCollider sensor;
    const int MIN_SPEED = 40;
    const int MAX_SPEED = 80;

    const int PLAYER = 3;
    const int CARS = 7;
    const int DESPAWNER = 8;


    void Start()
    {
        speed = Random.Range(MIN_SPEED, MAX_SPEED);
        rigidBody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (!crashed)
        {
            rigidBody.velocity = transform.forward * speed;
        }
        else
        {

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == CARS)
        {
            speed = MIN_SPEED - 20;
            other.gameObject.GetComponent<VehicleMovement>().speed = MIN_SPEED - 20;
        }
        else if (other.gameObject.layer == DESPAWNER)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.gameObject.layer == PLAYER)
        {
            other.collider.gameObject.GetComponent<ThirdPersonMovement>().ToggleRagdoll();
            crashed = true;
        }
        else if (other.collider.gameObject.layer == CARS)
        {
            crashed = true;
        }
    }
}