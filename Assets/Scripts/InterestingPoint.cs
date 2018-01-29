using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class InterestingPoint : MonoBehaviour
{
    public interface InterestingInteractor
    {
        void CompleteInterestingInteraction();
    }

    public delegate void InteractionCompletionHandler(InterestingPoint interacted);
    InteractionCompletionHandler Callback;
    
    public void RegisterInteractor(InteractionCompletionHandler actor)
    {
        Callback = actor;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        var radius = 10.0f;
        Gizmos.DrawSphere(transform.position, radius);
    }

    void Awake() {
        // Init variables here
    }
    void Start() {
        // First update happens here
    }

    void OnTriggerEnter(Collider interactor)
    {
        var actor = interactor.gameObject.GetComponent<InterestingInteractor>();
        if (actor != null) {
            actor.CompleteInterestingInteraction();
        }
    }
}
