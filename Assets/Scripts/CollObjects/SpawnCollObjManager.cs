using UnityEngine;
using PoolSpawner;
using System.Collections;

namespace SpawnedCollObj
{
    public class SpawnCollObjManager : MonoBehaviour
    {   
        [SerializeField] private ObstacleObj[] obstaclesToSpawn;
        [SerializeField] private float spawnInterval = 2.0f;

        [SerializeField] private int numberOfEquallySpacedSpawnPoints = 3;

        private SpawnWithPool<ObstacleObj> obstacleSpawner;
        private System.Random rnd;

        private Vector3[] spawnerPositions;
        private int numberOfObstacleTypes;

        private int nxtObstacleSpawnId;
        private int nxtSpawnPos;

        private void Awake()
        {
            obstacleSpawner = new SpawnWithPool<ObstacleObj>();
            rnd = new System.Random();

            numberOfObstacleTypes = obstaclesToSpawn.Length;
            for (int i = 0; i < numberOfObstacleTypes; i++)
            {
                obstacleSpawner.AddPoolForGameObject(obstaclesToSpawn[i].gameObject, i);
            }

            PrepareSpawnPoints();
        }


        private void PrepareSpawnPoints()
        {
            spawnerPositions = null;

            spawnerPositions = new Vector3[numberOfEquallySpacedSpawnPoints];

            float screenHeight = Camera.main.orthographicSize * 2f;
            float screenWidth = screenHeight * Camera.main.aspect;
            float spacing = screenHeight / (numberOfEquallySpacedSpawnPoints + 1);

            Vector3 startPosition = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, 0)
                + new Vector3(screenWidth / 2f, -screenHeight / 2f + spacing, 0f);
            
            for (int i = 0; i < numberOfEquallySpacedSpawnPoints; i++)
            {
                spawnerPositions[i] = startPosition + i * new Vector3(0f, spacing, 0f);
            }
        }

        private IEnumerator SpawnObstacles()
        {
            while (true)
            {
                SpawnObstacle();

                yield return new WaitForSeconds(spawnInterval);
            }
        }

        private void SpawnObstacle()
        {
            nxtObstacleSpawnId = rnd.Next(0, numberOfObstacleTypes);
            nxtSpawnPos = rnd.Next(0, numberOfEquallySpacedSpawnPoints);

            obstacleSpawner.Spawn(spawnerPositions[nxtSpawnPos], nxtObstacleSpawnId);
        }


        #region enable/disable
        private void OnEnable()
        {
            StartCoroutine(SpawnObstacles());
        }

        private void OnDisable()
        {
            StopAllCoroutines();
        }
        #endregion


#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            if (spawnerPositions != null)
            {
                for (int i = 0; i < spawnerPositions.Length; i++)
                {
                    Gizmos.color = Color.yellow;
                    Gizmos.DrawWireSphere(spawnerPositions[i], 1);
                }
            }
        }
#endif
    }
}
