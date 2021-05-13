using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Home : MonoBehaviour{
    // Start the game
    public void StartGame(){
        SceneManager.LoadScene("game");
    }
}
