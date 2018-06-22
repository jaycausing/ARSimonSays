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
		_QuitOnConnectionErrors();
	}
	private void Start() {
		Screen.autorotateToPortrait = true;
		Screen.orientation = ScreenOrientation.AutoRotation;

	}

	/*
		Remember that the app is tracking for planes every Update() call
	 */
	private void Update() {
		_UpdateGameLifecycle();

		if(Session.Status == SessionStatus.NotTracking){
			Debug.Log("Not tracking");
			return;
		} else {
			Debug.Log("Tracking");
		}
		

		Session.GetTrackables<DetectedPlane>(l_Planes, TrackableQueryFilter.New);
		bool showSearchingUI = true;
        for (int i = 0; i < l_Planes.Count; i++){
            if (l_Planes[i].TrackingState == TrackingState.Tracking){
				//instantiate plane viz

				GameObject plane = GameObject.CreatePrimitive(PrimitiveType.Sphere);
				plane.transform.position = l_Planes[i].CenterPose.position;

				//GameObject plane = Instantiate(, Vector3.zero,
				//Quaternion.identity, transform);
				//plane.GetComponent<DetectedPlaneVizualizer>().Initialize(l_Planes[i]);
                showSearchingUI = false;
                break;
            }
        }
        searchingText.SetActive(showSearchingUI);

		Touch touch;

		//if no touch, update is done
		if (Input.touchCount < 1 || (touch = Input.GetTouch(0)).phase != TouchPhase.Began) {
            return;
        }

		//else update if plane
		TrackableHit hit;
        TrackableHitFlags raycastFilter = TrackableHitFlags.PlaneWithinPolygon |
        TrackableHitFlags.FeaturePointWithSurfaceNormal;

		if (Frame.Raycast(touch.position.x, touch.position.y, raycastFilter, out hit)){
            // Use hit pose and camera pose to check if hittest is from the
            // back of the plane, if it is, no need to create the anchor.
            if (hit.Trackable is DetectedPlane){
				Debug.Log("Plane is detected and tracking!");
				
			}
		}
	}


	void _QuitOnConnectionErrors(){

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

	void _UpdateGameLifecycle(){
		if(Session.Status != SessionStatus.Tracking){
			const int LOST_TRACKING_SLEEP_TIMEOUT = 15;
			Screen.sleepTimeout = LOST_TRACKING_SLEEP_TIMEOUT;
		} else {
			Screen.sleepTimeout = SleepTimeout.NeverSleep;
		}
	}
}
