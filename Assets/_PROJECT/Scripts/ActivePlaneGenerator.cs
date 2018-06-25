using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore;
using GoogleARCore.Examples.Common;

public class ActivePlaneGenerator : MonoBehaviour {

	public GameObject trackingPlanePrefab;
	public static DetectedPlane detectedPlane;
	private GameObject activePlane;
	private static bool isPlaneActive;
	private static List<DetectedPlane> l_NewPlanes = new List<DetectedPlane>();

	void Awake() {
		isPlaneActive = false;
	}
	
	void Update () {
		if (Session.Status != SessionStatus.Tracking){
			return;
		}

		Session.GetTrackables<DetectedPlane>(l_NewPlanes, TrackableQueryFilter.New);
		for (int i = 0; i < l_NewPlanes.Count; i++){
			activePlane = Instantiate(trackingPlanePrefab, l_NewPlanes[i].CenterPose.position,
				Quaternion.identity);
			activePlane.GetComponent<DetectedPlaneVisualizer>().Initialize(l_NewPlanes[i]);
			detectedPlane = l_NewPlanes[i];
			isPlaneActive = true;
		}
		
	}

	public static bool GetActivePlaneStatus(){
		return isPlaneActive;
	}

	public static DetectedPlane GetActivePlane(){
		return detectedPlane;
	}
}
