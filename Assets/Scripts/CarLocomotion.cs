using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction { Left = 0, Right = 1 }

public enum MoveState 
{ 
    Stopped, 
    Stopping,
    Stop,
    Moving,
    TurnLeft,
    TurnRight, 
    Turning 
}

public class CarLocomotion : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [Tooltip("Time to turn in seconds.")][SerializeField] private float turnSpeed = 0.3f;    
    private Rigidbody rb;
    private MoveState state;
    private Vector3 startPosition;
    private Quaternion startRotation;

    private void Awake() 
    {
        rb = GetComponent<Rigidbody>();
        state = MoveState.Stopped;
        startPosition = transform.position;
        startRotation = transform.rotation;
    }

    private void FixedUpdate() {
        switch (state) 
        {
            case (MoveState.Moving) :
                DriveForward(speed);
                break;
            case (MoveState.TurnLeft) :
                Turn(Direction.Left);
                break;
            case (MoveState.TurnRight) :
                Turn(Direction.Right);
                break;
            case (MoveState.Stop) :
                StopCar();
                break;
        }
    }

    private void DriveForward(float speed)
    {
        transform.position += transform.forward * speed * Time.fixedDeltaTime;
    }

    private void Turn(Direction direction)
    {
        if (direction == Direction.Left)
        {
            StartCoroutine(SmoothRotate(-90f));
            state = MoveState.Turning;
        }
        else
        {
            StartCoroutine(SmoothRotate(90f));
            state = MoveState.Turning;
        }
        
        state = MoveState.Turning;
    }

    private void StopCar()
    {
        StartCoroutine(StopSlowly());
        state = MoveState.Stopping;
    }

    public void MessageDriveForward()
    {
        state = MoveState.Moving;
    }

    public void MessageTurn(Direction direction)
    {
        //prevent turning when stopped and prevent calling new turn if already turning
        if (state != MoveState.Moving) return;

        if (direction == Direction.Left)
        {
            state = MoveState.TurnLeft;
        }
        else
        {
            state = MoveState.TurnRight;
        }
    }

    public void MessageTurnPivot(Direction direction, Transform pivotPoint)
    {
        if (state != MoveState.Moving) return;
        StartCoroutine(RotateAround(pivotPoint, direction, 0.4f));
        state = MoveState.Turning;
    }

    public void MessageStopCar()
    {
        if (state == MoveState.Moving)
        {
            state = MoveState.Stop;        
        }   
    }

    public void MessageMoveHorizontal(float amount)
    {
        if (state != MoveState.Moving) return;

        transform.position += transform.right * amount;
    }

    public MoveState GetMoveState()
    {
        return state;
    }

    public void ResetPosition()
    {
        transform.SetPositionAndRotation(startPosition, startRotation);
        state = MoveState.Stopped;
    }

    /*
    Rotate 90 degrees around a pivot point smoothly
    */
    private IEnumerator RotateAround(Transform pivot, Direction direction, float duration)
    {
        int degrees = 90 * (direction == Direction.Left ? -1 : 1);
        Quaternion startRot = transform.rotation;
        transform.RotateAround(transform.position, Vector3.up, degrees);
        Quaternion targetRot = transform.rotation;
        transform.rotation = startRot;

        float rotationSpeed = degrees/duration;
        for (float i = 0.0f; i < Mathf.Abs(degrees); i += Time.fixedDeltaTime * Mathf.Abs(rotationSpeed))
        {            
            transform.RotateAround(pivot.position, Vector3.up, Time.fixedDeltaTime * rotationSpeed);
            yield return new WaitForFixedUpdate();
        }
        transform.rotation = targetRot;
        state = MoveState.Moving;
    }

    private IEnumerator SmoothRotate(float angle)
    {
        Quaternion rotateStart = transform.rotation;
        Quaternion rotateGoal = transform.rotation * Quaternion.Euler(Vector3.up * angle);

        float elapsedTime = 0.0f;
        while (elapsedTime < turnSpeed)
        {
            transform.rotation = Quaternion.Slerp(rotateStart, rotateGoal, elapsedTime / turnSpeed);
            elapsedTime += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }

        transform.rotation = rotateGoal;
        state = MoveState.Moving;
    }

    private IEnumerator StopSlowly()
    {
        float lerpValue = 0.0f;
        float lerpedSpeed;

        while (lerpValue < 1f)
        {
            lerpedSpeed = Mathf.Lerp(speed, 0.0f, lerpValue);
            DriveForward(lerpedSpeed);
            lerpValue += 2 * Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }
        state = MoveState.Stopped;
    }
}
