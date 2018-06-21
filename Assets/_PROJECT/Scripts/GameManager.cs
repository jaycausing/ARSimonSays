using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    /*
    GameManager handles the following
    - game sessions
    - if game starts or ends
    - keeps track of button lists
     */
     public Button _startBtn;
     
     void Start(){
         Button startBtn = _startBtn.GetComponent<Button>();
         startBtn.onClick.AddListener(GameStart);
     }

     public void GameStart(){
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.GetComponent<Renderer>().GetComponent<Renderer>().material.color = Color.red;
        _startBtn.enabled = false;
     }
     
}