using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private bool _gameover = false;
    public bool Gameover
    {
        get { return _gameover; }
        set { _gameover = value; }
    }

    private int _score = 0;
    public int Score
    {
        get { return _score; }
    }

    public static GameManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
            return;
        }
    }

    public void UpdateScore(int points)
    {
        _score = _score + points;
        UIController.Instance.ScoreText.text = Score.ToString();
    }

    public void GameOver()
    {
        Gameover = true;
        UIController.Instance.GameOverScoreText.text = Score.ToString();
        UIController.Instance.GameOverPanel.SetActive(true);
    }
}
