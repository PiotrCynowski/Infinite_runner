using UnityEngine;

namespace SpawnedCollObjects
{
    public class SpawnedTypeObstacles : SpawnedCollObj, ICanBeDestroyedByShield
    {
        [SerializeField] private int movingSpeedMin = 3;
        [SerializeField] private int movingSpeedMax = 5;
 
        [SerializeField] private Vector3 rotatationVector = Vector3.one;

        private int decidedSpeed;
        private float rotatationAngle;

        private System.Random rnd;

       
        private void Awake()
        {
            rnd = new System.Random();
        }

        void OnEnable()
        {
            decidedSpeed = rnd.Next(movingSpeedMin, movingSpeedMax);
            rotatationAngle = decidedSpeed * 0.2f;
        }

        void Update()
        {
            transform.Translate(Vector3.left * decidedSpeed * Time.deltaTime, Space.World);

            transform.Rotate(rotatationVector, rotatationAngle, Space.World);
        }


        public void ObstacleDestroyed()
        {
            ReturnToPool();
        }
    }
}