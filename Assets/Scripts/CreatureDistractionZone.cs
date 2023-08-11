using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

[RequireComponent(typeof(Collider))]
public class CreatureDistractionZone : MonoBehaviour
{
    public float DistractionSeconds = 5.0f;
    public float CooldownSeconds = 30.0f;

    Collider trigger;

    void OnDrawGizmosSelected() {
        if (trigger == null) {
            trigger = GetComponent<Collider>();
        }

        var c = Color.yellow + Color.red;
        c.a = 0.1f;
        Gizmos.color = c;
        Gizmos.DrawCube(transform.position, trigger.bounds.size);
    }

    void Start() {
        trigger = GetComponent<Collider>();
    }

    void OnTriggerEnter(Collider creature_collider)
    {
        var creature = creature_collider.transform.root;
        var obj = creature.gameObject;
        if (obj && obj.CompareTag("distractable")) {
            // TODO: add Distractable and setup tag. Or apply this to CreatureBrain?
            var target = obj.GetComponent<Distractable>();
            target.StartDistraction(this, DistractionSeconds, CooldownSeconds);
        }
    }
    
}
