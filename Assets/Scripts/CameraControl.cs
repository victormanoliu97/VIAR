using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {
    Camera myCam;
    Vector3 firstPersonDefault = Vector3.zero, thirdPersionDefault = Vector3.zero;

    float sensitivity_x = 1, sensitivity_y = 1, sensitivity_zoom = 1;
    const int MIN_ANGLE_3 = 360 - 15, MAX_ANGLE_3 = 40;
    const int MIN_ANGLE_1 = 40, MAX_ANGLE_1 = 360 - 68;
    const int MIN_ZOOM = -2, MAX_ZOOM = -9;
    public bool isReversed = false, FirstPerson = false, isCameraConstrained = false;
    float Offset_Collision = 0.2f, Offset_1st_z = 0.3f, Offset_1st_y = 0.9f;
    float zoom;

    bool isLocked = false;
    Vector3 lockedPosition;

    private void Start()
    {
        myCam = transform.GetChild(0).GetComponent<Camera>();
        zoom = myCam.transform.localPosition.z;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
            ChangePerspective();
        if (Input.GetKeyDown(KeyCode.L))
        {
            if (!isLocked)
                Lock();
            else
                UnLock();
        }
            

        if (FirstPerson)
        {
            float xRot = myCam.transform.localEulerAngles.x + Input.GetAxis("Mouse Y") * sensitivity_y;
            if (xRot > MIN_ANGLE_1 && xRot < 180)
                xRot = MIN_ANGLE_1;
            else if (xRot < MAX_ANGLE_1 && xRot > 180)
                xRot = MAX_ANGLE_1;
            myCam.transform.localEulerAngles = new Vector3(xRot, 0, 0);
        }
        else if(!isLocked)
        {
            //Rotation
            float yRot = Input.GetAxis("Mouse X") * sensitivity_x;
            float xRot = Input.GetAxis("Mouse Y") * sensitivity_y;
            if (isReversed)
                xRot = -xRot;

            Vector3 finalRotation = transform.localEulerAngles;
            finalRotation.y += yRot;
            finalRotation.x += xRot;
            if (finalRotation.x < MIN_ANGLE_3 && finalRotation.x > 180)
                finalRotation.x = MIN_ANGLE_3;
            else if (finalRotation.x > MAX_ANGLE_3 && finalRotation.x < 180)
                finalRotation.x = MAX_ANGLE_3;
            if (Input.GetMouseButton(1))
                transform.localEulerAngles = finalRotation;

            //Zoom
            ResolveCollision();
            if (!isCameraConstrained)
            {
                float newZoomPosition = zoom + Input.GetAxis("Mouse ScrollWheel") * sensitivity_zoom;
                if (newZoomPosition > MIN_ZOOM)
                {
                    ChangePerspective();
                    newZoomPosition = -4f;
                }
                else if (newZoomPosition < MAX_ZOOM)
                    newZoomPosition = MAX_ZOOM;

                zoom = newZoomPosition;
            }
        }

        else if (isLocked)
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, lockedPosition.y, 0);
    }

    void ResolveCollision()
    {
        Ray myRay = new Ray(transform.position - transform.forward, -transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(myRay,out hit,-zoom))
        {
            isCameraConstrained = true;
            myCam.transform.localPosition += Vector3.forward * ( -hit.distance - myCam.transform.localPosition.z + Offset_Collision);
        }
        else
        {
            isCameraConstrained = false;
            RestoreZoomPos();
        }
        
    }
    void RestoreZoomPos()
    {   
        myCam.transform.localPosition += Vector3.forward * (zoom - myCam.transform.localPosition.z); //Place Holder
    }
    void ChangePerspective()
    {
        FirstPerson = !FirstPerson;

        if(FirstPerson)
        {
            myCam.transform.localPosition = Vector3.forward * Offset_1st_z + Vector3.up * Offset_1st_y;
            transform.localEulerAngles = firstPersonDefault;
        }
        else
        {
            myCam.transform.localPosition += Vector3.forward * zoom - Vector3.up * Offset_1st_y;
            transform.localEulerAngles = thirdPersionDefault;
        }
    }
    public void Lock()
    {
        if(isLocked == false)
        {
            isLocked = true;
            lockedPosition = transform.eulerAngles;
        }
    }
    public void UnLock()
    {
        if(isLocked == true)
        {
            isLocked = false;
        }
    }
    public void ReturnToLock()
    {
        if (isLocked)
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, lockedPosition.y, 0);
    }
}
