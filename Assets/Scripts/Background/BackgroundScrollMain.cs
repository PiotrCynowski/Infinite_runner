using UnityEngine;
using Player;

namespace Background
{
    public class BackgroundScrollMain : MonoBehaviour
    {
       
        [SerializeField] private BackgroundScrollParalaxElement[] backgroundElements;
        [SerializeField] private Transform positionYReference;

        private bool isBackgroundMoving;
        private int elementsSize;
        private float time, camYPos;

        private void Awake()
        {
            PlayerInteractionCollision.OnGameEnd += OnGameEnd;
            UIGameManager.OnGameRestart += OnGameRestart;
        }

        private void Start()
        {
            elementsSize = backgroundElements.Length;
            isBackgroundMoving = true;
        }

        private void Update()
        {
            if (isBackgroundMoving)
            {
                time = Time.deltaTime;
                camYPos = positionYReference != null ? positionYReference.position.y : 0;

                for (int i = 0; i < elementsSize; i++)
                {
                    backgroundElements[i].PosParalaxUpdate(time, camYPos);
                }
            }
        }


        #region game end/restart
        private void OnGameEnd()
        {
            isBackgroundMoving = false;
        }

        private void OnGameRestart()
        {
            isBackgroundMoving = true;
        }
        #endregion
    }
}
