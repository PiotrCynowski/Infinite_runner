using UnityEngine;
using Player;

namespace GameInput
{
    public class InputManager : MonoBehaviour
    {
        [SerializeField] private PlayerMouseMovement Player;

        private PlayerInputActions controls;
        private Vector2 mouseInput;


        private void Awake()
        {
            controls = new PlayerInputActions();
            controls.Player.MousePosY.performed += ctx => mouseInput = ctx.ReadValue<Vector2>();

            PlayerInteractionCollision.OnGameEnd += OnGameEnd;
            UIGameManager.OnGameRestart += OnGameRestart;
        }

        private void Update()
        {
            Player.ReceiveInput(mouseInput);
        }


        #region enable/disable
        private void OnEnable()
        {
            controls.Enable();
            Player.enabled = true;
        }

        private void OnDisable()
        {
            controls.Disable();
            Player.enabled = false;
        }
        #endregion


        #region game end/restart
        private void OnGameEnd()
        {
            controls.Disable();
            Player.enabled = false;
        }

        private void OnGameRestart()
        {
            controls.Enable();
            Player.enabled = true;
        }
        #endregion
    }
}
