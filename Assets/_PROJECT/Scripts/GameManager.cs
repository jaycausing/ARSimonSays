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
     public Button m_startBtn;
     Button startBtn;
     
     void Start(){
         startBtn = m_startBtn.GetComponent<Button>();
         startBtn.onClick.AddListener(GameStart);
     }

     void Update(){
         
     }

     public void GameStart(){
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.GetComponent<Renderer>().GetComponent<Renderer>().material.color = Color.red;
        Destroy(startBtn.gameObject);
     }
     
}