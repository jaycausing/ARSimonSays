﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore;

public class ActivePlane : MonoBehaviour {

	private int planeCount = 0;
	private Color[] planeColors = new Color[]{
		Color.magenta,
		Color.cyan,
		Color.yellow
	};

	private DetectedPlane dPlane;

	

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
