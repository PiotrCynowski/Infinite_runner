using UnityEngine;

namespace GameInput
{
    public class InputManager : MonoBehaviour
    {
        private PlayerInputActions controls;
        private PlayerInputActions.PlayerActions playerActions;

        private Vector2 mouseInput;


        private void Awake()
        {
            controls = new PlayerInputActions();
            playerActions = controls.Player;
        }
    }
}
