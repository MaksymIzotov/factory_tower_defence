using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSCounter : MonoBehaviour
{
    public float updateInterval = 0.5f; //How often should the number update

    float accum = 0.0f;
    int frames = 0;
    float timeleft;
    float fps;

    GUIStyle textStyle = new GUIStyle();

    // Use this for initialization
    void Start()
    {
        Application.targetFrameRate = 120;

        timeleft = updateInterval;

        textStyle.fontStyle = FontStyle.Bold;
        textStyle.normal.textColor = Color.white;
        textStyle.fontSize = 54;
    }

    // Update is called once per frame
    void Update()
    {
        timeleft -= Time.deltaTime;
        accum += Time.timeScale / Time.deltaTime;
        ++frames;

        // Interval ended - update GUI text and start new interval
        if (timeleft <= 0.0)
        {
            // display two fractional digits (f2 format)
            fps = (accum / frames);
            timeleft = updateInterval;
            accum = 0.0f;
            frames = 0;
        }
    }

    void OnGUI()
    {
        //Display the fps and round to 2 decimals
        GUI.Label(new Rect(500, 540, 800, 200), fps.ToString("F2") + "FPS", textStyle);
    }
}
