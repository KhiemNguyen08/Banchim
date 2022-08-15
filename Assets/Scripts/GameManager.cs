using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public Bird[] bird;
    public float spawnTime;
    bool m_isGameover;
    public int timeLimit;
    int m_curTimeLimit;
    int m_birdKilled;

    public int BirdKilled { get => m_birdKilled; set => m_birdKilled = value; }
    public int CurTimeLimit { get => m_curTimeLimit; set => m_curTimeLimit = value; }

    public override void Awake()
    {
        MakeSingleton(false);
        m_curTimeLimit = timeLimit;
    }
    public override void Start()
    {
        GameGUIManager.Ins.ShowGameGUI(false);
        GameGUIManager.Ins.UpdateKillerCounting(m_birdKilled);
    }
    public void Playgame()
    {
        StartCoroutine(GameSpawn());
        StartCoroutine(TimeCountDown());
        GameGUIManager.Ins.ShowGameGUI(true);
    }
    IEnumerator TimeCountDown()
    {
        while (m_curTimeLimit>0)
        {
            yield return new WaitForSeconds(1);
            m_curTimeLimit--;
            if (m_curTimeLimit <= 0)
            {
                m_isGameover = true;

                //GameGUIManager.Ins.gameDialog.UpdateDialog("Your best", "best killer"+m_birdKilled);
                if (m_birdKilled > Prefs.bestScore)
                {
                    GameGUIManager.Ins.gameDialog.UpdateDialog("New best", "best killer"+m_birdKilled);

                }
                else
                    GameGUIManager.Ins.gameDialog.UpdateDialog("Your best", "best killer"+Prefs.bestScore);
                Prefs.bestScore = m_birdKilled;
                GameGUIManager.Ins.gameDialog.Show(true);
                GameGUIManager.Ins.CurDialog = GameGUIManager.Ins.gameDialog;

               
            }
            GameGUIManager.Ins.UpdateTimer(IntToTime(m_curTimeLimit));

        }
    }
    IEnumerator GameSpawn()
    {
        while (!m_isGameover)
        {
            SpawnBird();
            yield return new WaitForSeconds(spawnTime);
        }
    }
    void SpawnBird()
    {
        Vector3 spawnPos = Vector3.zero;
        float randCheck = Random.Range(0f, 1f);
        if (randCheck >= 0.5f)
        {
            spawnPos = new Vector3(11, Random.Range(-5, -1),0);
        }
        else
        {
            spawnPos = new Vector3(-11, Random.Range(-5, -1), 0);
        }
        if(bird != null&& bird.Length>0)
        {
            int randIdx = Random.Range(0, bird.Length);
            if(bird[randIdx] != null)
            {
                Bird birdClone = Instantiate(bird[randIdx], spawnPos, Quaternion.identity);
            }
        }
    }
    string IntToTime(int time)
    {
        float minute = Mathf.Floor(time / 60);
        float second = Mathf.RoundToInt(time % 60);
        return minute.ToString("00") + " : " + second.ToString("00");
    }
}
