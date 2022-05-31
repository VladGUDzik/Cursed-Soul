using Hero.Data;
using Hero.HeroState.SubStates;
using Hero.Input;
using UnityEngine;

namespace Hero.HeroFiniteStateMachine
{
    public  class Hero : MonoBehaviour
    {
        #region State Variables
        public HeroStateMachine stateMachine { get; private set; }
        public HeroIdleState idleState { get; private set; }
        public HeroMoveState moveState { get; private set; }
        public HeroJumpState jumpState { get; private set; }
        public HeroInAirState airState { get; private set; }
        public HeroLandState landState { get; private set; }
        public HeroWallSlideState wallSlideState { get; private set; }
        public HeroWallGrabState wallGrabState { get; private set; }
        public HeroWallClimbState wallClimbState { get; private set; }
        public HeroJumpWallState jumpWallState { get; private set; }
        public HeroLedgeClimbeState ledgeClimbeState { get; private set; }
        public HeroDashState dashState { get; private set; }

        public HeroAttackState primarAttackState { get; private set; }
        
        public HeroAttackState secondaryAttackState { get; private set; }

        [SerializeField] 
        private HeroData data;
        #endregion

        #region Components

        public Core.Core core { get; private set; }
        public Animator anim { get; private set; }
        public HeroInputHandler inputHandler { get; private set; }
        public Rigidbody2D rb { get; private set; }
        public Transform dashDirectionIndicator { get; private set; }
        public HeroInventory inventory { get; private set; }

        #endregion
        
        #region Other Variable

        private Vector2 _workSpace;

        private Transform _originalParent;
        #endregion
    
        #region Unity Callback Functions
        private void Awake()
        {
            core = GetComponentInChildren<Core.Core>();
            
            stateMachine = new HeroStateMachine();

            idleState = new HeroIdleState(this,stateMachine,data,"idle");
            moveState = new HeroMoveState(this, stateMachine, data, "move");
            jumpState = new HeroJumpState(this, stateMachine, data, "inAir");
            airState = new HeroInAirState(this, stateMachine, data, "inAir");
            landState = new HeroLandState(this, stateMachine, data, "land");
            wallSlideState = new HeroWallSlideState(this, stateMachine, data, "wallSlide");
            wallGrabState = new HeroWallGrabState(this, stateMachine, data, "wallGrab");
            wallClimbState = new HeroWallClimbState(this, stateMachine, data, "wallClimbe");
            jumpWallState = new HeroJumpWallState(this, stateMachine, data, "inAir");
            ledgeClimbeState = new HeroLedgeClimbeState(this, stateMachine, data, "ledgeClimbeState");
            dashState = new HeroDashState(this, stateMachine, data, "inAir");//dash
            primarAttackState = new HeroAttackState(this, stateMachine, data, "attack");
            secondaryAttackState = new HeroAttackState(this, stateMachine, data, "attack");
        }
        private void Start()
        {
            anim = GetComponent<Animator>();
            inputHandler = GetComponent<HeroInputHandler>();
            rb = GetComponent<Rigidbody2D>();
            dashDirectionIndicator = transform.Find("DashDirectionIndicator");
            inventory = GetComponent<HeroInventory>();
 
            primarAttackState.SetWeapon(inventory.weapons[(int) CombatInputs.Primary]);
            secondaryAttackState.SetWeapon(inventory.weapons[(int) CombatInputs.Secondary]);//??
            
            stateMachine.Initialize(idleState);
            _originalParent = transform.parent;
        }
        
        private void Update()
        {
            core.LogicUpdate();
            stateMachine.currentState.LogicUpdate();
        }

        private void FixedUpdate()
        {
            stateMachine.currentState.PhysicsUpdate();
        }
        #endregion
        
        #region Other Functions
        
        private void AnimationTrigger() => stateMachine.currentState.AnimationTrigger();

        private void AnimationFinishTrigger() => stateMachine.currentState.AnimationFinishTrigger();

        public void SetParent(Transform newParent)
        {
            var trans = transform;
            _originalParent = trans.parent;
            trans.parent = newParent;
        }

        public void ResetParent()
        {
            transform.parent = _originalParent;
        }

        #endregion
    
    }
}
