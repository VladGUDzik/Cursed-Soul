using Generics;
using UnityEngine;

namespace Core.CoreComponents
{
    public class CollisionSenses : CoreComponent
    {
        protected Movement Movement => _movement ??= Core.GetCoreComponent(ref _movement);
        private Movement _movement;
        
        #region Check Transforms

        public Transform GroundCheck
        {
            get => GenericNotImplementedError<Transform>.TryGet(groundCheck, Core.transform.parent.name);
            private set => groundCheck = value;
        }
        public Transform WallCheck
        {
            get => GenericNotImplementedError<Transform>.TryGet(wallCheck, Core.transform.parent.name);
            private set => wallCheck = value; }
        public Transform LedgeCheckHorizontal {  
            get => GenericNotImplementedError<Transform>.TryGet(ledgeCheckHorizontal, Core.transform.parent.name);
            private set => ledgeCheckHorizontal = value; }
        public Transform LedgeCheckVertical {  
            get => GenericNotImplementedError<Transform>.TryGet(ledgeCheckVertical, Core.transform.parent.name);
            private set => ledgeCheckVertical = value; }
        public float GroundCheckRadius { get => groundCheckRadius; set => groundCheckRadius = value; }
        public float WallCheckDistance { get => wallCheckDistance; set => wallCheckDistance = value; }
        public LayerMask WhatIsGround { get => whatIsGround; set => whatIsGround = value; }

        [SerializeField] private Transform groundCheck;
        [SerializeField] private Transform wallCheck;
        [SerializeField] private Transform ledgeCheckHorizontal;
        [SerializeField] private Transform ledgeCheckVertical;
        [SerializeField] private float groundCheckRadius;
        [SerializeField] private float wallCheckDistance;

        [SerializeField] private LayerMask whatIsGround;
        [SerializeField] private LayerMask whatIsOtherEnemy;//в процессе
    
        #endregion
    
        #region Check Functions

        public bool ground
        {
            get => Physics2D.OverlapCircle(GroundCheck.position, groundCheckRadius, whatIsGround);
        }

        public bool ledgeHorizontal
        {
            get => Physics2D.Raycast(LedgeCheckHorizontal.position, Vector2.right * Movement.facingDirection,
                wallCheckDistance,
                whatIsGround);
        }

        public bool wallFront
        {
            get => Physics2D.Raycast(WallCheck.position, Vector2.right * Movement.facingDirection, 
                wallCheckDistance, whatIsGround);
        } 
        public bool wallBack
        {
            get => Physics2D.Raycast(WallCheck.position, Vector2.right * -Movement.facingDirection, 
                wallCheckDistance, whatIsGround);
        }

        public bool ledgeVertical
        {
            get => Physics2D.Raycast(LedgeCheckVertical.position, Vector2.down,
                wallCheckDistance,
                whatIsGround);
        }

        public bool otherEnemyOnTheWay//
        {
            get => Physics2D.Raycast(WallCheck.position, Vector2.right * Movement.facingDirection, 
                wallCheckDistance,
                whatIsOtherEnemy);
        }

        #endregion
    }
}
