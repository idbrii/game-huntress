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
		var position = transform.position;
		foreach (var point in StaticInterestingPoints)
		{
			if (Vector3.Distance(point.transform.position, position) > MinDistanceForNewTargetDestination)
			{
				TargetInterestPoint = point.transform;
				return;
			}
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
