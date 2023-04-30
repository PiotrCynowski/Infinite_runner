using System;
using System.Collections;
using UnityEngine;

namespace Player
{
    public class PlayerInteractionCollision : MonoBehaviour
    {
        public static event Action OnGameEnd;

        public delegate void OnPowerupSpeedUp(int _puDuration);
        public static event OnPowerupSpeedUp OnSpeedUpPU;

        [Header("PowerUp")]
        private bool isShield;
        private float movementBoostDuration;
        private Coroutine movementBoost;

        private void OnTriggerEnter(Collider _other)
        {
            IAmPowerup powerup = _other.gameObject.GetComponent<IAmPowerup>();

            if (powerup != null)
            {
                UsePowerUp(powerup);
                powerup.PowerUpCollected();
                return;
            }

            if(!isShield)
                OnGameEnd?.Invoke();
        }


        private void UsePowerUp(IAmPowerup _powerup)
        {
            switch (_powerup.PowerUpType())
            {
                case typeOfPowerup.Shield:
                    EnableShieldPowerup(_powerup.powerDuration);
                    break;
                case typeOfPowerup.SpeedUp:
                    OnSpeedUpPU?.Invoke(_powerup.powerDuration);
                    break;
                default:
                    break;
            }
        }


        #region PowerUp
        private void EnableShieldPowerup(int _addTimeToPuDuration)
        {
            movementBoostDuration += _addTimeToPuDuration;

            if (movementBoost == null)
            {
                movementBoost = StartCoroutine(MovementBoostRoutine());
            }
        }

        private IEnumerator MovementBoostRoutine()
        {
            isShield = true;

            while (movementBoostDuration > 0)
            {
                movementBoostDuration -= Time.deltaTime;
                yield return null;
            }

            isShield = false;
        }
        #endregion
    }
}


interface IAmPowerup
{
    int powerDuration { get; }
    typeOfPowerup PowerUpType();
    void PowerUpCollected();
}