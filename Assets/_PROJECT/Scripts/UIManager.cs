using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using GoogleARCore;
using GoogleARCore.Examples.Common;

#if UNITY_EDITOR
	using Input = GoogleARCore.InstantPreviewInput;
#endif


public class UIManager : MonoBehaviour {

    private GameObject SearchingUI;

    void Awake(){
        SearchingUI = GameObject.Find("SearchingUI");
    }

    void Start(){
        SearchingUI.SetActive(false);
    }

    public void StartTracking(){
		Destroy(GameObject.Find("StartBtn"));
		ActivePlaneGenerator.StartTracking = true;
        Debug.Log("Is tracking active? " + ActivePlaneGenerator.StartTracking);
        Debug.Log("Session status: " + Session.Status);
        SearchingUI.SetActive(true);
	}
}