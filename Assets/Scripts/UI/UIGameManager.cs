using UnityEngine;
using UnityEngine.UI;
using Player;
using System;

public class UIGameManager : MonoBehaviour
{
    [SerializeField] private Text scoreText;
    [SerializeField] private Button buttonRestart;
    [SerializeField] private GameObject panelPause;

    public static event Action OnGameRestart;


    private void Awake()
    {
        PlayerInteractionCollision.OnGameEnd += OnGameEnd;
        PlayerScoreCalc.OnScoreAdd += OnScoreGaines;

        buttonRestart.onClick.AddListener(() => OnButtonRestartGame());
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
    #endregion


    private void OnGameEnd()
    {
        CurrentScore = 0;
        panelPause.SetActive(true);
    }

    private void OnButtonRestartGame()
    {
        panelPause.SetActive(false);
        OnGameRestart?.Invoke();
    }
}
