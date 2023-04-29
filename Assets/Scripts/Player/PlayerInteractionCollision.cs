using System;
using UnityEngine;

namespace Player
{
    public class PlayerInteractionCollision : MonoBehaviour
    {
        public static event Action OnGameEnd;

        private void OnTriggerEnter(Collider _other)
        {
            Debug.Log("trigger");

            OnGameEnd?.Invoke();


        }
    }
}
