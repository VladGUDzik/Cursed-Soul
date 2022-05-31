using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Hero.Input
{
    public class HeroInputHandler : MonoBehaviour
    {
        private PlayerInput _playerInput;
        private Camera _cam;
        public Vector2 rawMovementInput { get; private set; }
        public Vector2 rawDashDirectionInput { get; private set; }
        public Vector2Int dashDirectionInput { get; private set; }
        public int normInputX { get; private set; }
        public int normInputY { get; private set; }
        public bool jumpInput { get; private set; }
        public bool jumpInputStop { get; private set; }
        public bool grabInput { get; private set; }
        public bool dashInput { get; private set; }
        public bool dashInputStop { get; private set; }
        public bool[] attackInputs { get; private set; }

        [SerializeField] 
        private float inputHoldTime =.2f;

        private float _jumpInputStartTime;
        private float _dashInputStartTime;

        private void Start()
        {
            _playerInput = GetComponent<PlayerInput>();

            var count = Enum.GetValues(typeof(CombatInputs)).Length;
            attackInputs = new bool[count];
            _cam = Camera.main;
        }

        private void Update()
        {
            CheckJumpInputHoldTime();
            CheckDashInputHoldTime();
        }

        public void OnPrimaryAttackInput(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                attackInputs[(int) CombatInputs.Primary]=true;
            }
        
            if (context.canceled)
            {
                attackInputs[(int) CombatInputs.Primary]=false;
            }
        }

        public void OnSecondaryAttackInput(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                attackInputs[(int) CombatInputs.Secondary]=true;
            }

            if (context.canceled)
            {
                attackInputs[(int) CombatInputs.Secondary]=false;
            }
        }

        public void OnMoveInput(InputAction.CallbackContext context)
        {
            rawMovementInput = context.ReadValue<Vector2>();

            normInputX = Mathf.RoundToInt(rawMovementInput.x);
            normInputY = Mathf.RoundToInt(rawMovementInput.y);
        }

        public void OnJumpInput(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                jumpInput = true;
                jumpInputStop = false;
                _jumpInputStartTime = Time.time;
            }

            if (context.canceled)
            {
                jumpInputStop = true;
            }
        }

        public void OnGrabInput(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                grabInput = true;
            }

            if (context.canceled)
            {
                grabInput = false;
            }
        }

        public void OnDashInput(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                dashInput = true;
                dashInputStop = false;
                _dashInputStartTime = Time.time;
            }
            if (context.canceled)
            {
                dashInputStop = true;
            }
        }

        public void OnDashDirectionInput(InputAction.CallbackContext context)
        {
            rawDashDirectionInput = context.ReadValue<Vector2>();

            if (_playerInput.currentControlScheme == "Keyboard")
            {
                rawDashDirectionInput = _cam.ScreenToWorldPoint(rawDashDirectionInput)-transform.position;
            }

            dashDirectionInput = Vector2Int.RoundToInt(rawDashDirectionInput.normalized);
        }

        public void UseJumpInput() => jumpInput = false; 
        public void UseDashInput() => dashInput = false;

        private void CheckJumpInputHoldTime()
        {
            if (Time.time >= _jumpInputStartTime + inputHoldTime)
            {
                jumpInput = false;
            }
        }

        private void CheckDashInputHoldTime()
        {
            if (Time.time >= _dashInputStartTime + inputHoldTime)
            {
                dashInput = false;
            }
        }
    }

    public enum CombatInputs
    {
        Primary,
        Secondary
    }
}