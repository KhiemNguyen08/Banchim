﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class GameGUIManager : Singleton<GameGUIManager>
{
    public GameObject homeGUI;
    public GameObject gameGUI;
    public Dialog gameDialog;
    public Dialog pauseDialog;
    public Image fireRateFilled;
    public Text timerText;
    public Text killerCountText;
    Dialog m_curDialog;

    public Dialog CurDialog { get => m_curDialog; set => m_curDialog = value; }

    public override void Awake()
    {
        MakeSingleton(false);
    }
    public void ShowGameGUI(bool isShow)
    {
        if (gameGUI)
            gameGUI.SetActive(isShow);
        if (homeGUI)
            homeGUI.SetActive(!isShow);
    }
    public void UpdateTimer(string time)
    {
        if (timerText)
            timerText.text = time;
    }
    public void UpdateKillerCounting(int killer)
    {
        if (killerCountText)
            killerCountText.text = "x"+killer.ToString();
    }
    public void UpdateFireRate(float rate)
    {
        if (fireRateFilled)
            fireRateFilled.fillAmount = rate;
    }
    public void PauseGame()
    {
        Time.timeScale = 0;
        if (pauseDialog)
            pauseDialog.Show(true);
        pauseDialog.UpdateDialog("Game Pause", "Best Killed : x" + Prefs.bestScore);
        m_curDialog = pauseDialog;
    }
    public void ResumeGame()
    {
        Time.timeScale = 1;
        if (m_curDialog)
            m_curDialog.Show(false);
    }
    public void BackToHome()
    {
        ResumeGame();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void Replay()
    {
        if (m_curDialog)
            m_curDialog.Show(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }
    public void ExitGame()
    {
        ResumeGame();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Application.Quit();
    }
}
