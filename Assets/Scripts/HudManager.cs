using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HudManager : MonoBehaviour {

    public Text scoreLabel;
    public GameManager lvl;
    // Use this for initialization
    void Start()
    {
        Refresh();
    }

    // Show player stats in the HUD
    public void Refresh()
    {
        scoreLabel.text = "Score: " + lvl.score;
    }
}
