using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Infrastructure.Services.Input
{
    [CreateAssetMenu(fileName = "PlayerInputActionReader", menuName = "Input/PlayerInputActionReader", order = 0)]
    public class PlayerInputActionReader : ScriptableObject, PlayerInputActions.IPlayerActions,
        PlayerInputActions.IUIActions
    {
        private PlayerInputActions _playerInputAction;

        public Vector2 MovementValue;
        public Vector2 LookValue;
        public event Action Attack; 
        public event Action Interact; 
        public event Action Crouch; 
        public event Action Jump; 
        public event Action Previous; 
        public event Action Next; 
        public event Action<bool> Sprint; 
        public event Action<Vector2> Navigate; 
        public event Action Submit; 
        public event Action Cancel; 
        public event Action<Vector2> Point; 
        public event Action Click; 
        public event Action RightClick; 
        public event Action MiddleClick; 
        public event Action<Vector2> ScrollWheel; 
        public event Action<Vector3> TrackedDevicePosition; 
        public event Action<Vector3> TrackedDeviceOrientation; 
        

        private void OnEnable()
        {
            if (_playerInputAction != null)
            {
                return;
            }

            _playerInputAction = new PlayerInputActions();

            _playerInputAction.UI.SetCallbacks(this);
            _playerInputAction.Enable();
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            MovementValue = context.ReadValue<Vector2>();
        }

        public void OnLook(InputAction.CallbackContext context)
        {
            LookValue = context.ReadValue<Vector2>();
        }

        public void OnAttack(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                Attack?.Invoke();
            }
        }

        public void OnInteract(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                Interact?.Invoke();
            }
        }

        public void OnCrouch(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                Crouch?.Invoke();
            }
        }

        public void OnJump(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                Jump?.Invoke();
            }
        }

        public void OnPrevious(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                Previous?.Invoke();
            }
        }

        public void OnNext(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                Next?.Invoke();
            }
        }

        public void OnSprint(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                Sprint?.Invoke(true);
            }

            if (context.canceled)
            {
                Sprint?.Invoke(false);
            }
        }

        public void OnNavigate(InputAction.CallbackContext context)
        {
            Navigate?.Invoke(context.ReadValue<Vector2>());
        }

        public void OnSubmit(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                Submit?.Invoke();
            }
        }

        public void OnCancel(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                Cancel?.Invoke();
            }
        }

        public void OnPoint(InputAction.CallbackContext context)
        {
            Point?.Invoke(context.ReadValue<Vector2>());
        }

        public void OnClick(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                Click?.Invoke();
            }
        }

        public void OnRightClick(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                RightClick?.Invoke();
            }
        }

        public void OnMiddleClick(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                MiddleClick?.Invoke();
            }
        }

        public void OnScrollWheel(InputAction.CallbackContext context)
        {
            ScrollWheel?.Invoke(context.ReadValue<Vector2>());
        }

        public void OnTrackedDevicePosition(InputAction.CallbackContext context)
        {
            TrackedDevicePosition?.Invoke(context.ReadValue<Vector3>());
        }

        public void OnTrackedDeviceOrientation(InputAction.CallbackContext context)
        {
            TrackedDeviceOrientation?.Invoke(context.ReadValue<Vector3>());
        }
    }
}