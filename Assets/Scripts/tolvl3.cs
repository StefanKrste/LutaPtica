using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class tolvl3 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    public void Goto()
    {
        if (StaticVar.score2 != 0)
        {
            SceneManager.LoadScene("lvl3");
        }
    }
    // Update is called once per frame
    void Update()
    {

    }
}