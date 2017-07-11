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
        myAnim.SetBool("isWalking", myMotor.isWalkingForward);
        if(myMotor.isRunning)
            myAnim.SetFloat("forwardSpeed", myMotor.runSpeedForwardOffset);
        else
            myAnim.SetFloat("forwardSpeed", 1);
    }
}
