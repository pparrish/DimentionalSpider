using Common;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    [CreateAssetMenu(fileName = "PlayerControl", menuName = "EventBusses/PlayerControl")]
    public class PayerControlEventBus : ControlEventBus
    {
        private static ShipControls _shipControls;
        
        protected override void OnEnable()
        {
            base.OnEnable();
            _shipControls ??= new ShipControls();
            _shipControls.Player.Move.performed += OnMovementPerformed;
            _shipControls.Player.Move.canceled += OnMovementPerformed;
            _shipControls.Player.Fire.started += OnShootStarted;
            _shipControls.Player.Fire.canceled += OnStopShoot;
            _shipControls.Player.Turbo.started += OnTurboStarted;
            _shipControls.Player.Turbo.canceled += OnStopTurbo;
            _shipControls.Player.Enable();
        }

        public void EnableControls()
        {
            _shipControls.Player.Enable();
        }

        public void DisableControls()
        {
            _shipControls.Player.Disable();
        }

        private void OnTurboStarted(InputAction.CallbackContext context)
        {
            OnTurbo?.Invoke(true);
        }
        private void OnStopTurbo(InputAction.CallbackContext context)
        {
            OnTurbo?.Invoke(false);
        }
        
        private void OnMovementPerformed(InputAction.CallbackContext context)
        {
            OnMove?.Invoke(context.ReadValue<Vector2>());
        }

        
        private void OnShootStarted(InputAction.CallbackContext context)
        {
            ShootActive = true;
        }

        private void OnStopShoot(InputAction.CallbackContext context)
        {
            ShootActive = false;
        }
    }
}