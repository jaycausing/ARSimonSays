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


//Used for coordinating between ARCore and Unity
public class SceneController : MonoBehaviour {

	//HelloARController
	public Camera FirstPersonCamera;
	public GameObject detectedPlanePrefab;
	public GameObject searchingText;
	private List<DetectedPlane> l_Planes = new List<DetectedPlane>();
	private bool isQuitting = false;

	private void Awake() {
		QuitOnConnectionErrors();
	}
	private void Start() {
		Screen.autorotateToPortrait = true;
		Screen.orientation = ScreenOrientation.AutoRotation;

	}

	private void Update() {
		UpdateGameLifecycle();

		Session.GetTrackables<DetectedPlane>(l_Planes);
		Debug.Log(Session.Status);
		bool showSearchingUI = true;
        for (int i = 0; i < l_Planes.Count; i++){
            if (l_Planes[i].TrackingState == TrackingState.Tracking){
                showSearchingUI = false;
                break;
            }
        }
        searchingText.SetActive(showSearchingUI);

		Touch touch;
		if (Input.touchCount < 1 || (touch = Input.GetTouch(0)).phase != TouchPhase.Began) {
            return;
        }

		TrackableHit hit;
        TrackableHitFlags raycastFilter = TrackableHitFlags.PlaneWithinPolygon |
        TrackableHitFlags.FeaturePointWithSurfaceNormal;

		if (Frame.Raycast(touch.position.x, touch.position.y, raycastFilter, out hit)){
			if (Frame.Raycast(touch.position.x, touch.position.y, raycastFilter, out hit)){
                // Use hit pose and camera pose to check if hittest is from the
                // back of the plane, if it is, no need to create the anchor.
                if ((hit.Trackable is DetectedPlane) &&
                    Vector3.Dot(FirstPersonCamera.transform.position - hit.Pose.position,
					hit.Pose.rotation * Vector3.up) < 0){
                    	Debug.Log("Hit at back of the current DetectedPlane");
                }
			}
		}


	}

	void QuitOnConnectionErrors(){

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

	void AndroidShowToastMessage(){
		//toast errors
	}

	void UpdateGameLifecycle(){
		if(Session.Status != SessionStatus.Tracking){
			const int LOST_TRACKING_SLEEP_TIMEOUT = 15;
			Screen.sleepTimeout = LOST_TRACKING_SLEEP_TIMEOUT;
		} else {
			Screen.sleepTimeout = SleepTimeout.NeverSleep;
		}
	}
}
