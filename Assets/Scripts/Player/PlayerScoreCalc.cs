using UnityEngine;

namespace Player
{
    public class PlayerScoreCalc : MonoBehaviour
    {
        private float timeCalc;
        private bool isPlayingGameplay;

        public delegate void OnScoreGained(int _addScore);
        public static event OnScoreGained OnScoreAdd;

        private void Awake()
        {
            PlayerInteractionCollision.OnGameEnd += OnGameEnd;
            UIGameManager.OnGameRestart += OnGameRestart;
        }


        public void FixedUpdate()
        {
            if(isPlayingGameplay)
                timeCalc += Time.fixedDeltaTime;

            if (timeCalc >= 1)
            {
                OnScoreAdd?.Invoke(1);
                timeCalc = 0;
            }
        }

        #region game end/restart
        private void OnGameEnd()
        {
            isPlayingGameplay = false;
        }
        private void OnGameRestart()
        {
            isPlayingGameplay = true;
        }
        #endregion
    }
}
