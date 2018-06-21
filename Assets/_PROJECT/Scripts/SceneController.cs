using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore;

//taken from HelloARController
#if UNITY_EDITOR
    // Set up touch input propagation while using Instant Preview in the editor.
    using Input = GoogleARCore.InstantPreviewInput;
#endif

/*
Used for coordinating between ARCore and Unity
 */
public class SceneController : MonoBehaviour {

	//HelloARController
	public Camera FirstPersonCamera;
	//public GameObject DetectedPlanePrefab;
	//private List<DetectedPlane> l_AllPlanes = new List<DetectedPlane>();
	private bool isQuitting = false;

	private void Awake() {
		QuitOnConnectionErrors();
	}
	private void Start() {
		Screen.autorotateToPortrait = true;
		Screen.orientation = ScreenOrientation.AutoRotation;



		Debug.Log(Session.Status);
	}

	private void Update() {
		UpdateGameLifecycle();

		/*Session.GetTrackables<DetectedPlane>(l_AllPlanes);
		bool showSearchingUI = true;
            for (int i = 0; i < l_AllPlanes.Count; i++){
                if (l_AllPlanes[i].TrackingState == TrackingState.Tracking){
                    showSearchingUI = false;
                    break;
                }
            }
            SearchingForPlaneUI.SetActive(showSearchingUI);*/		
	}

	void QuitOnConnectionErrors(){

		/*if(SessionStatus.isError()){
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
		}*/

		
	}

	void AndroidShowToastMessage(){
		//toast errors
	}

	void UpdateGameLifecycle(){
		if(Session.Status != SessionStatus.Tracking){
			Debug.Log("Session not tracking: " + Session.Status);
			const int LOST_TRACKING_SLEEP_TIMEOUT = 15;
			Screen.sleepTimeout = LOST_TRACKING_SLEEP_TIMEOUT;
			return;
		}
		Screen.sleepTimeout = SleepTimeout.NeverSleep;
	}
}
