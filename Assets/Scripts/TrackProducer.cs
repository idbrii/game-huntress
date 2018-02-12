using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

[RequireComponent(typeof(Collider))]
public class TrackProducer : MonoBehaviour
{
    [Tooltip("Prefabs for visual represntationg of tracks.")]
    public List<GameObject> TracksToGenerate;

    Collider trigger;

    void OnDrawGizmosSelected() {
        if (trigger == null) {
            trigger = GetComponent<Collider>();
        }

        var c = Color.yellow;
        c.a = 0.1f;
        Gizmos.color = c;
        Gizmos.DrawCube(transform.position, trigger.bounds.size);
    }

    void Start() {
        trigger = GetComponent<Collider>();
        Dbg.Assert(TracksToGenerate.Count > 0, "Tracks are required.");
    }

    void OnTriggerEnter(Collider creature_collider)
    {
        var creature = creature_collider.transform.root;
        if (creature) {
            var tracks_prefab = TracksToGenerate[Random.Range(0, TracksToGenerate.Count-1)];
            var tracks = GameObject.Instantiate(tracks_prefab, creature.position, creature.rotation);
        }
    }
    
}
