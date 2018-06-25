#if UNITY_EDITOR
	using Input = GoogleARCore.InstantPreviewInput;
#endif

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore;
using GoogleARCore.Examples.Common;

/*
* Custom library for handling core ARCore functionality
* Add to ARCoreManager GameObject
 */
public class ARCoreUtils : MonoBehaviour {

	public void QuitOnConnectionError(){
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

	public void UpdateAppLifecycle(){
		if(Session.Status != SessionStatus.Tracking){
			const int LOST_TRACKING_SLEEP_TIMEOUT = 15;
			Screen.sleepTimeout = LOST_TRACKING_SLEEP_TIMEOUT;
		} else {
			Screen.sleepTimeout = SleepTimeout.NeverSleep;
		}
	}

	public void AndroidShowToastMessage(){

	}

}
