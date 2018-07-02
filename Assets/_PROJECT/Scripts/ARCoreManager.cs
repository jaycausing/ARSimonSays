#if UNITY_EDITOR
	using Input = GoogleARCore.InstantPreviewInput;
#endif

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using GoogleARCore;

public class ARCoreManager : MonoBehaviour {

	public ARCoreUtils Utils;
	public GameManager GameManager;

	public GameObject ARCoreDevice;

	//private GameObject SceneUI;
	//


	// holds planes app is currently tracking that frame
	private List<DetectedPlane> l_Planes = new List<DetectedPlane>();

	void Awake() {

	}

	void OnEnable() {
		
	}
	
	void Start () {
		Utils.QuitOnConnectionError();
	}
	
	void Update () {
		Utils.UpdateAppLifecycle();

		if(Session.Status == SessionStatus.NotTracking)
			return;

		if(ActivePlaneGenerator.StartTracking){
			Session.GetTrackables<DetectedPlane>(l_Planes);
			for (int i = 0; i < l_Planes.Count; i++){
				if (l_Planes[i].TrackingState == TrackingState.Tracking){
					if(GameObject.Find("SearchingUI"))
						GameObject.Find("SearchingUI").SetActive(false);
				}
			}
		}
	}


}
