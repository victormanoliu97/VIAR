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
        if (!myMotor.isWalkingForward && !myMotor.isWalkingBackward)
            myAnim.SetInteger("Vertical", 0);
        else
            if (myMotor.isWalkingForward)
                myAnim.SetInteger("Vertical", 1);
            else
                if (myMotor.isWalkingBackward)
                    myAnim.SetInteger("Vertical", -1);

        if (myMotor.strafingDirection == 0)
            myAnim.SetInteger("Horizontal", 0);
        else
            if (myMotor.strafingDirection > 0)
            myAnim.SetInteger("Horizontal", 1);
            else
                if (myMotor.strafingDirection < 0)
                myAnim.SetInteger("Horizontal", -1);
    }
}
