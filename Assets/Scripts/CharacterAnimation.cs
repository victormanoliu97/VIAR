using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimation : MonoBehaviour {
    MoveMotor myMotor;
    Animator myAnim;

    private void Start()
    {
        myMotor = transform.parent.parent.GetComponent<MoveMotor>();
        myAnim = GetComponent<Animator>();
    }
    private void Update()
    {
        // Check for walk anim
        if (!myMotor.isWalkingForward && !myMotor.isWalkingBackward)
        {
            myAnim.SetInteger("Vertical", 0);

            // Check for strafe anim
            if (myMotor.strafingDirection == 0)
            {
                myAnim.SetInteger("Horizontal", 0);

                // Check for rotate anim
                if (myMotor.isRotating == 0)
                    myAnim.SetInteger("Rotate", 0);
                else
                    if(myMotor.isRotating > 0)
                    myAnim.SetInteger("Rotate", 1);
                else
                    if (myMotor.isRotating < 0)
                    myAnim.SetInteger("Rotate", -1);
            }
            else
            if (myMotor.strafingDirection > 0)
                myAnim.SetInteger("Horizontal", 1);
            else
                if (myMotor.strafingDirection < 0)
                myAnim.SetInteger("Horizontal", -1);

        }
        else
            if (myMotor.isWalkingForward)
            {
            myAnim.SetInteger("Vertical", 1);
            myAnim.SetInteger("Horizontal", 0);
            }
            else
                if (myMotor.isWalkingBackward)
                {
                myAnim.SetInteger("Vertical", -1);
                myAnim.SetInteger("Horizontal", 0);
                }   
    }
}
