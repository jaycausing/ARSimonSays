using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    /*
    GameManager handles the following
    - creating and destroying game sessions
    - allocating data between Player/Simon and session
    - player score
     */

    GameSession session;
     
     public void GameStart(){
         session = Instantiate(new GameSession());
     }

     public void GameEnd(){
         Destroy(session);
     }

     public void GameRestart(){
         GameEnd();
         GameStart();
     }
     
}