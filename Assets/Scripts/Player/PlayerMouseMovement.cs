using System.Collections;
using UnityEngine;

namespace Player
{
    public class PlayerMouseMovement : MonoBehaviour
    {
        [SerializeField] private float movementSpeed = 5f;
        [SerializeField] private float playerPosScreenEdge = 15f;
        private Vector3 mousePos;
        private float mouseYPos, newYPos;
        private float screenTop, screenBottom;

        private Vector3 dir;
        private float angle;

        [Header("PowerUp")]
        [SerializeField] private ParticleSystem particlesSpeedUpPU;
        private float movementBoostDuration;
        private Coroutine movementBoost;


        private void Start()
        {
            screenTop = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height- playerPosScreenEdge, 0)).y;
            screenBottom = Camera.main.ScreenToWorldPoint(new Vector3(0, playerPosScreenEdge, 0)).y;
        }

        private void Update()
        {
            #region movement
            Vector3 currentPos = transform.position;
            mouseYPos = Mathf.Clamp(mousePos.y, screenBottom, screenTop);
            newYPos = Mathf.MoveTowards(currentPos.y, mouseYPos, movementSpeed * Time.deltaTime);

            transform.position = new Vector3(currentPos.x, newYPos, currentPos.z);
            #endregion

            #region rotation
            dir = new Vector3(0, mouseYPos, currentPos.z) - transform.position;
            angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            #endregion
        }


        public void ReceiveInput(Vector2 _mousePos)
        {
            mousePos = Camera.main.ScreenToWorldPoint(_mousePos);
        }


        #region PowerUp
        private void ActivatePowerup(int _addTimeToPuDuration)
        {
            movementBoostDuration += _addTimeToPuDuration;
            AudioManager.Instance.PlaySound(TypeOfAudioClip.SpeedUpStart);
            particlesSpeedUpPU.Play();

            if (movementBoost == null)
            {
                movementBoost = StartCoroutine(MovementBoostRoutine());
            }
        }

        private IEnumerator MovementBoostRoutine()
        {         
            movementSpeed *= 2;

            while (movementBoostDuration > 0)
            {
                movementBoostDuration -= Time.deltaTime;
                yield return null;
            }

            StopMovementBoost();
            AudioManager.Instance.PlaySound(TypeOfAudioClip.SpeedUpStop);
            
        }

        private void StopMovementBoost()
        {
            movementSpeed *= 0.5f;         
            movementBoost = null;
            particlesSpeedUpPU.Stop();
        }
        #endregion


        #region enable/disable
        private void OnEnable()
        {
            PlayerInteractionCollision.OnSpeedUpPU += ActivatePowerup;
        }

        private void OnDisable()
        {
            if (movementBoost != null)
            {
                StopCoroutine(movementBoost);
                StopMovementBoost();
            }


            PlayerInteractionCollision.OnSpeedUpPU -= ActivatePowerup;
        }
        #endregion
    }
}
