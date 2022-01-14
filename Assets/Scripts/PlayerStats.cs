using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public GameManager gameManager;
    
    public int deathCount = 0;
    public float playTime; // Played time in seconds

    // Update is called once per frame
    void Update()
    {
        if (!gameManager.isPaused)
        {
            playTime += Time.deltaTime;
        }
    }

    // Return the playtime in hours, min, sec and ms
    public string ReturnTime()
    {
        var time = TimeSpan.FromSeconds(playTime);

        return $"{time.Hours:D2}h:{time.Minutes:D2}m:{time.Seconds:D2}s:{time.Milliseconds:D2}";
    }
}
