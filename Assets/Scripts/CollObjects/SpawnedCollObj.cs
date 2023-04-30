using UnityEngine;
using PoolSpawner;
using System;

namespace SpawnedCollObjects
{
    public abstract class SpawnedCollObj : MonoBehaviour, IPoolable<SpawnedCollObj>
    {
        private Action<SpawnedCollObj, int> onReturnToPool;
        private bool isVisible;
        private int id;


        #region pool
        public void Initialize(Action<SpawnedCollObj, int> _returnAction, int _id)
        {
            UIGameManager.OnGameRestart += ReturnToPool;
            this.onReturnToPool = _returnAction;
            id = _id;
        }

        public void ReturnToPool()
        {
            if (isVisible)
            {
                isVisible = false;
                onReturnToPool?.Invoke(this, id);
            }
        }
        #endregion


        #region visible/invisible
        private void OnBecameVisible()
        {
            isVisible = true;
        }

        private void OnBecameInvisible()
        {               
            ReturnToPool();
        }
        #endregion
    }
}
