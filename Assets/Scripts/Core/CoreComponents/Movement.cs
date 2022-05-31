using UnityEngine;

namespace Core.CoreComponents
{
    public class Movement : CoreComponent
    {
        public Rigidbody2D rb { get; private set; }

        public int facingDirection { get; private set; }
        public bool CanSetVelocity { get; set; }
        public Vector2 currentVelocity { get; private set; }

        private Vector2 _workSpace;

        protected override void Awake()
        {
            base.Awake();
        
            rb=GetComponentInParent<Rigidbody2D>();

            facingDirection = 1;
            CanSetVelocity = true;
        }

        public override void LogicUpdate()
        {
            currentVelocity = rb.velocity;
        }

        #region Set Functions

        public void SetVelocityZero()
        {
            _workSpace = Vector2.zero;
            SetFinalVelocity();
        }

        public void SetVelocity(float velocity, Vector2 angle, int direction)
        {
            angle.Normalize();
            _workSpace.Set(angle.x*velocity*direction,angle.y * velocity);
            SetFinalVelocity();
        }

        public void SetVelocity(float velocity, Vector2 dir)
        {
            _workSpace = dir * velocity;
            SetFinalVelocity();
        }

        public void SetVelocityX(float velocity)
        {
            _workSpace.Set(velocity, currentVelocity.y);
            SetFinalVelocity();
        }

        public void SetVelocityY(float velocity)
        {
            _workSpace.Set(currentVelocity.x, velocity);
            SetFinalVelocity();
        }

        private void SetFinalVelocity()
        {
            if (CanSetVelocity)
            {
                rb.velocity = _workSpace;
                currentVelocity = _workSpace;
            }
        }

        public void CheckIfShouldFlip(int inputX)
        {
            if (inputX != 0 && inputX != facingDirection)
                Flip();
        }
    
        public void Flip()
        {
            facingDirection *= -1;
            rb.transform.Rotate(0.0f, 180.0f, 0.0f);
        }
    
        #endregion
    }
}
