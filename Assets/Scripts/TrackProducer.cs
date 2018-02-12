using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

[RequireComponent(typeof(Collider))]
public class TrackProducer : MonoBehaviour
{
    [System.Serializable]
    public struct TagToTrack
    {
        public string ForThisTag;
        public GameObject TracksToGenerate;
    }

    [Tooltip("Prefabs for visual representation of tracks for each kind of actor.")]
    public List<TagToTrack> TracksToGenerate;

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
            var obj = creature.gameObject;
            var tracks_prefab = TracksToGenerate
                .Where(pair => obj.CompareTag(pair.ForThisTag))
                .Select(pair => pair.TracksToGenerate)
                .First();
            // Could randomly select from results intead.
            // [Random.Range(0, TracksToGenerate.Count-1)];
            Dbg.Assert(tracks_prefab != null, string.Format("Failed to find tracks for creature tag {0}", obj.tag));
            var tracks = GameObject.Instantiate(tracks_prefab, creature.position, creature.rotation);
        }
    }
    
}
