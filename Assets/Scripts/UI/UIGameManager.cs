using UnityEngine;
using UnityEngine.UI;
using Player;
using System;

public class UIGameManager : MonoBehaviour
{
    [SerializeField] private Text scoreText;
    [SerializeField] private Text scoreEndResultText;
    [SerializeField] private Text scoreBestResultText;

    [SerializeField] private Button buttonRestart;
    [SerializeField] private GameObject panelPause;

    public static event Action OnGameRestart;

    private int bestScore = 0;


    private void Awake()
    {
        PlayerInteractionCollision.OnGameEnd += OnGameEnd;
        PlayerScoreCalc.OnScoreAdd += OnScoreGaines;

        buttonRestart.onClick.AddListener(() => OnButtonRestartGame());

        bestScore = PlayerPrefs.GetInt("BestScore");
        scoreBestResultText.text = "Best Score: " + bestScore.ToString();
    }


    #region Score
    private int currentScore;
    private int CurrentScore
    {
        set
        {
            if (scoreText != null)
            {
                scoreText.text = value.ToString();
            }

            currentScore = value;
        }
        get
        {
            return currentScore;
        }
    }

    private void OnScoreGaines(int _addScore)
    {
        CurrentScore += _addScore;
    }

    private void CheckBestScore()
    {
        if(currentScore > bestScore)
        {
            scoreBestResultText.text = "Best Score: " + currentScore.ToString();
            PlayerPrefs.SetInt("BestScore", currentScore);
        }
    }
    #endregion


    #region game end/restart
    private void OnGameEnd()
    {
        CheckBestScore();

        scoreEndResultText.text = "Result: " + currentScore.ToString();
        CurrentScore = 0;
        panelPause.SetActive(true);

        Time.timeScale = 0;
    }

    private void OnButtonRestartGame()
    {
        Time.timeScale = 1;

        panelPause.SetActive(false);
        OnGameRestart?.Invoke();
    }
    #endregion
}