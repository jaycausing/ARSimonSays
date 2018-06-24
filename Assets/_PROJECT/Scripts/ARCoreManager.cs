#if UNITY_EDITOR
	using Input = GoogleARCore.InstantPreviewInput;
#endif

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore;

public class ARCoreManager : MonoBehaviour {

	public ARCoreUtils Utils;
	public GameManager GameManager;

	private GameObject SceneUI;
	private GameObject GameUI;

	private bool isAppStarted;

	void Awake() {
		SceneUI = GameObject.FindWithTag("SceneUI");
		GameUI = GameObject.FindWithTag("GameUI");

		setSceneUI(true);
		setGameUI(false);

		isAppStarted = false;
	}
	
	void Start () {
		Utils.QuitOnConnectionError();
	}
	
	// Update is called once per frame
	void Update () {
		Utils.UpdateAppLifecycle();

		if(Session.Status == SessionStatus.NotTracking){
			Debug.Log("Not tracking");
			return;
		} else {
			//TODO: when player presses start button, then start tracking
			if(isAppStarted)
				Utils.TrackPlanes();
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

	void StartTrackingOnClick(){
		setSceneUI(false);
		setGameUI(true);

		isAppStarted = true;
	}

	public void setSceneUI(bool setScene) { SceneUI.SetActive(setScene); }
	public void setGameUI(bool setGame) { GameUI.SetActive(setGame); }



}
