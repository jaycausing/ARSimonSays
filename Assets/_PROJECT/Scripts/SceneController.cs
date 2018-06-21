using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore;

//taken from HelloARController
#if UNITY_EDITOR
    // Set up touch input propagation while using Instant Preview in the editor.
    using Input = InstantPreviewInput;
#endif

public class SceneController : MonoBehaviour {

	//HelloARController
	public Camera FirstPersonCamera;
	public GameObject DetectedPlanePrefab;
	private List<DetectedPlane> m_AllPlanes = new List<DetectedPlane>();
	private bool m_IsQuitting = false;

	private void Start() {
		QuitOnConnectionErrors();
	}

	private void Update() {
		UpdateGameLifecycle();

		Session.GetTrackables<DetectedPlane>(m_AllPlanes);
		bool showSearchingUI = true;
            for (int i = 0; i < m_AllPlanes.Count; i++){
                if (m_AllPlanes[i].TrackingState == TrackingState.Tracking){
                    showSearchingUI = false;
                    break;
                }
            }
            SearchingForPlaneUI.SetActive(showSearchingUI);		
	}

	void QuitOnConnectionErrors(){
		//if phones not compatible
		//if permissions not enabled
		//if cant connect to arcore services

		//if(Session.Status == SessionStatus.ErrorPermissionNotGranted)


		//call AndroidShowToastMessage() to show errors
		//Application.Quit()
	}

	void AndroidShowToastMessage(){
		//toast errors
	}

	void UpdateGameLifecycle(){
		if(Session.Status != SessionStatus.Tracking){
			const int LOST_TRACKING_SLEEP_TIMEOUT = 15;
			Screen.sleepTimeout = LOST_TRACKING_SLEEP_TIMEOUT;
			return;
		}
		Screen.sleepTimeout = SleepTimeout.NeverSleep;
	}

}
