﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GoogleMobileAds.Api;


public class SoloGameManager : MonoBehaviour
{
    public Text replayText;
    public GameObject confettis;
    public Text score;
    public Text HighScoreText;
    public GameObject PauseMenu;
    public Slider MusicSlider;
    public Slider SFXSlider;
    int normalPlayerSpeed = 4;
    public GameObject SpeedBoostPrefab;
    public Text AdRespopnseText;
    public GameObject AdButton;
    public GameObject ReviveButton;
    public static GameObject GameOver;
    public Text P1_GameOverTextGO;
    public static string P1_GameOverText;
    public static string replaytext;
    public static GameObject GameOn;
    string gameId = "1452701";
    int lastScore;
    List<float> BoosterXPos = new List<float>();
    RewardBasedVideoAd ReviveRewardBasedVideo;


    void Start()
    {
        lastScore = PlayerPrefs.GetInt("CurrentScore");
        confettis.SetActive(false);
        HighScoreText.text = "High Score: " + PlayerPrefs.GetInt("HighScore").ToString();
        Time.timeScale = 1;
        normalPlayerSpeed = 4;
        Time.timeScale = 1;
        GameOn = GameObject.FindGameObjectWithTag("GameOn");
        GameOver = GameObject.FindGameObjectWithTag("GameOver");
        GameOver.SetActive(false);
        GameOn.SetActive(true);
        BoosterXPos.Add(-5f);
        BoosterXPos.Add(5f);
        InvokeRepeating("SpawnSpeedBoost", 10, Random.Range(10, 15));
        MusicSlider.value = PlayerPrefs.GetFloat("MusicVol");
        SFXSlider.value = PlayerPrefs.GetFloat("SFXVol");
        AdButton.SetActive(false);
        ReviveButton.SetActive(false);
        string appId = "ca-app-pub-4105711425411317~2575620706";
        MobileAds.Initialize(appId);
        ReviveRewardBasedVideo = RewardBasedVideoAd.Instance;
        ReviveRewardBasedVideo.OnAdRewarded += HandleReviveRewardBasedVideoRewarded;
        requestReviveRewardAd();
    }
    void requestReviveRewardAd()
    {

        string adUnitId = "ca-app-pub-4105711425411317/2982798909";
        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the rewarded video ad with the request.
        ReviveRewardBasedVideo.LoadAd(request, adUnitId);
    }

    public void ShowReviveRewardedAd(int i)
    {
        if (ReviveRewardBasedVideo.IsLoaded())
        {
            Debug.Log("Show Revive Ad");
            ReviveRewardBasedVideo.Show();
        }
    }

    void HandleReviveRewardBasedVideoRewarded(object sender, System.EventArgs arg)
    {

        Application.LoadLevel(Application.loadedLevelName);

    }
    void Update()
    {
        replayText.text = replaytext;
        P1_GameOverTextGO.text = P1_GameOverText;
        if (SoloP1_Controls.Health > 0)
        {
            score.text = "Your Score: " + PlayerPrefs.GetInt("CurrentScore").ToString();
        }
        else
        {
            score.text = "Your Score: " + lastScore.ToString();

        }
        if (PlayerPrefs.GetInt("CurrentScore") > PlayerPrefs.GetInt("HighScore"))
        {
            confettis.SetActive(true);
            PlayerPrefs.SetInt("HighScore", PlayerPrefs.GetInt("CurrentScore"));
            HighScoreText.text = "New High Score! : " + PlayerPrefs.GetInt("HighScore").ToString();
        }

        if (P1_GameOverText == "Game Over!")
        {
            ReviveButton.SetActive(true);
        }
    }


    public void changeMusicVolume()
    {
        PlayerPrefs.SetFloat("MusicVol", MusicSlider.value);
    }

    public void changeSFXVolume()
    {
        PlayerPrefs.SetFloat("SFXVol", SFXSlider.value);
    }

    public void ShowPause()
    {
        PauseMenu.SetActive(true);
        Time.timeScale = 0;
    }

    public void HidePauses()
    {
        Time.timeScale = 1;
        PauseMenu.SetActive(false);
    }

    public void RestartLevel()
    {
        if (P1_GameOverText == "Game Over!")
        {
            PlayerPrefs.SetInt("CurrentScore", 0);
        }
        Application.LoadLevel(Application.loadedLevelName);

    }

    public void GoHome()
    {
        PlayerPrefs.SetInt("CurrentScore", 0);
        Application.LoadLevel("Start");
    }

    void SpawnSpeedBoost()
    {
        GameObject SpeedBoostClone;
        SpeedBoostClone = Instantiate(SpeedBoostPrefab, new Vector3(BoosterXPos[Random.Range(0, BoosterXPos.Count)], Random.Range(0.5f, -0.65f), 0), Quaternion.identity) as GameObject;
    }
}
