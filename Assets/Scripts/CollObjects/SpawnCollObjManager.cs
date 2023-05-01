using UnityEngine;
using PoolSpawner;
using System.Collections;
using Player;

namespace SpawnedCollObjects
{
    public class SpawnCollObjManager : MonoBehaviour
    {   
        [SerializeField] private SpawnedCollObj[] obstaclesToSpawn;
        [SerializeField] private SpawnedCollObj[] powerupsToSpawn;

        [Range(0,100)]
        [SerializeField] private float chanceOfPowerupSpawn;

        [SerializeField] private float spawnInterval = 2.0f;
        [SerializeField] private int numberOfEquallySpacedSpawnPoints = 3;

        private SpawnWithPool<SpawnedCollObj> objSpawner;
        private System.Random rnd;

        private Vector3[] spawnerPositions;
        private int numberOfObstacleTypes;
        private int numberOfPowerUpTypes;

        private int nxtObjSpawnId;
        private int nxtSpawnPos;


        private void Awake()
        {
            PrepareSpawner();
            PrepareSpawnPoints();
          
            rnd = new System.Random();
              
            PlayerInteractionCollision.OnGameEnd += OnGameEnd;
            UIGameManager.OnGameRestart += OnGameRestart;
        }


        #region spawning
        private void PrepareSpawner()
        {
            objSpawner = new SpawnWithPool<SpawnedCollObj>();

            numberOfObstacleTypes = obstaclesToSpawn.Length;
            for (int i = 0; i < numberOfObstacleTypes; i++)
            {
                objSpawner.AddPoolForGameObject(obstaclesToSpawn[i].gameObject, i);
            }

            numberOfPowerUpTypes = powerupsToSpawn.Length;
            for (int i = numberOfObstacleTypes; i < numberOfPowerUpTypes + numberOfObstacleTypes; i++)
            {
                objSpawner.AddPoolForGameObject(powerupsToSpawn[i - numberOfObstacleTypes].gameObject, i);
            }
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
                if (rnd.Next(0, 100) < chanceOfPowerupSpawn)
                {
                    SpawnObj(TypeOfCollObj.PowerUp);
                    yield return new WaitForSeconds(spawnInterval);
                }

                SpawnObj(TypeOfCollObj.Obstacle);

                yield return new WaitForSeconds(spawnInterval);
            }
        }

        private void SpawnObj(TypeOfCollObj _obj)
        {
            switch (_obj)
            {
                case TypeOfCollObj.Obstacle:
                    nxtObjSpawnId = rnd.Next(0, numberOfObstacleTypes);
                    break;
                case TypeOfCollObj.PowerUp:
                    nxtObjSpawnId = rnd.Next(numberOfObstacleTypes, numberOfPowerUpTypes + numberOfObstacleTypes);
                    break;
                default:
                    break;
            }

            nxtSpawnPos = rnd.Next(0, numberOfEquallySpacedSpawnPoints);
            objSpawner.Spawn(spawnerPositions[nxtSpawnPos], nxtObjSpawnId);
        }
        #endregion


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


        #region game end/restart
        private void OnGameEnd()
        {
            StopAllCoroutines();
        }

        private void OnGameRestart()
        {
            StartCoroutine(SpawnObstacles());
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

    public enum TypeOfCollObj { Obstacle, PowerUp };
}