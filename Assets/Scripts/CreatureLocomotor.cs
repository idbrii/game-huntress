using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class CreatureLocomotor : MonoBehaviour
{

    [Tooltip("Max speed we can reach.")]
    public float MaxSpeed = 1.0f;

    [Tooltip("Max change in acceleration.")]
    public float MaxAcceleration = 20.0f;

    Vector3 DesiredVelocity = Vector3.zero;

    Rigidbody body;
    CreatureBrain brain;

    void Awake() {
        body = GetComponent<Rigidbody>();
        brain = GetComponent<CreatureBrain>();
    }

    void Update()
    {
        if (brain.HasDestination())
        {
            DesiredVelocity = MaxSpeed * (brain.TargetDestination - transform.position);
        }
        else
        {
            DesiredVelocity = Vector3.zero;
        }
    }

    void FixedUpdate()
    {
        var amount = Time.deltaTime * MaxAcceleration;
        body.velocity = Vector3.MoveTowards(body.velocity, DesiredVelocity, amount);
    }

}
