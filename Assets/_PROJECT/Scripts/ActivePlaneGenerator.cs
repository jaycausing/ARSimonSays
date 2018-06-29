using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore;
using GoogleARCore.Examples.Common;

public class ActivePlaneGenerator : MonoBehaviour {

	public GameObject trackingPlanePrefab;
	public static DetectedPlane detectedPlane;
	private static bool isPlaneActive;
	private static List<DetectedPlane> l_NewPlanes = new List<DetectedPlane>();

	[SerializeField]
	public static bool StartTracking = false;

	void Awake() {
		isPlaneActive = false;
	}
	
	void Update () {
		if (Session.Status != SessionStatus.Tracking){
			return;
		}

		if(StartTracking){
			Session.GetTrackables<DetectedPlane>(l_NewPlanes, TrackableQueryFilter.New);
			for (int i = 0; i < l_NewPlanes.Count; i++){
				GameObject activePlane = Instantiate(trackingPlanePrefab, Vector3.zero,
					Quaternion.identity, gameObject.transform);
				activePlane.GetComponent<DetectedPlaneVisualizer>().Initialize(l_NewPlanes[i]);
				Debug.Log("Plane spawned with area of: " + l_NewPlanes[i].ExtentX + " x " + l_NewPlanes[i].ExtentZ);
				detectedPlane = l_NewPlanes[i];
				isPlaneActive = true;
			}
		}
	}

	public static bool GetActivePlaneStatus(){
		return isPlaneActive;
	}

	public static DetectedPlane GetActivePlane(){
		return detectedPlane;
	}

}
