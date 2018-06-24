﻿#if UNITY_EDITOR
	using Input = GoogleARCore.InstantPreviewInput;
#endif

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore;
using GoogleARCore.Examples.Common;

/*
* Custom library for handling core ARCore functionality
* Add to ARCoreManager GameObject
 */
public class ARCoreUtils : MonoBehaviour {

	private List<DetectedPlane> l_Planes = new List<DetectedPlane>();

	GameObject GameUI;
	
	// Tracking Plane prefab
	public GameObject trackingPlane;

	// if true, will create multiple active planes
	// else only track first active plane
	//public bool isMultipleActivePlanes;

	//FIXME: take care of subsuming DetectedPlane objects
	//FIXME: why isnt plane tracking on my desk or floor

	public void Awake() {
		GameUI = GameObject.FindWithTag("GameUI");
	}
	public void TrackPlanes(){

		Session.GetTrackables<DetectedPlane>(l_Planes, TrackableQueryFilter.New);
		bool showSearchingUI = true;
        for (int i = 0; i < l_Planes.Count; i++){
            if (l_Planes[i].TrackingState == TrackingState.Tracking){
				
				/*Anchor planeAnchor = l_Planes[i].CreateAnchor(l_Planes[i].CenterPose);
				//instantiate plane viz
				GameObject orb = GameObject.CreatePrimitive(PrimitiveType.Sphere);
				Instantiate(orb, planeAnchor.transform);*/

				// Gonna try to use DetectedPlaneVisualizer

				GameObject activePlane = Instantiate(trackingPlane, l_Planes[i].CenterPose.position,
				Quaternion.identity, transform);
				Debug.Log("Instantiated plane");

				activePlane.GetComponent<DetectedPlaneVisualizer>().Initialize(l_Planes[i]);
				
                showSearchingUI = false;
            }
			GameUI.SetActive(showSearchingUI);
		}
	}

	public void QuitOnConnectionError(){
		if(SessionStatusExtensions.IsError(Session.Status)){
			SessionStatus status = Session.Status;

			switch (status){
				case SessionStatus.ErrorPermissionNotGranted:
					AndroidShowToastMessage();
					Application.Quit();
					break;
				case SessionStatus.ErrorSessionConfigurationNotSupported:
					AndroidShowToastMessage();
					Application.Quit();
					break;
				default:
					AndroidShowToastMessage();
					Application.Quit();
					break;
			}
		}
	}

	public void UpdateAppLifecycle(){
		if(Session.Status != SessionStatus.Tracking){
			const int LOST_TRACKING_SLEEP_TIMEOUT = 15;
			Screen.sleepTimeout = LOST_TRACKING_SLEEP_TIMEOUT;
		} else {
			Screen.sleepTimeout = SleepTimeout.NeverSleep;
		}
	}

	public void AndroidShowToastMessage(){

	}

}
