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
            this.onReturnToPool = _returnAction;
            id = _id;
        }

        public void ReturnToPool()
        {
            throw new NotImplementedException();
        }
        #endregion


        #region visible/invisible
        private void OnBecameVisible()
        {
            isVisible = true;
        }

        private void OnBecameInvisible()
        {
            if (isVisible)
            {
                onReturnToPool?.Invoke(this, id);
            }
        }
        #endregion
    }
}
