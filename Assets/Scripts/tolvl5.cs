using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class tolvl5 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    public void Goto()
    {
        if (StaticVar.score4 != 0)
        {
            SceneManager.LoadScene("lvl5");
        }
    }
    // Update is called once per frame
    void Update()
    {

    }
}