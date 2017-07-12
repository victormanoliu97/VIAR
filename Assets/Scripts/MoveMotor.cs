using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveMotor : MonoBehaviour {
    CharacterController myChar;
    bool inControl = true;
    [SerializeField]
    float forwardSpeed = 3.5f, backwardSpeed = 1.5f, turnSpeed = 50f, lateralSpeed = 2f;

    public float runSpeedForwardOffset = 1.6f, runSpeedTurnOffset = 0.5f, runSpeedLateralOffset = 0.4f;

    //paramaters for animations
    public bool isIdle = true, isWalkingForward, isWalkingBackward, isTurning, isRunning, isStrafing;
    public int isRotating;
    public int turningDirection, strafingDirection;
    

    private void Start()
    {
        myChar = GetComponent<CharacterController>();
    }
    private void FixedUpdate()
    {
        //gravity
        myChar.Move(Vector3.down * 0.98f); 
    }


    public void Turn(float _direction)
    {
        if(myChar.isGrounded)
        {
            float _speedOffset = Mathf.Abs(_direction); 
            if (_direction > 0)
                _direction = 1;
            else if (_direction < 0)
                _direction = -1;
            transform.eulerAngles += Vector3.up * _direction * turnSpeed * _speedOffset * Time.deltaTime;

            turningDirection = (int)_direction;
            if (turningDirection != 0)
                isTurning = true;
            else
                isTurning = false;
        }
    }
    public void TurnTo(float _newRotation)
    {
        //Place Holder
        transform.eulerAngles += Vector3.up * (_newRotation - transform.eulerAngles.y);
    }
    public void Move(int _direction)
    {
        if(myChar.isGrounded)
        {
            if (_direction > 0)
                MoveForward();
            else if (_direction < 0)
                MoveBackward();
            else
                Stand();
        }   
    }
    void MoveForward()
    {
        myChar.Move(transform.forward * forwardSpeed * Time.deltaTime);
        isWalkingForward = true;
        isWalkingBackward = false;
        isIdle = false;
    }
    void MoveBackward()
    {
        myChar.Move(-transform.forward * backwardSpeed * Time.deltaTime);
        isWalkingForward = false;
        isWalkingBackward = true;
        isIdle = false;
    }
    void Stand()
    {
        isWalkingForward = false;
        isWalkingBackward = false;
        isIdle = true;
    }

    public void Strafe(int _direction)
    {
        myChar.Move(transform.right * lateralSpeed * Time.deltaTime * _direction);

        strafingDirection = _direction;
        if (strafingDirection != 0)
            isStrafing = true;
        else
            isStrafing = false;
    }
    public void ActivateRunning()
    {
        forwardSpeed *= runSpeedForwardOffset;
        lateralSpeed *= runSpeedLateralOffset;
        turnSpeed *= runSpeedTurnOffset;
        isRunning = true;
    }
    public void DeactivateRunning()
    {
        forwardSpeed /= runSpeedForwardOffset;
        lateralSpeed /= runSpeedLateralOffset;
        turnSpeed /= runSpeedTurnOffset;
        isRunning = false;
    }
}
