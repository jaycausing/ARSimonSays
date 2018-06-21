using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour {

	protected GameObject btnObj;
	public Color color;
	private bool isActive;
	private bool isDown;

	/*
	When starting the game
	1. Instantiate button with paired color
	2. When button is created and enabled in game, spawn button in random area on plane
	 */

	public Button(GameObject obj, String color){
		this.name = color + "Button";
		btnObj = obj;
		//set color
		//set location on tracked plane
	}

	//enabled animation on create
	//void OnEnable();

	//disabled animation before destroy
	//void OnDisable();

	//void setActive(boolean setActive) { this.isActive = setActive; }

//	void getActive() { return isActive; }

	//void selected();

}
