using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnityChanSceneDirector : MonoBehaviour
{
    public Text logText;

    // Start is called before the first frame update
    void Start()
    {
        this.logText.text = "Start Unity-Chan Control Demo.";    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void WriteLog(string log) {
        this.logText.text = log;
    }
}
