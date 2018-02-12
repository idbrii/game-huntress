using UnityEngine.AI;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class CreatureLocomotor : MonoBehaviour
{

    Rigidbody body;
    CreatureBrain brain;
    NavMeshAgent agent;

    void Awake() {
        body = GetComponent<Rigidbody>();
        brain = GetComponent<CreatureBrain>();
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        var should_move = brain.HasDestination();
        agent.isStopped = !should_move;
        if (should_move)
        {
            agent.destination = brain.TargetDestination; 
        }
    }

    void FixedUpdate()
    {
    }

}
