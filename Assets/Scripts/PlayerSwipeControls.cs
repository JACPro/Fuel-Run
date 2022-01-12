using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CarLocomotion))]
public class PlayerSwipeControls : MonoBehaviour
{
    CarLocomotion driver;
    private float horizMoveRange = 12f;
    private float currHorizPos;
    private float startHorizPos;
    [Range(0, 1)] private float touchSpeedModifier = 1f;

    private Vector2 prevTouchPos;
    private Vector2 currTouchPos;

    private void Awake() 
    {
        currHorizPos = startHorizPos = horizMoveRange / 2;
        driver = GetComponent<CarLocomotion>();
    }

    private void Update()
    {      
        if (driver.GetMoveState() == MoveState.Turning)
        {
            if (Input.GetMouseButton(0) || Input.GetMouseButtonDown(0))
            {
                prevTouchPos = Input.mousePosition;
            }
        }
        
        if (driver.GetMoveState() != MoveState.Moving) return;
        
        if (Input.GetMouseButtonDown(0))
        {
            prevTouchPos = Input.mousePosition;
        }
        else if (Input.GetMouseButton(0))
        {
            currTouchPos = Input.mousePosition;
            MoveHorizontal((currTouchPos.x - prevTouchPos.x) * touchSpeedModifier);
            prevTouchPos = currTouchPos;
        }
    }

    private void MoveHorizontal(float amount)
    {
        float newXVal = currHorizPos + amount * Time.fixedDeltaTime;

        if (newXVal > horizMoveRange)
        {
            driver.MessageMoveHorizontal(horizMoveRange - currHorizPos);
            currHorizPos = horizMoveRange;
        }
        else if (newXVal < 0)
        {
            driver.MessageMoveHorizontal(- currHorizPos);
            currHorizPos = 0;
        }
        else
        {
            driver.MessageMoveHorizontal(amount * Time.fixedDeltaTime);
            currHorizPos = newXVal;
        }
    }

    public void ResetHorizPos()
    {
        currHorizPos = startHorizPos;
    }
}
