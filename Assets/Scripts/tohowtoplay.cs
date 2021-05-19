using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class tohowtoplay : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    public void Goto()
    { 
        SceneManager.LoadScene("howtoplay");
    }
    // Update is called once per frame
    void Update()
    {

    }
}