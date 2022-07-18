using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameGUIManager : Singleton<GameGUIManager>
{

    public GameObject homeGui;
    public GameObject gameGui;

    public Dialog gameDialog;
    public Dialog pauseDialog;

    public Image fireRateKilled;
    public Text timerText;
    public Text killedCountingText;


    Dialog m_curDialog;

    public Dialog CurDialog { get => m_curDialog; set => m_curDialog = value; }

    public override void Awake()
    {
        MakeSingleton(false);
    }

    public void ShowGameGui(bool isShow)
    {
        if (gameGui)
        {
            gameGui.SetActive(isShow);
        }

        if (homeGui)
        {
            homeGui.SetActive(!isShow);
        }
    }

    public void UpdateTimer(string time)
    {
        if (timerText)
        {
            timerText.text = time;
        }

    }

    public void UpdateKilledCounting(int killed)
    {
        if (killedCountingText)
        {
            killedCountingText.text = "x" + killed.ToString();
        }
    }

    public void UpdateFireRate(float rate)
    {
        if (fireRateKilled)
        {
            fireRateKilled.fillAmount = rate;
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;

        if (pauseDialog)
        {
            pauseDialog.Show(true);

            pauseDialog.UpdateDialog("GAME PAUSE", "BEST KILLED : x" + Pres.bestScore);

            m_curDialog = pauseDialog;
        }
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;

        if (m_curDialog)
        {
            m_curDialog.Show(false);
        }
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

        GameManager.Ins.PlayGame();
    }

    public void ExitGame()
    {
        ResumeGame();

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        Application.Quit();
    }
}
