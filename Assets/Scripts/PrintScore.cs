using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrintScore : MonoBehaviour
{
    public Text scoreLabel1;
    public Text scoreLabel2;
    public Text scoreLabel3;
    public Text scoreLabel4;
    public Text scoreLabel5;
    public StaticVar readscore;

    // Start is called before the first frame update
    void Start()
    {
        scoreLabel1.text = "Score: " + StaticVar.score1;
        scoreLabel2.text = "Score: " + StaticVar.score2;
        scoreLabel3.text = "Score: " + StaticVar.score3;
        scoreLabel4.text = "Score: " + StaticVar.score4;
        scoreLabel5.text = "Score: " + StaticVar.score5;
    }
   
    // Update is called once per frame
    void Update()
    {
        
    }
}
