using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveInput : MonoBehaviour {
    MoveMotor myMotor;
    Transform myCamPivot;
    CameraControl myCamControl;

    float sensitivity_x = 3f;

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
            //Place Holder
            if (Input.GetKeyDown(KeyCode.A))
                TurnTo(-90);
            if (Input.GetKeyDown(KeyCode.D))
                TurnTo(90);
            if (Input.GetKeyDown(KeyCode.W))
                TurnTo(0);
            if (Input.GetKeyDown(KeyCode.S))
                TurnTo(180);

            if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
                myMotor.Move(1);
            else
                myMotor.Move(0);
        }
	}

    void TurnTo(float _direction)
    {
        myCamControl.Lock();
        myMotor.TurnTo(myCamPivot.eulerAngles.y + _direction);
        myCamControl.ReturnToLock();
        myCamControl.UnLock();
    }
}
