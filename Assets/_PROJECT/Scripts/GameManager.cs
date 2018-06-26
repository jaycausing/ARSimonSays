using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore;
using GoogleARCore.Examples.Common;

public class GameManager : MonoBehaviour {
    /*
    GameManager handles the following
    - creating and destroying game sessions
    - allocating data between Player/Simon and session
    - player score
     */
    
    //ITS CAUSE THIS NEVER BECAME A THING
    public static GameSession activeSession = null;
    public GameObject SessionPrefab;
    bool isGameStarted;
    public static DetectedPlane activePlane;
    public static TrackableHit spawnHit;
    
    void Awake(){
        isGameStarted = false;
    }
    void Update() {
        /*if(ActivePlaneGenerator.GetActivePlaneStatus() &&
        activeSession == null){
            Touch touch;

            if(Input.touchCount < 1 || (touch = Input.GetTouch(0)).phase != TouchPhase.Began)
			    return;
            
            touch = Input.GetTouch(0);

            Debug.Log("Touching");

            TrackableHit hit;
            TrackableHitFlags raycastFilter = TrackableHitFlags.PlaneWithinPolygon |
		    TrackableHitFlags.FeaturePointWithSurfaceNormal;

            if (Frame.Raycast(touch.position.x, touch.position.y, raycastFilter, out hit)){
                if (hit.Trackable is DetectedPlane){
                    GameStart(ActivePlaneGenerator.GetActivePlane(), hit.Pose);
                }
            }
        }*/
    }

    public void GameStart(DetectedPlane dp, TrackableHit h){
        activePlane = dp;
        spawnHit = h;
        activeSession = Instantiate(SessionPrefab).GetComponent<GameSession>();
    }

    public void GameEnd(){
         Destroy(activeSession);
    }

    public void GameRestart(){
         GameEnd();
         GameStart(activePlane, spawnHit);
    }
     
}