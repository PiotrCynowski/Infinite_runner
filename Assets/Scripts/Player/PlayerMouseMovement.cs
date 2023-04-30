using System.Collections;
using UnityEngine;

namespace Player
{
    public class PlayerMouseMovement : MonoBehaviour
    {
        [SerializeField] private float movementSpeed = 5f;
        [SerializeField] private float playerPosScreenEdge = 15f;
        private Vector3 mousePos;
        private float newYPos;
        private float screenTop, screenBottom;

        [Header("PowerUp")]
        private float movementBoostDuration;
        private Coroutine movementBoost;

        private void Start()
        {
            screenTop = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height- playerPosScreenEdge, 0)).y;
            screenBottom = Camera.main.ScreenToWorldPoint(new Vector3(0, playerPosScreenEdge, 0)).y;
        }

        private void Update()
        {
            Vector3 currentPos = transform.position;
            newYPos = Mathf.MoveTowards(currentPos.y, mousePos.y, movementSpeed * Time.deltaTime);

            newYPos = Mathf.Clamp(newYPos, screenBottom, screenTop);
            transform.position = new Vector3(currentPos.x, newYPos, currentPos.z);
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

            movementSpeed *= 0.5f;
            AudioManager.Instance.PlaySound(TypeOfAudioClip.SpeedUpStop);
            movementBoost = null;
        }
        #endregion


        #region enable/disable
        private void OnEnable()
        {
            PlayerInteractionCollision.OnSpeedUpPU += ActivatePowerup;
        }

        private void OnDisable()
        {
            if(movementBoost !=null)
                StopCoroutine(movementBoost);
            
            PlayerInteractionCollision.OnSpeedUpPU -= ActivatePowerup;
        }
        #endregion
    }
}
