using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Player Data")]
public class PlayerData : ScriptableObject
{
    [Header("Gravity")]
    [HideInInspector] public float gravityStrength; //Downwards force needed for the desired jumpHeight and jumpTimeToApex
    [HideInInspector] public float gravityScale;// Strength of the players gravity as a multiplier of gravity

    public float fallGravityMult;//Multiplier to the players gravityScale when falling
    public float maxFallSpeed;//Maximum fall speed of the player when falling

    public float fastFallGravityMult;//Larger multiplier to the players gravityScale when they are falling and downwards input is pressed
    public float maxFastFallSpeed;//Maximum fal speed of the player when performing faster fall

    [Header("Run")]
    public float runMaxSpeed; // Target speed we want the player to reach
    public float runAcceleration; // The speed at which our player accelerates to max speed, can be set to runMaxSpeedfor instant acceleration down to 0 for none at all
    [HideInInspector] public float runAccelAmount; //The actual force (multiplied with speedDiff) applied to the player
    public float runDecceleration; //The speed at which our player decelerates from their current speed, can be set to runMaxSpeed for instant deceleration down to 0 for none at all
    [HideInInspector] public float runDeccelAmount;//Actual force (multiplied with speedDiff) applied to the player
    [Range(0f, 1)] public float accelInAir;
    [Range(0f, 1)] public float deccelInAir;
    public bool doConserveMomentum = true;

    [Header("Dash")]
    public float dashTime;
    public float dashSpeed;

    [Header("Jump")]
    public float jumpHeight;
    public float jumpTimeToApex;
    [HideInInspector] public float jumpForce;

    [Header("Both Jumps")]
    public float jumpCutGravityMult; //Multiplier to increase gravity if the player releases the jump button while still jumping
    [Range(0f, 1)] public float jumpHangGravityMult; //Reduces gravity while close to the apex (desired max height of the jump)
    public float jumpHangTimeThreshold; //Speed where the player will experience extra "jump hang".

    public float jumpHangAccelerationMult;
    public float jumpHangMaxSpeedMult;

    [Header("Health")]
    public float colorDuration;
    public float maxHealth;
    public float hitNoColorTime;

    
    [Header("Assists")]
    [Range(0.01f, 0.5f)] public float coyoteTime;
    [Range(0.01f, 0.5f)] public float jumpInputBufferTime;
    [Range(0.01f, 0.5f)] public float dashInputBufferTime;


    private void OnValidate()
    {
        //Calculate gravity strength using the formula (gravity = 2* jumpHeight/ timeToJumpApex ^2)
        gravityStrength = -(2 * jumpHeight) / (jumpTimeToApex * jumpTimeToApex);

        //Calculate the rigidbodys gravity scale
        gravityScale = gravityStrength / Physics2D.gravity.y;

        //Calculate are run acceleration & decceleration forces using formula: amount = ((1/Time.fixedDeltaTime) * acceleration)/runMaxSpeed
        runAccelAmount = (50 * runAcceleration) / runMaxSpeed;
        runDeccelAmount = (50 * runDecceleration) / runMaxSpeed;

        //Calculate jumpForce using the formula (initialJumpVelocity = gravity * timeToJumpApex)
        jumpForce = Mathf.Abs(gravityStrength) * jumpTimeToApex;

        runAcceleration = Mathf.Clamp(runAcceleration, 0.01f, runMaxSpeed);
        runDecceleration= Mathf.Clamp(runDecceleration, 0.01f, runMaxSpeed);
    }


}
