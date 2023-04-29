using UnityEngine;

namespace SpawnedCollObjects
{
    public class SpawnedTypeObstacles : SpawnedCollObj
    {
        [SerializeField] private float movingSpeed = 5f;


        void Update()
        {
            transform.Translate(Vector3.left * movingSpeed * Time.deltaTime);
        }
    }
}
