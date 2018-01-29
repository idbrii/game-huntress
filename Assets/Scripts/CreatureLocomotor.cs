using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class CreatureLocomotor : MonoBehaviour
{

    [Tooltip("Max speed we can reach.")]
    public float MaxSpeed = 1.0f;

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
        body.velocity = DesiredVelocity;
    }

}
