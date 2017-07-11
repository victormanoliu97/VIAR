using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveInput : MonoBehaviour {
    MoveMotor myMotor;
    Transform myCamPivot;
    CameraControl myCamControl;

    float sensitivity_x = 3f;
    Vector2 desiredDirection;
    float desiredRotation;

    private void Start()
    {
        myMotor = GetComponent<MoveMotor>();
        myCamPivot = transform.GetChild(1);
        myCamControl = myCamPivot.GetComponent<CameraControl>();
    }
    void Update ()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
            myMotor.ActivateRunning();
        if (Input.GetKeyUp(KeyCode.LeftShift))
            myMotor.DeactivateRunning();

        if(myCamControl.FirstPerson)
        {
            transform.eulerAngles += Vector3.up * Input.GetAxis("Mouse X") * sensitivity_x;

            myMotor.Move((int)Input.GetAxisRaw("Vertical"));
            myMotor.Strafe((int)Input.GetAxisRaw("Horizontal"));
        }
        else
        {
            Vector2 newDesiredDirection = new Vector2(Input.GetAxisRaw("Vertical"), Input.GetAxisRaw("Horizontal"));
            if(newDesiredDirection != desiredDirection)
            {
                desiredDirection = newDesiredDirection;
                desiredRotation = Angle.Add(Vector2toAngles(desiredDirection), myCamPivot.eulerAngles.y);
                if (desiredDirection != Vector2.zero)
                    TurnTo(desiredRotation);
            }
            
            //if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))
                
            if (desiredDirection == Vector2.zero)
                myMotor.Move(0);
            else if (RotatedAt(desiredRotation))
                myMotor.Move(1);
            else
                myMotor.Move(0);
        }
    }

    void TurnTo(float _direction)
    {
        myCamControl.Lock();
        myMotor.TurnTo(_direction);
        myCamControl.ReturnToLock();
        myCamControl.UnLock();
    }
    bool RotatedAt(float _rotation,float _sensibility = 1f)
    {
        //PlaceHolder
        //_rotation = -_rotation;
        float camRotation = myCamPivot.localEulerAngles.y;
        return Angle.Equals(transform.eulerAngles.y, _rotation, _sensibility);  
    }
    float Vector2toAngles(Vector2 _direction)
    {
        return Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg;
    }
}
