using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental;
using UnityEngine;

namespace NarvalDev.Runner
{
    public class Player : MonoBehaviour
    {
        public static Player Instance => s_Instance;
        static Player s_Instance;

        GameManager m_GameManager;
        [SerializeField]
        private Transform startingPosition;

        [SerializeField] private PlayerData playerData;
        [SerializeField] private GameInput gameInput;

        [SerializeField] private LayerMask platformLayerMask;

        private Vector2 moveDirection;


        private bool isJumping = false;
        private bool isFacingRight;
        private bool isWalking;
        private bool isDashing;


        [Header("Jumping Variables")]
        private float lastOnGroundTime;
        private float lastPressedJumpTime;

        [Header("Dash variables")]
        private float lastPressedDashTime;

        private bool isJumpCut;
        private bool isJumpFalling;

        //Local references
        private Rigidbody2D rb;
        private BoxCollider2D boxCollider;

        private int coinCount=0;

        
        // Start is called before the first frame update
        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            boxCollider = GetComponent<BoxCollider2D>();
        }

        void Start()
        {
            GameManager.Instance.ResetLevel();
            SetGravityScale(playerData.gravityScale);
            transform.position = startingPosition.position;
            isFacingRight = true;
            gameInput.OnJumpAction += GameInput_OnJumpAction;
            gameInput.OnJumpUpAction += GameInput_OnJumpUpAction;
            gameInput.OnDashAction += GameInput_OnDashAction;
            


        }



        public void ResetPlayer()
        {
            transform.position = startingPosition.position;
        }

        // Update is called once per frame
        void Update()
        {
            if (GameManager.Instance.IsPlaying)
            {
                HandleTimers();
                HandleMovement();
                HandleDashing();
                CheckCollisions();
                HandleJumping();
                HandleGravity();
            }
            
        }

        private void FixedUpdate()
        {
            if (!isDashing && GameManager.Instance.IsPlaying)
            {
                rb.velocity = new Vector2(1f * playerData.runMaxSpeed * Time.deltaTime, rb.velocity.y);
            }

        }

        private void GameInput_OnJumpAction(object sender, EventArgs e)
        {
            lastPressedJumpTime = playerData.jumpInputBufferTime;
        }

        private void GameInput_OnJumpUpAction(object sender, EventArgs e)
        {
            if (CanJumpCut())
            {
                isJumpCut = true;
            }
        }

        private void GameInput_OnDashAction(object sender, EventArgs e)
        {
            lastPressedDashTime = playerData.dashTime;
        }
        private void HandleGravity()
        {
            if (!isDashing)
            {
                if (rb.velocity.y < 0 && moveDirection.y < 0)
                {
                    SetGravityScale(playerData.gravityScale * playerData.fastFallGravityMult);

                    rb.velocity = new Vector2(rb.velocity.x, Mathf.Max(rb.velocity.y, -playerData.maxFastFallSpeed));
                }
                else if (isJumpCut)
                {
                    SetGravityScale(playerData.gravityScale * playerData.jumpCutGravityMult);

                    rb.velocity = new Vector2(rb.velocity.x, Mathf.Max(rb.velocity.y, -playerData.maxFallSpeed));
                }
                else if ((isJumping || isJumpFalling) && Mathf.Abs(rb.velocity.y) < playerData.jumpHangTimeThreshold)
                {
                    SetGravityScale(playerData.gravityScale * playerData.jumpHangGravityMult);
                }
                else if (rb.velocity.y < 0)
                {
                    //Higher gravity if falling
                    SetGravityScale(playerData.gravityScale * playerData.fallGravityMult);
                    //Caps maximum fall speed, so when falling over large distances we dont accelerate to insanely high speeds
                    rb.velocity = new Vector2(rb.velocity.x, Mathf.Max(rb.velocity.y, -playerData.maxFallSpeed));
                }
                else
                {
                    SetGravityScale(playerData.gravityScale);
                }
            }
            else
            {
                SetGravityScale(0);
            }

        }

        private void HandleTimers()
        {
            lastOnGroundTime -= Time.deltaTime;
            lastPressedJumpTime -= Time.deltaTime;
            lastPressedDashTime -= Time.deltaTime;
        }


        private void HandleMovement()
        {
            if (!isDashing)
            {
                //moveDirection = gameInput.GetMovementVector();
                isWalking = rb.velocity.x !=0;

                if (isWalking)
                {
                    CheckDirectionToFace(rb.velocity.x > 0);
                }
                //moveDirection.x = Mathf.Round(moveDirection.x);
            }



        }


        private void HandleAttack()
        {

        }


        private void HandleJumping()
        {
            if (isJumping && rb.velocity.y < 0)
            {
                isJumping = false;
                isJumpFalling = true;

            }
            else if (rb.velocity.y < 0)
            {
                isJumpFalling = true;
            }

            if (lastOnGroundTime > 0 && !isJumping)
            {
                isJumpCut = false;
                if (!isJumping)
                {
                    isJumpFalling = false;
                }
            }
            if (CanJump() && lastPressedJumpTime > 0)
            {
                isJumping = true;
                isJumpCut = false;
                isJumpFalling = false;
                Jump();
            }
        }
        private void HandleDashing()
        {
            if (lastPressedDashTime > 0)
            {
                isDashing = true;
                rb.velocity = Vector2.zero;
                //Dash();
            }
            else
            {
                isDashing = false;
            }
        }



        private void Jump()
        {
            lastPressedJumpTime = 0;
            lastOnGroundTime = 0;

            #region Perform Jump
            float force = playerData.jumpForce;
            if (rb.velocity.y < 0)
            {
                force -= rb.velocity.y;
            }
            rb.AddForce(Vector2.up * force, ForceMode2D.Impulse);
            #endregion
        }

        private void Dash()
        {
            lastPressedDashTime = 0;

            float dashSpeed = playerData.dashSpeed;
            rb.AddForce(moveDirection * dashSpeed, ForceMode2D.Impulse);
        }


        private bool CanJump()
        {
            return lastOnGroundTime > 0 && !isJumping;
        }

        private bool CanJumpCut()
        {
            return isJumping && rb.velocity.y > 0;
        }

        public bool IsWalking()
        {
            return isWalking;
        }
        public bool IsJumping()
        {
            return isJumping;
        }

        public bool IsJumpFalling()
        {
            return isJumpFalling;
        }

        public bool IsDashing()
        {
            return isDashing;
        }
        private void CheckCollisions()
        {
            if (!isJumping)
            {
                if (CheckGrounded() && !isJumping)
                {
                    lastOnGroundTime = playerData.coyoteTime;
                }


            }
        }
        private void Turn()
        {
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
            isFacingRight = !isFacingRight;
        }
        private void CheckDirectionToFace(bool isMovingRight)
        {
            if (isMovingRight != isFacingRight)
                Turn();
        }
        private bool CheckGrounded()
        {
            RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, Vector2.down, 0.15f, platformLayerMask);
            return raycastHit.collider != null;
        }

        private void SetGravityScale(float scale)
        {
            rb.gravityScale = scale;
        }

        public Vector3 GetPosition()
        {
            return transform.position;
        }

        //Move from here to a manager
        public void AddCoin()
        {
            coinCount += 1;
        }
    }

}
