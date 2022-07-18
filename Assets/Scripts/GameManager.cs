using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public Bird[] birdPrefabs;
    public float spawnTime;
    public int timeLimit;

    int m_curTimeLimit;

    bool m_isGameOver;
     int m_birdKilled;

    public bool IsGameOver { get => m_isGameOver; set => m_isGameOver = value; }
    public int BirdKilled { get => m_birdKilled; set => m_birdKilled = value; }



    // ko luu gia tri khi sang secens mới
    public override void Awake()
    {
        MakeSingleton(false);

        m_curTimeLimit = timeLimit;
    }

    override public void Start()
    {
        GameGUIManager.Ins.ShowGameGui(false);

        GameGUIManager.Ins.UpdateKilledCounting(m_birdKilled);
    }

    public void PlayGame()
    {
        StartCoroutine(GameSpawn());

        StartCoroutine(TimeCountDown());


        GameGUIManager.Ins.ShowGameGui(true);
    }

    IEnumerator GameSpawn()
    {
        while (!m_isGameOver)
        {
            SpawnBird();
            yield return new WaitForSeconds(spawnTime);
        }
    }

    IEnumerator TimeCountDown()
    {
        while(m_curTimeLimit > 0)
        {
            yield return new WaitForSeconds(1f);
            m_curTimeLimit--;
            if(m_curTimeLimit <= 0)
            {
                m_isGameOver = true;

                if(m_birdKilled > Pres.bestScore)
                {
                    GameGUIManager.Ins.gameDialog.UpdateDialog("NEW BEST", "BEST KILLED: x" + m_birdKilled);
                }else if(m_birdKilled < Pres.bestScore)
                {
                    GameGUIManager.Ins.gameDialog.UpdateDialog("YOUR BEST", "BEST KILLED: x" + Pres.bestScore);
                }
                Pres.bestScore = m_birdKilled;

                GameGUIManager.Ins.gameDialog.Show(true);
                GameGUIManager.Ins.CurDialog = GameGUIManager.Ins.gameDialog;

               
            }

            GameGUIManager.Ins.UpdateTimer(InToTime(m_curTimeLimit));
        }
    }

    void SpawnBird()
    {
        Vector3 spawnPos = Vector3.zero;

        float randomCheck = Random.Range(0f, 1f);

        if(randomCheck >= 0.5f)
        {
            spawnPos = new Vector3(11, Random.Range(1.5f, 4), 0);
        }
        else
        {
            spawnPos = new Vector3(-11,Random.Range(1.5f,4), 0);
        }

        if(birdPrefabs != null && birdPrefabs.Length > 0)
        {
            int randIdx = Random.Range(0, birdPrefabs.Length);

            if(birdPrefabs[randIdx] != null)
            {
                Bird birdClone = Instantiate(birdPrefabs[randIdx], spawnPos, Quaternion.identity);
            }
        }
    }

    string InToTime(int time)
    {
        float minute = Mathf.Floor(time / 60);
        float second = Mathf.RoundToInt(time % 60);

        return minute.ToString("00") + ":" + second.ToString("00");
    }

    
}
