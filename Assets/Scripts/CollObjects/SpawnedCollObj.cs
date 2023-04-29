using UnityEngine;
using PoolSpawner;
using System;

namespace SpawnedCollObjects
{
    public class SpawnedCollObj : MonoBehaviour, IPoolable<SpawnedCollObj>
    {
        [SerializeField] private float movingSpeed;

        private Action<SpawnedCollObj, int> onReturnToPool;
        private bool isVisible;
        private int id;


        void Update()
        {
            transform.Translate(Vector3.left * movingSpeed * Time.deltaTime);
        }


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
