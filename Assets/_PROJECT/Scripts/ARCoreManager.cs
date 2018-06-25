#if UNITY_EDITOR
	using Input = GoogleARCore.InstantPreviewInput;
#endif

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using GoogleARCore;

public class ARCoreManager : MonoBehaviour {

	public ARCoreUtils Utils;
	public GameManager GameManager;

	public GameObject ARCoreDevice;

	private GameObject SceneUI;
	private GameObject GameUI;


	// holds planes app is currently tracking that frame
	private List<DetectedPlane> l_Planes = new List<DetectedPlane>();

	void Awake() {
		SceneUI = GameObject.FindWithTag("SceneUI");
		GameUI = GameObject.FindWithTag("GameUI");

		setSceneUI(true);
		setGameUI(false);
	}
	
	void Start () {
		Utils.QuitOnConnectionError();
	}
	
	// Update is called once per frame
	void Update () {
		Utils.UpdateAppLifecycle();

		if(Session.Status == SessionStatus.NotTracking)
			return;

		Session.GetTrackables<DetectedPlane>(l_Planes);
		for (int i = 0; i < l_Planes.Count; i++){
            if (l_Planes[i].TrackingState == TrackingState.Tracking){
				setGameUI(false);
			}
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
				Debug.Log("Touch position: " + hit.Pose.position);
				Debug.Log("Distance from player: " + hit.Distance);
			}
		}

	}

	public void StartGameOnClick(){
		setSceneUI(false);
		setGameUI(true);
	}

	public void setSceneUI(bool setScene) { SceneUI.SetActive(setScene); }
	public void setGameUI(bool setGame) { GameUI.SetActive(setGame); }



}
