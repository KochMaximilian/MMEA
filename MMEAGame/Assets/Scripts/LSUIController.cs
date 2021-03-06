﻿using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LSUIController : MonoBehaviour
{
    public static LSUIController instance;

    private void Awake()
    {
        instance = this;
    }

    public Image fadeScreen;
    public float fadeSpeed;
    private bool shouldFadeToBlack, shouldFadeFromBlack;

    public GameObject levelInfoPanel;
    public TextMeshProUGUI levelName, gemsFound, gemsTarget, bestTime, timeTarget;
    
    // Start is called before the first frame update
    void Start()
    {
        FadeFromBlack();
    }

    // Update is called once per frame
    void Update()
    {
        if (shouldFadeToBlack)
        {
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.g, Mathf.MoveTowards(fadeScreen.color.a, 1f, fadeSpeed * Time.deltaTime));
            if (fadeScreen.color.a == 1f)
            {
                shouldFadeToBlack = false;
            }
        }

        if (shouldFadeFromBlack)
        {
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.g, Mathf.MoveTowards(fadeScreen.color.a, 0f, fadeSpeed * Time.deltaTime));
            if (fadeScreen.color.a == 0f)
            {
                shouldFadeFromBlack = false;
            } 
        } 
    }
    
    public void FadeToBlack()
    {
        shouldFadeToBlack = true;
        shouldFadeFromBlack = false;
    }

    public void FadeFromBlack()
    {
        shouldFadeFromBlack = true;
        shouldFadeToBlack = false;
    }

    public void ShowInfo(MapPoint levelInfo)
    { 
        levelName.text = levelInfo.levelName;
        levelInfoPanel.SetActive(true);
        gemsFound.text = "Found: " + levelInfo.gemsCollected;
        gemsTarget.text = "In Level: " + levelInfo.gemsTotal;
        timeTarget.text = "Target: " + levelInfo.targetTime + "s";
        if (levelInfo.bestTime == 0)
        {
            bestTime.text = "Best: ---";
        }
        else
        {                                                        // F1: float number with one decimal after comma
            bestTime.text = "Best: " + levelInfo.bestTime.ToString("F1") + "s";
        }
    }

    public void HideInfo()
    {
        levelInfoPanel.SetActive(false);
    }
}
