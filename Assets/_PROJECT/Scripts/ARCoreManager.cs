#if UNITY_EDITOR
	using Input = GoogleARCore.InstantPreviewInput;
#endif

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore;

public class ARCoreManager : MonoBehaviour {

	public ARCoreUtils utils;

	// Start tracking for planes
	private bool isTracking = false;

	private GameObject SceneUI;
	private GameObject GameUI;

	void Awake() {
		SceneUI = GameObject.FindWithTag("SceneUI");
		GameUI = GameObject.FindWithTag("GameUI");

		SceneUI.SetActive(false);
		//GameUI.SetActive(true);
	}
	
	void Start () {
		utils.QuitOnConnectionError();
	}
	
	// Update is called once per frame
	void Update () {
		utils.UpdateAppLifecycle();

		if(Session.Status == SessionStatus.NotTracking){
			Debug.Log("Not tracking");
			return;
		} else {
			utils.TrackPlanes();
		}

		Touch touch;

		if(Input.touchCount < 1 || (touch = Input.GetTouch(0)).phase != TouchPhase.Began)
			return;
		
		TrackableHit hit;
        TrackableHitFlags raycastFilter = TrackableHitFlags.PlaneWithinPolygon |
		TrackableHitFlags.FeaturePointWithSurfaceNormal;
		
		if (Frame.Raycast(touch.position.x, touch.position.y, raycastFilter, out hit)){
			if (hit.Trackable is DetectedPlane){
				Debug.Log("Plane is detected and tracking!");
			}
		}

	}

	public void startTracking(){
		isTracking = true;
	}

	public void stopTracking(){
		isTracking = false;
	}
}
