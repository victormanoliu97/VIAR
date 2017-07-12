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
        if (myMotor.isWalkingForward)
            myAnim.SetInteger("Vertical", 1);
        else if (myMotor.isWalkingBackward)
            myAnim.SetInteger("Vertical", -1);
        else
            myAnim.SetInteger("Vertical", 0);

        myAnim.SetInteger("Horizontal", myMotor.strafingDirection);
        myAnim.SetInteger("Rotate", myMotor.turningDirection);
    }
}
