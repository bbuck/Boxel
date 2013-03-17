using UnityEngine;
using System;
using System.Collections;

public enum MovementState
{
    Grounded,
    InAir,
    OnWall
}

[RequireComponent(typeof(Rigidbody))]
public class CharacterMotor : MonoBehaviour {

    public float maxVelocityChange = 1.0f;
    public float jumpHeight = 1.0f;
    public bool canJump = true;
	public bool canWallJump = true;
	public float airSpeed = 0.1f;
	public float wallSpeedAdjustment = 0.1f;
    public bool canMoveOnY = false;
	
    [HideInInspector]
    public Vector3 targetVelocity = Vector3.zero;

    [HideInInspector]
    public MovementState State { get; private set; }
	
    public bool OnGround
    {
        get
        {
            return State == MovementState.Grounded;
        }
    }

    public bool AbleToJump
    {
        get
        {
            return (State == MovementState.Grounded && canJump) || (State == MovementState.OnWall && canWallJump);
        }
    }

    public bool InAir
    {
        get
        {
            return State == MovementState.InAir || (!canWallJump && State == MovementState.OnWall);
        }
    }
	
	private const float OnGroundValue = .9f;
	private const float OnWallValue = 0.005f;
	
    private bool jumpQueued = false;
	private Vector3 lastContactNormal;

    void FixedUpdate()
    {
        Vector3 velocity = rigidbody.velocity;
        velocity = (targetVelocity - velocity);
        velocity = velocity.normalized * Mathf.Clamp(velocity.magnitude, 0, maxVelocityChange);
        if (!canMoveOnY)
            velocity.y = 0;
		
		if (State != MovementState.Grounded)
			velocity = velocity * airSpeed;
		
		if (State == MovementState.OnWall)
			velocity.x *= wallSpeedAdjustment;
		
		if (jumpQueued && AbleToJump)
        {
            ExecuteJump();
            jumpQueued = false;
        }

        rigidbody.AddForce(velocity, ForceMode.VelocityChange);
		
		//print(string.Format("Current State: {0}", Enum.GetName(typeof(MovementState), State)));
        State = MovementState.InAir;
    }

    void OnCollisionStay(Collision collision)
    {
		if (State == MovementState.Grounded)
			return;
		
		lastContactNormal = collision.contacts[0].normal;
		
		float totalDot = 0f;
		foreach (ContactPoint point in collision.contacts)
		{
			totalDot += Vector3.Dot(point.normal, Vector3.up);
		}
		
		float avgDot = totalDot / collision.contacts.Length;
		//print(string.Format("Average DOT: {0}", avgDot));
		
		if (Approximately(avgDot, OnGroundValue))
			State = MovementState.Grounded;
		else if (avgDot >= 0)
			State = MovementState.OnWall;
    }

    #region helper functions

    void ExecuteJump()
    {
        float jumpSpeed = Mathf.Sqrt(2 * jumpHeight * -Physics.gravity.y);
        rigidbody.velocity = new Vector3(rigidbody.velocity.x, jumpSpeed, rigidbody.velocity.z);
		if (State == MovementState.OnWall)
		{
			float xVel = rigidbody.velocity.x;
			xVel = lastContactNormal.x * 5f;
			rigidbody.AddForce(new Vector3(xVel, 0, 0), ForceMode.VelocityChange);
		}
    }
	
	// HACK: Mathf.Approximately didn't suit my needs
	bool Approximately(float a, float b) {
		float rangeMax, rangeMin;
		if (a >= 0.1)
		{
			rangeMax = a + 0.1f;
			rangeMin = a - 0.1f;
		}
		else if (a > 0.01) 
		{
			rangeMax = a + 0.01f;
			rangeMin = a - 0.01f;
		}
		else // a > 0.001f 
		{
			rangeMax = a + 0.001f;
			rangeMin = a - 0.001f;
		}
		
		return b > rangeMin && b < rangeMax;
	}
	
    #endregion helper functions

    #region public functions

    public void Impulse(Vector3 direction)
    {
        rigidbody.AddForce(direction, ForceMode.VelocityChange);
    }

    public void Jump()
    {
        jumpQueued = true;
    }

    public void CancelJump()
    {
        jumpQueued = false;
    }

    #endregion public functions
}
