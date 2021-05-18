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
    public GameObject lvl2Lock;
    public GameObject lvl3Lock;
    public GameObject lvl4Lock;
    public GameObject lvl5Lock;


    // Start is called before the first frame update
    void Start()
    {
        scoreLabel1.text = "Score: " + StaticVar.score1;
        scoreLabel2.text = "Score: " + StaticVar.score2;
        scoreLabel3.text = "Score: " + StaticVar.score3;
        scoreLabel4.text = "Score: " + StaticVar.score4;
        scoreLabel5.text = "Score: " + StaticVar.score5;
        if (StaticVar.score1 == 0)
        {
            lvl2Lock.SetActive(true);
        }
        if (StaticVar.score2 == 0)
        {
            lvl3Lock.SetActive(true);
        }
        if (StaticVar.score3 == 0)
        {
            lvl4Lock.SetActive(true);
        }
        if (StaticVar.score4 == 0)
        {
            lvl5Lock.SetActive(true);
        }

    }
   
    // Update is called once per frame
    void Update()
    {
        
    }
}
