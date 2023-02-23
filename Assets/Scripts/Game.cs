using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    public Text timeText;
    public float maxTime;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        TimeSpan time = TimeSpan.FromSeconds(((int)(maxTime - Time.time)));
        timeText.text = $"Time: {time.ToString(@"m\:ss")}";
        if(time.TotalSeconds <= 0)
        {
            SceneManager.LoadScene(0);
        }
    }
}
