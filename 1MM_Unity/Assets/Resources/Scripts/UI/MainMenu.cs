﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

    public CanvasGroup mainMenu, options, credits,cgToFade0,cgToFade1;
    public CanvasGroup t1,t2;
    
    public Resolution[] resolutions;
    public int currentRes, currenQual;
    public Slider resolutionSlider, graphicSlider;
    public Toggle wind;
    public Text resolutionText, currentResolution, quality, currentQuality;
    public int qualitySize, resolutionSize;
    public Text[] setting;
    public bool windowed, fade0, fade1, clickedStart;
    public float wantedTime, currentT, f0Start,f1Start;

    public CanvasGroup introduction1, introduction2, button;

    public void Awake()
    {
        Time.timeScale = 1;
        windowed = !Screen.fullScreen;
        wind.isOn = windowed;

        resolutions = Screen.resolutions;

        foreach (Resolution res in resolutions)
        {
            resolutionSize++;
            if (res.height == Screen.currentResolution.height && res.width == Screen.currentResolution.width && res.refreshRate == Screen.currentResolution.refreshRate)
                currentRes = resolutionSize - 1;
        }

        resolutionSlider.maxValue = resolutionSize - 1;
        resolutionSlider.value = currentRes;
        graphicSlider.maxValue = QualitySettings.names.Length - 1;


        currentResolution.text = "Current Resolution: \n" + Screen.currentResolution.width.ToString() + " x " + Screen.currentResolution.height.ToString();
        currentQuality.text = "Current Quality: \n" + QualitySettings.names[QualitySettings.GetQualityLevel()];
        graphicSlider.value = QualitySettings.GetQualityLevel();
    }



	void Update () {

        windowed = wind.isOn;
        resolutionText.text = resolutions[(int)resolutionSlider.value].width.ToString() + " x " + resolutions[(int)resolutionSlider.value].height.ToString();
         
        quality.text = QualitySettings.names[(int)graphicSlider.value];

        if(clickedStart)
        {

            currentT = (Time.time - f1Start) / wantedTime;

            if (fade1)
            {
                introduction1.alpha = Mathf.Lerp(0, 1, currentT);
            }


        }
	}

    public void showButton()
    {
        button.alpha = 1;
        button.interactable = true;
        button.blocksRaycasts = true;
    }

    public void OnClickStart()
    {
        clickedStart = true;
        fadeToAlpha0(Time.time, mainMenu);
        mainMenu.interactable = false;
        mainMenu.blocksRaycasts = false;

        mainMenu.gameObject.GetComponent<hide>().toggle();
        Invoke("showIntroduction", 3.0F);

        showButton();
    }

    public void showIntroduction()
    {
        fadeToAlpha1(Time.time, introduction1);
        introduction1.gameObject.GetComponent<hide>().toggle();
        Invoke("showSecondIntroduction", 7F);
    }

    public void showSecondIntroduction()
    {
        introduction2.alpha = 1;
        introduction2.gameObject.GetComponent<hide>().toggle();
    }

    public void apply()
    {
        QualitySettings.SetQualityLevel((int)graphicSlider.value);

        Screen.SetResolution(resolutions[(int)resolutionSlider.value].width, resolutions[(int)resolutionSlider.value].height, true);
        resolutionText.text = resolutions[(int)resolutionSlider.value].width + " x " + resolutions[(int)resolutionSlider.value].height;

        currentResolution.text = "Current Resolution: \n" + resolutions[(int)resolutionSlider.value].width.ToString() + " x " + resolutions[(int)resolutionSlider.value].height.ToString();
        currentQuality.text = "Current Quality: \n" + QualitySettings.names[QualitySettings.GetQualityLevel()];

        Screen.fullScreen = !wind.isOn;


    }

    public void OnClickOptions()
    {
        mainMenu.alpha = 0;
        mainMenu.interactable = false;
        mainMenu.blocksRaycasts = false;

        options.alpha = 1;
        options.interactable = true;
        options.blocksRaycasts = true;
    }

    public void OnClickCredits()
    {
        mainMenu.alpha = 0;
        mainMenu.interactable = false;
        mainMenu.blocksRaycasts = false;

        credits.alpha = 1;
        credits.interactable = true;
        credits.blocksRaycasts = true;
    }

    public void OnClickBack()
    {
        mainMenu.alpha = 1;
        mainMenu.interactable = true;
        mainMenu.blocksRaycasts = true;

        options.alpha = 0;
        options.interactable = false;
        options.blocksRaycasts = false;

        credits.alpha = 0;
        credits.interactable = false;
        credits.blocksRaycasts = false;
    }

    public void OnClickExit()
    {
        Application.Quit();
    }

    void fadeToAlpha0(float startTime, CanvasGroup cg)
    {
        cgToFade0 = cg;
        f0Start = startTime;
        fade0 = true;
    }

    void fadeToAlpha1(float startTime, CanvasGroup cg)
    {
        cgToFade1 = cg;
        f1Start = startTime;
        fade1 = true;
    }

    public void load()
    {
        Application.LoadLevel("FirstLevel");
    }

    void Transition()
    {
        fadeToAlpha0(Time.time, t1);
    }
}