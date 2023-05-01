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
        [SerializeField] private GameObject shieldPU;
        [SerializeField] private GameObject ExplosionCollision;
        private bool isShield;
        private float movementBoostDuration;
        private Coroutine shieldActive;


        private void OnTriggerEnter(Collider _other)
        {
            if (_other.gameObject.TryGetComponent<IAmPowerup>(out var powerup))
            {
                UsePowerUp(powerup);
                powerup.PowerUpCollected();
                return;
            }

            if (isShield)
            {
                if (_other.gameObject.TryGetComponent<ICanBeDestroyedByShield>(out var obstacle))
                {
                    obstacle.ObstacleDestroyed();
                    PlayExlosionOnCollision(_other.transform.position);
                }

                DeactivateShield();
                return;
            }

            if (!isShield)
            {
                PlayExlosionOnCollision(_other.transform.position);
                AudioManager.Instance.PlaySound(TypeOfAudioClip.ObstacleCollision);
                OnGameEnd?.Invoke();
            }
        }


        private void UsePowerUp(IAmPowerup _powerup)
        {
            switch (_powerup.PowerUpType())
            {
                case TypeOfPowerup.Shield:
                    ActivateShieldPowerup(_powerup.PowerDuration);
                    break;
                case TypeOfPowerup.SpeedUp:
                    OnSpeedUpPU?.Invoke(_powerup.PowerDuration);
                    break;
                default:
                    break;
            }
        }


        private void PlayExlosionOnCollision(Vector3 _position)
        {
            ExplosionCollision.transform.position = _position;
            ExplosionCollision.SetActive(true);
        }


        #region PowerUp
        private void ActivateShieldPowerup(int _addTimeToPuDuration)
        {
            movementBoostDuration += _addTimeToPuDuration;
            AudioManager.Instance.PlaySound(TypeOfAudioClip.ShieldStart);

            if (shieldActive == null)
            {
                shieldActive = StartCoroutine(ShieldActive());
            }
        }

        private IEnumerator ShieldActive()
        {
            isShield = true;
            shieldPU.SetActive(true);      

            while (movementBoostDuration > 0)
            {
                movementBoostDuration -= Time.deltaTime;
                yield return null;
            }

            DeactivateShield();
        }

        private void DeactivateShield()
        {
            StopCoroutine(shieldActive);
            isShield = false;
            shieldPU.SetActive(false);
            AudioManager.Instance.PlaySound(TypeOfAudioClip.ShieldEnd);
            shieldActive = null;
        }
        #endregion


#if UNITY_EDITOR
        public void EditorTestPowerup(TypeOfPowerup powerup)
        {
            switch (powerup)
            {
                case TypeOfPowerup.Shield:
                    ActivateShieldPowerup(3);
                    break;
                case TypeOfPowerup.SpeedUp:
                    OnSpeedUpPU?.Invoke(3);
                    break;
                default:
                    break;
            }
        }

        public void EditorTestGameOver()
        {
            OnGameEnd?.Invoke();
        }
#endif
    }
}


interface IAmPowerup
{
    int PowerDuration { get; }
    TypeOfPowerup PowerUpType();
    void PowerUpCollected();
}

interface ICanBeDestroyedByShield
{
    void ObstacleDestroyed();
}