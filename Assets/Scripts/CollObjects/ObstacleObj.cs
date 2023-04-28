using UnityEngine;
using PoolSpawner;
using System;

namespace SpawnedCollObj
{
    public class ObstacleObj : MonoBehaviour, IPoolable<ObstacleObj>
    {
        [SerializeField] private float movingSpeed;

        private Action<ObstacleObj, int> onReturnToPool;
        private bool isVisible;
        private int id;


        #region pool
        public void Initialize(Action<ObstacleObj, int> _returnAction, int _id)
        {
            this.onReturnToPool = _returnAction;
            id = _id;
        }

        public void ReturnToPool()
        {
            throw new NotImplementedException();
        }
        #endregion

        void Update()
        {
            transform.Translate(Vector3.left * movingSpeed * Time.deltaTime);
        }

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
