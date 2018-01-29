using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class CreatureBrain : MonoBehaviour, InterestingPoint.InterestingInteractor
{
	[Tooltip("Tag of InterestingPoint to navigate to.")]
	public string TagToSeekOut;

	[Tooltip("Probability I will decide to keep my current InterestingPoint after an interruption.")]
	[Range(0,1)]
	public float LikelihoodToKeepTargetDestination = 0.9f;

	[Tooltip("New InterestingPoint must be this many units away.")]
	[Range(0,1000)]
	public float MinDistanceForNewTargetDestination = 10;
	
	List<GameObject> StaticInterestingPoints;

	Transform TargetInterestPoint;
	

	void OnDrawGizmos() {
		if (HasDestination())
		{
			Gizmos.color = Color.yellow;
			var radius = 1.0f;
			Gizmos.DrawSphere(TargetInterestPoint.position, radius);
		}
	}

	void Start()
	{
		Dbg.Assert(!string.IsNullOrEmpty(TagToSeekOut), "Need a destination tag!");
		StaticInterestingPoints = GameObject.FindGameObjectsWithTag(TagToSeekOut).ToList();

		PickNewInterestPoint();
	}

	public void CompleteInterestingInteraction()
	{
		PickNewInterestPoint();
	}

	void PickNewInterestPoint()
	{
		var options = new List<Transform>();
		var position = transform.position;
		foreach (var point in StaticInterestingPoints)
		{
			if (Vector3.Distance(point.transform.position, position) > MinDistanceForNewTargetDestination)
			{
				options.Add(point.transform);
			}
		}
		TargetInterestPoint = null;
		if (options.Count > 0)
		{
			TargetInterestPoint = options[Random.Range(0, options.Count)];
		}
	}

	public bool HasDestination()
	{
		return TargetInterestPoint != null;
	}

	public Vector3 TargetDestination
	{
		get
		{
			return TargetInterestPoint.position;
		}
	}

}
